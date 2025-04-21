using ResultLibrary;

namespace ValidationLibrary;
public static class ValidationApply
{
    private static ValidationErrors Combine(ValidationErrors errors1, ValidationErrors errors2)
    {
        List<ValidationError> errors = [.. errors1.Errors, .. errors2.Errors];
        return new ValidationErrors(errors.AsReadOnly());
    }

    public static Result<R, ValidationErrors> Apply<T1, R>(this IResult<Func<T1, R>, ValidationErrors> funcResult, IResult<T1, ValidationErrors> validation)
    {
        return funcResult.Match(
            okFunc => validation.Match(
                t1 => Result<R, ValidationErrors>.Success(okFunc(t1)),
                validationErrors => validationErrors),
            validationErrors1 => validation.Match(
                t1 => validationErrors1,
                validationErrors2 => Combine(validationErrors1, validationErrors2)));
    }

    public static Result<Func<T2, R>, ValidationErrors> Apply<T1, T2, R>(this Result<Func<T1, T2, R>, ValidationErrors> funcResult, IResult<T1, ValidationErrors> validation)
    {
        return funcResult.Match(
            okFunc => validation.Match(
                t1 => Result<Func<T2, R>, ValidationErrors>.Success((t2) => okFunc(t1, t2)),
                validationErrors => validationErrors),
            validationErrors1 => validation.Match(
                t1 => validationErrors1,
                validationErrors2 => Combine(validationErrors1, validationErrors2)));
    }

    public static Result<Func<T2, T3, R>, ValidationErrors> Apply<T1, T2, T3, R>(this Result<Func<T1, T2, T3, R>, ValidationErrors> funcResult, IResult<T1, ValidationErrors> validation)
    {
        return funcResult.Match(
            okFunc => validation.Match(
                t1 => Result<Func<T2, T3, R>, ValidationErrors>.Success((t2, t3) => okFunc(t1, t2, t3)),
                validationErrors => validationErrors),
            validationErrors1 => validation.Match(
                t1 => validationErrors1,
                validationErrors2 => Combine(validationErrors1, validationErrors2)));
    }

    public static Result<Func<T2, T3, T4, R>, ValidationErrors> Apply<T1, T2, T3, T4, R>(this Result<Func<T1, T2, T3, T4, R>, ValidationErrors> funcResult, IResult<T1, ValidationErrors> validation)
    {
        return funcResult.Match(
            okFunc => validation.Match(
                t1 => Result<Func<T2, T3, T4, R>, ValidationErrors>.Success((t2, t3, t4) => okFunc(t1, t2, t3, t4)),
                validationErrors => validationErrors),
            validationErrors1 => validation.Match(
                t1 => validationErrors1,
                validationErrors2 => Combine(validationErrors1, validationErrors2)));
    }
}