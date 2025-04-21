using ResultLibrary;
using ValidationLibrary;

namespace Tests;

public class ValidateTests
{
    [Fact]
    public void Test1()
    {
        var request = new Request(Guid.NewGuid(), 2);
        var rules = Rules.ForJson(request);

        var commandResult = 
            (rules.For(x => x.Id).NotNull(),
            rules.For(x => x.Quantity).NotNull().Validate(Quantity.Create))
            .Apply((id, quantity) => new Command(id, quantity));

        Assert.IsType<Success<Command, ValidationErrors>>(commandResult);
    }

    public record Request(Guid? Id, int? Quantity);
    public record Command(Guid Id, Quantity Quantity);

    public class Quantity
    {
        public int Value { get; }

        private Quantity(int quantity)
        {
            Value = quantity;
        }

        public static Result<Quantity, InvalidQuantity> Create(int quantity)
        {
            if (quantity < 0)
                return InvalidQuantity.Instance;

            return new Quantity(quantity);
        }

        public class InvalidQuantity : Error
        {
            public InvalidQuantity() : base("invalid_quantity")
            {
            }

            public static readonly InvalidQuantity Instance = new();
        }
    }
}