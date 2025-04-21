using ResultLibrary;

namespace ValidationLibrary;

public abstract record Validation<T> : IResult<T, ValidationErrors>
{
    public static Validation<T> Success(IProperty property, T value) => new ValidationSuccess<T>(property, value);
    public static Validation<T> Failure(ValidationErrors errors) => new ValidationFailure<T>(errors);

    public abstract Validation<R> Bind<R>(Func<T, Validation<R>> bind);
    public abstract Validation<R> Bind<R>(Func<IProperty, T, Validation<R>> bind);
    public abstract R Match<R>(Func<T, R> okFunc, Func<ValidationErrors, R> errorFunc);
}

public sealed record ValidationSuccess<T>(IProperty Property, T Value) : Validation<T>
{
    public override Validation<R> Bind<R>(Func<T, Validation<R>> bind) => bind(Value);
    public override Validation<R> Bind<R>(Func<IProperty, T, Validation<R>> bind) => bind(Property, Value);

    public override R Match<R>(Func<T, R> okFunc, Func<ValidationErrors, R> errorFunc) => okFunc(Value);
}

public sealed record ValidationFailure<T>(ValidationErrors ValidationErrors) : Validation<T>
{
    public override Validation<R> Bind<R>(Func<T, Validation<R>> bind) => Validation<R>.Failure(ValidationErrors);
    public override Validation<R> Bind<R>(Func<IProperty, T, Validation<R>> bind) => Validation<R>.Failure(ValidationErrors);

    public override R Match<R>(Func<T, R> okFunc, Func<ValidationErrors, R> errorFunc) => errorFunc(ValidationErrors);
}