using FluentValidation;
using UrlShortenerMvc.ViewModels;

namespace UrlShortenerMvc.Validators;

public class CreateUrlValidator : AbstractValidator<CreateUrlViewModel>
{
    public CreateUrlValidator()
    {
        RuleFor(v => v.Url)
            .NotEmpty()
            .Must(url => Uri.TryCreate(url, UriKind.Absolute, out _))
            .WithMessage("Invalid URL.");

        RuleFor(v => v.ExpiresAt)
            .GreaterThan(DateTime.UtcNow)
            .When(v => v.ExpiresAt.HasValue)
            .WithMessage("Expiry must be in the future.");
    }
}