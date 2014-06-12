using FluentAssert;

using NUnit.Framework;

using PgnFileTools;

namespace PgnFileToolsTests.Constants
{
    public class CastleTypeTests
    {
        [TestFixture]
        public class When_asked_for_its_Symbol
        {
            [Test]
            public void Given_King_Side_Castle__should_return__O_DASH_O()
            {
                var result = CastleType.KingSide.Symbol;
                result.ShouldBeEqualTo("O-O");
            }

            [Test]
            public void Given_Queen_Side_Castle__should_return__O_DASH_O_DASH_O()
            {
                var result = CastleType.QueenSide.Symbol;
                result.ShouldBeEqualTo("O-O-O");
            }
        }
    }
}