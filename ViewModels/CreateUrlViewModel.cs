namespace UrlShortenerMvc.ViewModels;

public class CreateUrlViewModel
{
    public string Url { get; set; } = "";
    public DateTime? ExpiresAt { get; set; }      // optional, will default to 15 days from now
}