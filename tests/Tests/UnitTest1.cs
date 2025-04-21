using ResultLibrary;
using ValidationLibrary;

namespace Tests;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        var request = new Request(Guid.NewGuid(), 1, "hello", new DateTime(2025, 01, 01), new DateTime(2025, 12, 25));
        var rules = Rules.ForJson(request);

        var commandResult = 
            (rules.For(x => x.Id).NotNull(),
            rules.For(x => x.Number).NotNull(),
            rules.For(x => x.Label).NotNull(),
            rules.Complex(x => x.Start,
                (rules.For(x => x.Start).NotNull(),
                rules.For(x => x.End).NotNull())
                .Apply(Period.Create)))
            .Apply((id, number, label, period) => new Command(id, number, label, period));

        Assert.IsType<Success<Command, ValidationErrors>>(commandResult);
    }
}

public record Request(Guid? Id, int? Number, string? Label, DateTime? Start, DateTime? End);
public record Command(Guid Id, int Number, string Label, Period Period);

public class Period
{
    public DateTime Start { get; }
    public DateTime End { get; }

    private Period(DateTime start, DateTime end)
    {
        Start = start;
        End = end;
    }

    public static Result<Period, Error> Create(DateTime start, DateTime end)
    {
        if (start > end)
            return InvalidPeriod.Instance;

        return new Period(start, end);
    }

    public class InvalidPeriod : Error
    {
        public InvalidPeriod() : base("invalid_period")
        {
        }

        public static readonly InvalidPeriod Instance = new();
    }
}