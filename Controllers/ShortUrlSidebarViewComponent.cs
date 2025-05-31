using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UrlShortenerMvc.Data;

namespace UrlShortenerMvc.Controllers;

public class ShortUrlSidebarViewComponent : ViewComponent
{
    private readonly AppDbContext _db;
    public ShortUrlSidebarViewComponent(AppDbContext db) => _db = db;

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var urls = await _db.ShortUrls.OrderByDescending(u => u.CreatedAt).ToListAsync();
        return View(urls);
    }
} 