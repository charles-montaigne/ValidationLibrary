using ResultLibrary;
using ValidationLibrary;

namespace Tests;

public class ComplexTests
{
    [Fact]
    public void Test1()
    {
        var request = new Request(Guid.NewGuid(), 1, "hello", new DateTime(2025, 01, 01), new DateTime(2025, 12, 25));
        var rules = Rules.ForJson(request);

        var commandResult =
            (rules.For(x => x.Id).NotNull(),
            rules.Complex(x => x.Start,
                (rules.For(x => x.Start).NotNull(),
                rules.For(x => x.End).NotNull())
                .Apply(ComplexTestsPeriod.Create)))
            .Apply((id, period) => new ComplexTestsCommand(id, period));

        Assert.IsType<Success<Command, ValidationErrors>>(commandResult);
    }
}

public record ComplexTestsRequest(Guid? Id, DateTime? Start, DateTime? End);
public record ComplexTestsCommand(Guid Id, ComplexTestsPeriod Period);

public class ComplexTestsPeriod
{
    public DateTime Start { get; }
    public DateTime End { get; }

    private ComplexTestsPeriod(DateTime start, DateTime end)
    {
        Start = start;
        End = end;
    }

    public static Result<ComplexTestsPeriod, Error> Create(DateTime start, DateTime end)
    {
        if (start > end)
            return InvalidPeriod.Instance;

        return new ComplexTestsPeriod(start, end);
    }

    public class InvalidPeriod : Error
    {
        public InvalidPeriod() : base("invalid_period")
        {
        }

        public static readonly InvalidPeriod Instance = new();
    }
}