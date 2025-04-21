namespace ResultLibrary;

public interface IResult<TOk, TError>
{
    R Match<R>(Func<TOk, R> okFunc, Func<TError, R> errorFunc);
}

public abstract record Result<TOk, TError> : IResult<TOk, TError>
{
    public static Result<TOk, TError> Success(TOk ok) => new Success<TOk, TError>(ok);
    public static Result<TOk, TError> Failure(TError error) => new Failure<TOk, TError>(error);

    public abstract T Match<T>(Func<TOk, T> okFunc, Func<TError, T> errorFunc);
}

public record Success<TOk, TError>(TOk Ok) : Result<TOk, TError>
{
    public override T Match<T>(Func<TOk, T> okFunc, Func<TError, T> errorFunc) => okFunc(Ok);
}

public record Failure<TOk, TError>(TError Error) : Result<TOk, TError>
{
    public override T Match<T>(Func<TOk, T> okFunc, Func<TError, T> errorFunc) => errorFunc(Error);
}