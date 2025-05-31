namespace UrlShortenerMvc.Models;

public class Click
{
    public int Id { get; set; }
    public int ShortUrlId { get; set; }
    public ShortUrl ShortUrl { get; set; } = default!;
    public DateTime At { get; set; } = DateTime.UtcNow;
    public string? Referer { get; set; }
    public string? Agent { get; set; }
    public string Ip { get; set; } = default!;
}