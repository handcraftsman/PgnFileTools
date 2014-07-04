using System.Linq;

using FluentAssert;

using NUnit.Framework;

using PgnFileTools;

namespace PgnFileToolsTests
{
    public class PgnStreamReaderTests
    {
        [TestFixture]
        public class When_asked_to_read
        {
            private PgnStreamReader _reader;

            [TestFixtureSetUp]
            public void BeforeFirstTest()
            {
                _reader = new PgnStreamReader();
            }

            [Test]
            public void Given_a_stream_that_contains_one_game__should_return_the_game()
            {
                const string input = "[Result \"1-0\"]\n1.a4 1-0\n";
                var result = _reader.Read(input.CreateStream()).ToList();
                result.Count.ShouldBeEqualTo(1);
            }
        }
    }
}