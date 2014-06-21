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
            public void Given_a_move_representing__Bishop_captures_d4__should_return__Bxd4()
            {
                const string input = "Bxd4";
                var move = _parser.Parse(input);
                move.HasError.ShouldBeFalse();
                var result = move.ToAlgebraicString();
                result.ShouldBeEqualTo(input);
            }

            [Test]
            public void Given_a_move_representing__Bishop_on_Row_1_captures_d4__should_return__B1xd4()
            {
                const string input = "B1xd4";
                var move = _parser.Parse(input);
                move.HasError.ShouldBeFalse();
                var result = move.ToAlgebraicString();
                result.ShouldBeEqualTo(input);
            }

            [Test]
            public void Given_a_move_representing__Bishop_to_c8_DOUBLE_CHECK__should_return__Bc8_PLUS_PLUS()
            {
                const string input = "Bc8++";
                var move = _parser.Parse(input);
                move.HasError.ShouldBeFalse();
                var result = move.ToAlgebraicString();
                result.ShouldBeEqualTo(input);
            }

            [Test]
            public void Given_a_move_representing__Bishop_to_f5__should_return__Bf5()
            {
                const string input = "Bf5";
                var move = _parser.Parse(input);
                move.HasError.ShouldBeFalse();
                var result = move.ToAlgebraicString();
                result.ShouldBeEqualTo(input);
            }

            [Test]
            public void Given_a_move_representing__King_side_castle__should_return__O_DASH_O()
            {
                const string input = "O-O";
                var move = _parser.Parse(input);
                move.HasError.ShouldBeFalse();
                var result = move.ToAlgebraicString();
                result.ShouldBeEqualTo(input);
            }

            [Test]
            public void Given_a_move_representing__Pawn_on_File_c_captures_d4__should_return__cxd4()
            {
                const string input = "cxd4";
                var move = _parser.Parse(input);
                move.HasError.ShouldBeFalse();
                var result = move.ToAlgebraicString();
                result.ShouldBeEqualTo(input);
            }

            [Test]
            public void Given_a_move_representing__Pawn_to_a4__should_return__a4()
            {
                const string input = "a4";
                var move = _parser.Parse(input);
                move.HasError.ShouldBeFalse();
                var result = move.ToAlgebraicString();
                result.ShouldBeEqualTo(input);
            }

            [Test]
            public void Given_a_move_representing__Pawn_to_b1_Promotes_to_Queen__should_return__b1_EQUAL_Q()
            {
                const string input = "b1=Q";
                var move = _parser.Parse(input);
                move.HasError.ShouldBeFalse();
                var result = move.ToAlgebraicString();
                result.ShouldBeEqualTo(input);
            }

            [Test]
            public void Given_a_move_representing__Pawn_to_b3_CHECK__should_return__b3_PLUS()
            {
                const string input = "b3+";
                var move = _parser.Parse(input);
                move.HasError.ShouldBeFalse();
                var result = move.ToAlgebraicString();
                result.ShouldBeEqualTo(input);
            }

            [Test]
            public void Given_a_move_representing__Queen_side_castle__should_return__O_DASH_O_DASH_O()
            {
                const string input = "O-O-O";
                var move = _parser.Parse(input);
                move.HasError.ShouldBeFalse();
                var result = move.ToAlgebraicString();
                result.ShouldBeEqualTo(input);
            }
        }
    }
}