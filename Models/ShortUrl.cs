namespace UrlShortenerMvc.Models;

public class ShortUrl
{
    public int Id { get; set; }
    public string ShortCode { get; set; } = default!;
    public string OriginalUrl { get; set; } = default!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? ExpiresAt { get; set; }
    public int ClickCount { get; set; }

    public ICollection<Click> Clicks { get; set; } = [];
}