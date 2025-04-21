using Result;
using ValidationLibrary;

namespace Tests;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        var request = new Request(Guid.NewGuid(), 1, "hello");
        var rules = Rules.ForJson(request);

        var commandResult = 
            (rules.For(x => x.Id).NotNull(),
            rules.For(x => x.Number).NotNull(),
            rules.For(x => x.Label).NotNull())
            .Apply((id, number, label) => new Command(id, number, label));

        Assert.IsType<Success<Command, ValidationErrors>>(commandResult);
    }
}

public record Request(Guid? Id, int? Number, string? Label);
public record Command(Guid Id, int Number, string Label);