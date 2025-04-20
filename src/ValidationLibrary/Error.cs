using Microsoft.Extensions.Localization;

namespace ValidationLibrary;

public class Error
{
    public string Code { get; }

    public Error(string code)
    {
        Code = code;
    }

    public string GetLocalizedMessage(IStringLocalizer stringLocalizer)
    {
        var localizedString = stringLocalizer.GetString($"{Code}");
        return localizedString.ResourceNotFound ? string.Empty : localizedString;
    }
}