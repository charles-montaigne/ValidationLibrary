using System.Collections;
using System.Collections.ObjectModel;

namespace ValidationLibrary;

public record ValidationErrors(ReadOnlyCollection<ValidationError> Errors) : IReadOnlyCollection<ValidationError>
{
    public int Count => Errors.Count;
    public IEnumerator<ValidationError> GetEnumerator() => Errors.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public ValidationErrors(ValidationError validationError) : this(new ReadOnlyCollection<ValidationError>([validationError]))
    {
    }

    public ValidationErrors(List<ValidationError> validationErrors) : this(validationErrors.AsReadOnly())
    {
    }
}