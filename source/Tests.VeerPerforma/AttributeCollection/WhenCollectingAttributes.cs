using System.Linq;
using System.Reflection;
using Shouldly;
using VeerPerforma.Attributes;
using VeerPerforma.Utils;
using Xunit;

namespace Test.AttributeCollection;

public class WhenCollectingAttributes
{
    [Fact]
    public void AllAttributesCanBeFound()
    {
        var allTypesWithAttribute = Assembly
            .GetAssembly(typeof(WhenCollectingAttributes))!
            .GetTypes()
            .Where(t => t.HasAttribute<VeerPerformaAttribute>())
            .ToArray();

        allTypesWithAttribute.Length.ShouldBeGreaterThan(0);
        allTypesWithAttribute
            .Select(x => x.Name)
            .OrderBy(x => x)
            .ToArray()
            .ShouldBe(
                new[] { nameof(TestClassOne), nameof(TestClassTwo) }
                    .OrderBy(x => x));
    }

    [VeerPerforma]
    public class TestClassOne
    {
    }

    [VeerPerforma(3)]
    public class TestClassTwo
    {
    }
}