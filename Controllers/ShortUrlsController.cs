using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UrlShortenerMvc.Data;
using UrlShortenerMvc.Models;
using UrlShortenerMvc.ViewModels;

namespace UrlShortenerMvc.Controllers;

public class ShortUrlsController : Controller
{
    private readonly AppDbContext _db;
    public ShortUrlsController(AppDbContext db) => _db = db;

    // ---------- UI: Create form ----------
    [HttpGet]
    public IActionResult Create() => View(new CreateUrlViewModel());

    [HttpPost]
    public async Task<IActionResult> Create(CreateUrlViewModel vm)
    {
        if (!ModelState.IsValid) return View(vm);

        string code = await GenerateUniqueCodeAsync();
        var entity = new ShortUrl
        {
            ShortCode = code,
            OriginalUrl = vm.Url,
            ExpiresAt = vm.ExpiresAt?.ToUniversalTime() ?? DateTime.UtcNow.AddDays(15)
        };
        _db.Add(entity);
        await _db.SaveChangesAsync();
        return RedirectToAction(nameof(Stats), new { code });
    }

    // ---------- Public redirect ----------
    [HttpGet("{code}")]
    public async Task<IActionResult> Go(string code)
    {
        var link = await _db.ShortUrls.FirstOrDefaultAsync(u => u.ShortCode == code);

        if (link is null || (link.ExpiresAt.HasValue && link.ExpiresAt <= DateTime.UtcNow))
            return NotFound("Short link invalid or expired.");

        link.ClickCount++;

        // Get client IP, prefer X-Forwarded-For if present
        string ip = HttpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault();
        if (string.IsNullOrWhiteSpace(ip))
            ip = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "n/a";
        else if (ip.Contains(","))
            ip = ip.Split(',')[0].Trim(); // In case of multiple IPs, take the first

        _db.Clicks.Add(new Click
        {
            ShortUrlId = link.Id,
            Referer = Request.Headers.Referer.ToString(),
            Agent = Request.Headers.UserAgent.ToString(),
            Ip = ip
        });

        await _db.SaveChangesAsync();
        return Redirect(link.OriginalUrl);
    }

    // ---------- Stats ----------
    public async Task<IActionResult> Stats(string code)
    {
        var link = await _db.ShortUrls
                            .Include(u => u.Clicks.OrderByDescending(c => c.At).Take(10))
                            .FirstOrDefaultAsync(u => u.ShortCode == code);
        return link is null ? NotFound() : View(link);
    }

    // ---------- Helpers ----------
    private async Task<string> GenerateUniqueCodeAsync()
    {
        string code;
        do
        {
            code = Convert.ToBase64String(RandomNumberGenerator.GetBytes(3))
                   .Replace('+', '-').Replace('/', '_').Substring(0, 4);
        } while (await _db.ShortUrls.AnyAsync(u => u.ShortCode == code));

        return code;
    }
}