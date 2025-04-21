using Microsoft.Extensions.Localization;
using System.Collections;

namespace ValidationLibrary;

public class Error : IReadOnlyCollection<Error>
{
    public string Code { get; }

    public int Count => 1;

    public Error(string code)
    {
        Code = code;
    }

    public string GetLocalizedMessage(IStringLocalizer stringLocalizer)
    {
        var localizedString = stringLocalizer.GetString($"{Code}");
        return localizedString.ResourceNotFound ? string.Empty : localizedString;
    }

    public IEnumerator<Error> GetEnumerator()
    {
        yield return this;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        yield return this;
    }
}