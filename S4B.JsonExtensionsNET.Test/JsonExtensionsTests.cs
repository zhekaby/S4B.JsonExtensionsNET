using FluentAssertions;
using System.Linq;
using Xunit;

namespace S4B.JsonExtensionsNET.Test
{
    public class UnitTest1
    {
        [Theory, ClassData(typeof(FlattenTestDataProvider))]
        public void TestFlatten(Model data, (string key, string value)[] expected)
        {
            var actual = JsonExtensions.Flatten(data).ToArray();
            actual.Should().Equal(expected);
        }

        [Theory, ClassData(typeof(FlattenTestDataProvider))]
        public void TestUnflatten(Model expected, (string key, string value)[] data)
        {
            var actual = JsonExtensions.Unflatten<Model>(data);
            actual.Should().BeEquivalentTo(expected);
        }
    }
}
