using ResultLibrary;
using System.Linq.Expressions;

namespace ValidationLibrary;
public class Rules<T>
{
    private readonly IProperty _property;
    private readonly T _input;

    public Rules(IProperty property, T value)
    {
        _property = property;
        _input = value;
    }

    public Validation<TProperty> For<TProperty>(Expression<Func<T, TProperty>> expression)
    {
        var propertyName = ((MemberExpression)expression.Body).Member.Name;
        var property = _property.WithSubProperty(propertyName);

        var propertyValue = expression.Compile().Invoke(_input);

        return Validation<TProperty>.Success(property, propertyValue);
    }

    public Result<TValueObject, ValidationErrors> Complex<TProperty, TValueObject>(
        Expression<Func<T, TProperty>> expression,
        Result<Result<TValueObject, Error>, ValidationErrors> complexResult)
    {
        return complexResult.Match(
            result => result.Match<Result<TValueObject, ValidationErrors>>(
                valueObject => valueObject,
                error =>
                {
                    var propertyName = ((MemberExpression)expression.Body).Member.Name;
                    var property = _property.WithSubProperty(propertyName);
                    return new ValidationErrors(new ValidationError(property, error));
                }),
            validationErrors => validationErrors);
    }
}

public static class Rules
{
    public static Rules<T> ForJson<T>(T input) => new(JsonPointer.Root, input);
}