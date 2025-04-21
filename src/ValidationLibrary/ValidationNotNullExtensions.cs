using System.Collections.ObjectModel;

namespace ValidationLibrary;
public static class ValidationNotNullExtensions
{
    public static Validation<T> NotNull<T>(this Validation<T?> validation) where T : class
    {
        return validation.Bind((property, value) => value is not null ?
            Validation<T>.Success(property, value)
            : Validation<T>.Failure(new ValidationErrors(new ReadOnlyCollection<ValidationError>([new ValidationError(property, RequiredFieldIsMissing.Instance)]))));
    }

    public static Validation<T> NotNull<T>(this Validation<T?> validation) where T : struct
    {
        return validation.Bind((property, value) => value is not null ?
            Validation<T>.Success(property, value.Value)
            : Validation<T>.Failure(new ValidationErrors(new ReadOnlyCollection<ValidationError>([new ValidationError(property, RequiredFieldIsMissing.Instance)]))));
    }
}

public class RequiredFieldIsMissing : Error
{
    public RequiredFieldIsMissing() : base("required_field_is_missing")
    {
    }

    public static readonly RequiredFieldIsMissing Instance = new();
}