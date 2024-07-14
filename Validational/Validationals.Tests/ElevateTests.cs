using FluentAssertions;
using FluentValidation.Results;

namespace Validational.Tests;

public class ElevateTests
{
    [Fact]
    public void TestAllEntriesAreValid()
    {
        List<Validational<string>> foo = ["I", "Kina", "spiser", "de", "hund"];
        foo.Elevate()
            .Should().BeEquivalentTo(new
        {
            Value = new List<string> { "I", "Kina", "spiser", "de", "hund" }
        });
    }
    
    [Fact]
    public void TestOneEntryIsFailure()
    {
        List<Validational<string>> foo = ["I", new ValidationFailure("Kina", "kina"), "spiser", "de", "hund"];
        foo.Elevate()
            .Should().BeEquivalentTo(new
        {
            Failures = (object[]) [ new  { PropertyName = "Kina" } ]
        });
    }

    [Fact]
    public void TestMultipleEntriesAreFailures()
    {
        List<Validational<string>> foo = [
            "I",
            new ValidationFailure("Kina", string.Empty),
            "spiser",
            "de",
            new ValidationFailure("hund", string.Empty)
        ];
        foo.Elevate()
            .Should().BeEquivalentTo(new
        {
            Failures = (object[]) [ new  { PropertyName = "Kina" }, new { PropertyName = "hund"} ]
        });
    }
}