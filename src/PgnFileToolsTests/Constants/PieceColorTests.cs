using FluentAssert;

using NUnit.Framework;

using PgnFileTools;

namespace PgnFileToolsTests.Constants
{
    public class PieceColorTests
    {
        [TestFixture]
        public class When_asked_to_GetFor_a_FEN_peice_type_code
        {
            [Test]
            public void Given__B__should_get__White()
            {
                var result = PieceColor.GetForFen('B');
                result.ShouldBeEqualTo(PieceColor.White);
            }

            [Test]
            public void Given__K__should_get__White()
            {
                var result = PieceColor.GetForFen('K');
                result.ShouldBeEqualTo(PieceColor.White);
            }

            [Test]
            public void Given__N__should_get__White()
            {
                var result = PieceColor.GetForFen('N');
                result.ShouldBeEqualTo(PieceColor.White);
            }

            [Test]
            public void Given__P__should_get__White()
            {
                var result = PieceColor.GetForFen('P');
                result.ShouldBeEqualTo(PieceColor.White);
            }

            [Test]
            public void Given__Q__should_get__White()
            {
                var result = PieceColor.GetForFen('Q');
                result.ShouldBeEqualTo(PieceColor.White);
            }

            [Test]
            public void Given__R__should_get__White()
            {
                var result = PieceColor.GetForFen('R');
                result.ShouldBeEqualTo(PieceColor.White);
            }

            [Test]
            public void Given__b__should_get__Black()
            {
                var result = PieceColor.GetForFen('b');
                result.ShouldBeEqualTo(PieceColor.Black);
            }

            [Test]
            public void Given__k__should_get__Black()
            {
                var result = PieceColor.GetForFen('k');
                result.ShouldBeEqualTo(PieceColor.Black);
            }

            [Test]
            public void Given__n__should_get__Black()
            {
                var result = PieceColor.GetForFen('n');
                result.ShouldBeEqualTo(PieceColor.Black);
            }

            [Test]
            public void Given__p__should_get__Black()
            {
                var result = PieceColor.GetForFen('p');
                result.ShouldBeEqualTo(PieceColor.Black);
            }

            [Test]
            public void Given__q__should_get__Black()
            {
                var result = PieceColor.GetForFen('q');
                result.ShouldBeEqualTo(PieceColor.Black);
            }

            [Test]
            public void Given__r__should_get__Black()
            {
                var result = PieceColor.GetForFen('r');
                result.ShouldBeEqualTo(PieceColor.Black);
            }
        }
    }
}