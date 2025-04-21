using Result;

namespace ValidationLibrary;
public static class ValidationTuplesApply
{
    public static Result<R, ValidationErrors> Apply<T1, T2, R>(
    this (Validation<T1>, Validation<T2>) validations,
    Func<T1, T2, R> func)
    {
        return Result<Func<T1, T2, R>, ValidationErrors>.Success(func)
            .Apply(validations.Item1).Apply(validations.Item2);
    }

    public static Result<R, ValidationErrors> Apply<T1, T2, T3, R>(
        this (Validation<T1>, Validation<T2>, Validation<T3>) validations,
        Func<T1, T2, T3, R> func)
    {
        return Result<Func<T1, T2, T3, R>, ValidationErrors>.Success(func)
            .Apply(validations.Item1).Apply(validations.Item2).Apply(validations.Item3);
    }
}