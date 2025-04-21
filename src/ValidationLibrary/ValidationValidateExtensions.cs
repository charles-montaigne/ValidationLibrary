using ResultLibrary;

namespace ValidationLibrary;

public static class ValidationValidateExtensions
{
    public static Validation<R> Validate<T, R, TErrors>(this Validation<T> validation, Func<T, Result<R, TErrors>> func)
        where TErrors : IEnumerable<Error>
    {
        return validation.Bind((property, t) => func(t).Match(
            r => Validation<R>.Success(property, r),
            errors => Validation<R>.Failure(new ValidationErrors([.. errors.Select(e => new ValidationError(property, e))]))));
    }
}