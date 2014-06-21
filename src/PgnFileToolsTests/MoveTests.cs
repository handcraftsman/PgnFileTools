using FluentAssert;

using NUnit.Framework;

using PgnFileTools;

namespace PgnFileToolsTests
{
    public class MoveTests
    {
        [TestFixture]
        public class When_asked_to_convert_the_move_to_algebraic_notation
        {
            private AlgebraicMoveParser _parser;

            [TestFixtureSetUp]
            public void BeforeFirstTest()
            {
                _parser = new AlgebraicMoveParser();
            }

            [Test]
            public void Given_a_move_representing__B1xd4__should_return__B1xd4()
            {
                const string input = "B1xd4";
                var move = _parser.Parse(input);
                move.HasError.ShouldBeFalse();
                var result = move.ToAlgebraicString();
                result.ShouldBeEqualTo(input);
            }

            [Test]
            public void Given_a_move_representing__Bf5__should_return__Bf5()
            {
                const string input = "Bf5";
                var move = _parser.Parse(input);
                move.HasError.ShouldBeFalse();
                var result = move.ToAlgebraicString();
                result.ShouldBeEqualTo(input);
            }

            [Test]
            public void Given_a_move_representing__Bxd4__should_return__Bxd4()
            {
                const string input = "Bxd4";
                var move = _parser.Parse(input);
                move.HasError.ShouldBeFalse();
                var result = move.ToAlgebraicString();
                result.ShouldBeEqualTo(input);
            }

            [Test]
            public void Given_a_move_representing__a4__should_return__a4()
            {
                const string input = "a4";
                var move = _parser.Parse(input);
                move.HasError.ShouldBeFalse();
                var result = move.ToAlgebraicString();
                result.ShouldBeEqualTo(input);
            }

            [Test]
            public void Given_a_move_representing__b1_PROMOTE_Q__should_return__b1_EQUAL_Q()
            {
                const string input = "b1=Q";
                var move = _parser.Parse(input);
                move.HasError.ShouldBeFalse();
                var result = move.ToAlgebraicString();
                result.ShouldBeEqualTo(input);
            }

            [Test]
            public void Given_a_move_representing__b3_CHECK__should_return__b3_PLUS()
            {
                const string input = "b3+";
                var move = _parser.Parse(input);
                move.HasError.ShouldBeFalse();
                var result = move.ToAlgebraicString();
                result.ShouldBeEqualTo(input);
            }

            [Test]
            public void Given_a_move_representing__cxd4__should_return__cxd4()
            {
                const string input = "cxd4";
                var move = _parser.Parse(input);
                move.HasError.ShouldBeFalse();
                var result = move.ToAlgebraicString();
                result.ShouldBeEqualTo(input);
            }
        }
    }
}