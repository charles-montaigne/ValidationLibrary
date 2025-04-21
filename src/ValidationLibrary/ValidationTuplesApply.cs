using ResultLibrary;

namespace ValidationLibrary;
public static class ValidationTuplesApply
{
    private static Result<T, ValidationErrors> ToResult<T>(T value) => Result<T, ValidationErrors>.Success(value);

    public static Result<R, ValidationErrors> Apply<T1, T2, R>(
        this (Validation<T1>, Validation<T2>) validations,
        Func<T1, T2, R> func)
    {
        return ToResult(func)
            .Apply(validations.Item1).Apply(validations.Item2);
    }

    public static Result<R, ValidationErrors> Apply<T1, T2, T3, R>(
        this (IResult<T1, ValidationErrors>, IResult<T2, ValidationErrors>, IResult<T3, ValidationErrors>) validations,
        Func<T1, T2, T3, R> func)
    {
        return ToResult(func)
            .Apply(validations.Item1).Apply(validations.Item2).Apply(validations.Item3);
    }

    public static Result<R, ValidationErrors> Apply<T1, T2, T3, T4, R>(
        this (IResult<T1, ValidationErrors>, IResult<T2, ValidationErrors>, IResult<T3, ValidationErrors>, IResult<T4, ValidationErrors>) validations,
        Func<T1, T2, T3, T4, R> func)
    {
        return ToResult(func)
            .Apply(validations.Item1).Apply(validations.Item2).Apply(validations.Item3).Apply(validations.Item4);
    }
}