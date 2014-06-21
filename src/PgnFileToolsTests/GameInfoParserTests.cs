using System.IO;
using System.Text;

using FluentAssert;

using NUnit.Framework;

using PgnFileTools;

namespace PgnFileToolsTests
{
    [TestFixture]
    public class GameInfoParserTests
    {
        private GameInfoParser _parser;

        [TestFixtureSetUp]
        public void BeforeFirstTest()
        {
            _parser = new GameInfoParser();
        }

        [Test]
        public void Given_a_GOOD_MOVE_symbolic_annotation_after_a_move_text__should_add_the_annotation_to_the_Move_information()
        {
            const string input = "[Result \"1-0\"]\n1.a4! 1-0\n";
            var result = _parser.Parse(CreateStream(input));
            result.Headers.Count.ShouldBeEqualTo(1);
            result.Headers["Result"].ShouldBeEqualTo("1-0");
            result.HasError.ShouldBeFalse();
            result.Moves.Count.ShouldBeEqualTo(1);
            result.Moves[0].ToAlgebraicString().ShouldBeEqualTo("a4");
            result.Moves[0].Annotation.ShouldBeEqualTo(SymbolicMoveAnnotation.GoodMove.Id);
        }

        [Test]
        public void Given_a_POOR_MOVE_symbolic_annotation_after_a_move_text__should_add_the_annotation_to_the_Move_information()
        {
            const string input = "[Result \"1-0\"]\n1.a4? 1-0\n";
            var result = _parser.Parse(CreateStream(input));
            result.Headers.Count.ShouldBeEqualTo(1);
            result.Headers["Result"].ShouldBeEqualTo("1-0");
            result.HasError.ShouldBeFalse();
            result.Moves.Count.ShouldBeEqualTo(1);
            result.Moves[0].ToAlgebraicString().ShouldBeEqualTo("a4");
            result.Moves[0].Annotation.ShouldBeEqualTo(SymbolicMoveAnnotation.PoorMove.Id);
        }

        [Test]
        public void Given_a_QUESTIONABLE_MOVE_symbolic_annotation_after_a_move_text__should_add_the_annotation_to_the_Move_information()
        {
            const string input = "[Result \"1-0\"]\n1.a4?! 1-0\n";
            var result = _parser.Parse(CreateStream(input));
            result.Headers.Count.ShouldBeEqualTo(1);
            result.Headers["Result"].ShouldBeEqualTo("1-0");
            result.HasError.ShouldBeFalse();
            result.Moves.Count.ShouldBeEqualTo(1);
            result.Moves[0].ToAlgebraicString().ShouldBeEqualTo("a4");
            result.Moves[0].Annotation.ShouldBeEqualTo(SymbolicMoveAnnotation.QuestionableMove.Id);
        }

        [Test]
        public void Given_a_SPECULATIVE_MOVE_symbolic_annotation_after_a_move_text__should_add_the_annotation_to_the_Move_information()
        {
            const string input = "[Result \"1-0\"]\n1.a4!? 1-0\n";
            var result = _parser.Parse(CreateStream(input));
            result.Headers.Count.ShouldBeEqualTo(1);
            result.Headers["Result"].ShouldBeEqualTo("1-0");
            result.HasError.ShouldBeFalse();
            result.Moves.Count.ShouldBeEqualTo(1);
            result.Moves[0].ToAlgebraicString().ShouldBeEqualTo("a4");
            result.Moves[0].Annotation.ShouldBeEqualTo(SymbolicMoveAnnotation.SpeculativeMove.Id);
        }

        [Test]
        public void Given_a_VERY_GOOD_MOVE_symbolic_annotation_after_a_move_text__should_add_the_annotation_to_the_Move_information()
        {
            const string input = "[Result \"1-0\"]\n1.a4!! 1-0\n";
            var result = _parser.Parse(CreateStream(input));
            result.Headers.Count.ShouldBeEqualTo(1);
            result.Headers["Result"].ShouldBeEqualTo("1-0");
            result.HasError.ShouldBeFalse();
            result.Moves.Count.ShouldBeEqualTo(1);
            result.Moves[0].ToAlgebraicString().ShouldBeEqualTo("a4");
            result.Moves[0].Annotation.ShouldBeEqualTo(SymbolicMoveAnnotation.VeryGoodMove.Id);
        }

        [Test]
        public void Given_a_VERY_POOR_MOVE_symbolic_annotation_after_a_move_text__should_add_the_annotation_to_the_Move_information()
        {
            const string input = "[Result \"1-0\"]\n1.a4?? 1-0\n";
            var result = _parser.Parse(CreateStream(input));
            result.Headers.Count.ShouldBeEqualTo(1);
            result.Headers["Result"].ShouldBeEqualTo("1-0");
            result.HasError.ShouldBeFalse();
            result.Moves.Count.ShouldBeEqualTo(1);
            result.Moves[0].ToAlgebraicString().ShouldBeEqualTo("a4");
            result.Moves[0].Annotation.ShouldBeEqualTo(SymbolicMoveAnnotation.VeryPoorMove.Id);
        }

        [Test]
        public void Given_a_comment_after_a_move_text__should_add_the_comment_to_the_Move_information()
        {
            const string input = "[Result \"1-0\"]\n1.a4 {foo} 1-0\n";
            var result = _parser.Parse(CreateStream(input));
            result.Headers.Count.ShouldBeEqualTo(1);
            result.Headers["Result"].ShouldBeEqualTo("1-0");
            result.HasError.ShouldBeFalse();
            result.Moves.Count.ShouldBeEqualTo(1);
            result.Moves[0].ToAlgebraicString().ShouldBeEqualTo("a4");
            result.Moves[0].Comment.ShouldBeEqualTo("foo");
        }

        [Test]
        public void Given_a_comment_between_the_last_header_and_first_move_number__should_add_the_comment_to_the_Game_information()
        {
            const string input = "[Result \"1-0\"]\n {foo}\n1.a4 1-0\n";
            var result = _parser.Parse(CreateStream(input));
            result.Headers.Count.ShouldBeEqualTo(1);
            result.Headers["Result"].ShouldBeEqualTo("1-0");
            result.HasError.ShouldBeFalse();
            result.Comment.ShouldBeEqualTo("foo");
            result.Moves.Count.ShouldBeEqualTo(1);
            result.Moves[0].ToAlgebraicString().ShouldBeEqualTo("a4");
        }

        [Test]
        public void Given_a_game_that_does_not_end_with_the_Result_header_value__should_set__HasError()
        {
            const string input = "[Result \"1-0\"]\n1.a4 ";
            var result = _parser.Parse(CreateStream(input));
            result.Headers.Count.ShouldBeEqualTo(1);
            result.Headers["Result"].ShouldBeEqualTo("1-0");
            result.HasError.ShouldBeTrue();
        }

        [Test]
        public void Given_a_numbered_annotation_after_a_move_text__should_add_the_annotation_to_the_Move_information()
        {
            const string input = "[Result \"1-0\"]\n1.a4 $2 1-0\n";
            var result = _parser.Parse(CreateStream(input));
            result.Headers.Count.ShouldBeEqualTo(1);
            result.Headers["Result"].ShouldBeEqualTo("1-0");
            result.HasError.ShouldBeFalse();
            result.Moves.Count.ShouldBeEqualTo(1);
            result.Moves[0].ToAlgebraicString().ShouldBeEqualTo("a4");
            result.Moves[0].Annotation.ShouldBeEqualTo(2);
        }

        [Test]
        public void Given_no_moves__the_game_should_be_marked__HasError()
        {
            const string input = "[Result \"0-1\"]";
            var result = _parser.Parse(CreateStream(input));
            result.HasError.ShouldBeTrue();
        }

        [Test]
        public void Given_one_header_and_one_half_move__should_parse_the_header_and_move()
        {
            const string input = "[Result \"1-0\"]\n1.a4 ";
            var result = _parser.Parse(CreateStream(input));
            result.Headers.Count.ShouldBeEqualTo(1);
            result.Headers["Result"].ShouldBeEqualTo("1-0");
            result.HasError.ShouldBeTrue();
            result.Moves.Count.ShouldBeEqualTo(1);
            result.Moves[0].ToAlgebraicString().ShouldBeEqualTo("a4");
        }

        [Test]
        public void Given_one_header_and_one_move_and_result__should_parse_the_header_and_move_and_set_HasError_to_false()
        {
            const string input = "[Result \"1-0\"]\n1.a4 1-0\n";
            var result = _parser.Parse(CreateStream(input));
            result.Headers.Count.ShouldBeEqualTo(1);
            result.Headers["Result"].ShouldBeEqualTo("1-0");
            result.HasError.ShouldBeFalse();
            result.Moves.Count.ShouldBeEqualTo(1);
            result.Moves[0].ToAlgebraicString().ShouldBeEqualTo("a4");
        }

        [Test]
        public void Given_one_header_and_one_ply__should_parse_the_header_and_moves()
        {
            const string input = "[Result \"1-0\"]\n1.d4 d5 1-0\n";
            var result = _parser.Parse(CreateStream(input));
            result.Headers.Count.ShouldBeEqualTo(1);
            result.Headers["Result"].ShouldBeEqualTo("1-0");
            result.HasError.ShouldBeFalse();
            result.Moves.Count.ShouldBeEqualTo(2);
            var move1 = result.Moves[0];
            move1.ToAlgebraicString().ShouldBeEqualTo("d4");
            move1.Number.ShouldBeEqualTo(1);
            var move2 = result.Moves[1];
            move2.ToAlgebraicString().ShouldBeEqualTo("d5");
            move2.Number.ShouldBeEqualTo(1);
        }

        [Test]
        public void Given_one_header_and_two_plys__should_parse_the_header_and_moves()
        {
            const string input = "[Result \"1-0\"]\n1.e4 c6\n2.d4 d5 1-0\n";
            var result = _parser.Parse(CreateStream(input));
            result.Headers.Count.ShouldBeEqualTo(1);
            result.Headers["Result"].ShouldBeEqualTo("1-0");
            result.HasError.ShouldBeFalse();
            result.Moves.Count.ShouldBeEqualTo(4);
            var move1 = result.Moves[0];
            move1.ToAlgebraicString().ShouldBeEqualTo("e4");
            move1.Number.ShouldBeEqualTo(1);
            var move2 = result.Moves[1];
            move2.ToAlgebraicString().ShouldBeEqualTo("c6");
            move2.Number.ShouldBeEqualTo(1);
            var move3 = result.Moves[2];
            move3.ToAlgebraicString().ShouldBeEqualTo("d4");
            move3.Number.ShouldBeEqualTo(2);
            var move4 = result.Moves[3];
            move4.ToAlgebraicString().ShouldBeEqualTo("d5");
            move4.Number.ShouldBeEqualTo(2);
        }

        [Test]
        public void Given_only_a_header_line__should_parse_the_header_line()
        {
            const string input = "[Result \"0-1\"]";
            var result = _parser.Parse(CreateStream(input));
            result.Headers["Result"].ShouldBeEqualTo("0-1");
            result.HasError.ShouldBeTrue();
        }

        [Test]
        public void Given_two_header_lines__should_parse_the_header_lines()
        {
            const string input = "[Result \"0-1\"]\n[Date \"2014.06.19\"]";
            var result = _parser.Parse(CreateStream(input));
            result.Headers.Count.ShouldBeEqualTo(2);
            result.Headers["Result"].ShouldBeEqualTo("0-1");
            result.Headers["Date"].ShouldBeEqualTo("2014.06.19");
            result.HasError.ShouldBeTrue();
        }

        [Test]
        public void Given_whitespace_between_the_move_number_and_move_text__should_parse_the_move()
        {
            const string input = "[Result \"1-0\"]\n1. a4 1-0\n";
            var result = _parser.Parse(CreateStream(input));
            result.Headers.Count.ShouldBeEqualTo(1);
            result.Headers["Result"].ShouldBeEqualTo("1-0");
            result.HasError.ShouldBeFalse();
            result.Moves.Count.ShouldBeEqualTo(1);
            result.Moves[0].ToAlgebraicString().ShouldBeEqualTo("a4");
        }

        private static StreamReader CreateStream(string input)
        {
            return new StreamReader(new MemoryStream(Encoding.ASCII.GetBytes(input)));
        }
    }
}