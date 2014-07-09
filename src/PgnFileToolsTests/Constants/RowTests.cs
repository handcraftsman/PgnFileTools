using FluentAssert;

using NUnit.Framework;

using PgnFileTools;

namespace PgnFileToolsTests.Constants
{
    public class RowTests
    {
        [TestFixture]
        public class When_asked_for_it_IsPromotionRow
        {
            [Test]
            public void Given__Row_1__should_return__true()
            {
                var result = Row.Row1.IsPromotionRow;
                result.ShouldBeTrue();
            }

            [Test]
            public void Given__Row_2__should_return__false()
            {
                var result = Row.Row2.IsPromotionRow;
                result.ShouldBeFalse();
            }

            [Test]
            public void Given__Row_3__should_return__false()
            {
                var result = Row.Row3.IsPromotionRow;
                result.ShouldBeFalse();
            }

            [Test]
            public void Given__Row_4__should_return__false()
            {
                var result = Row.Row4.IsPromotionRow;
                result.ShouldBeFalse();
            }

            [Test]
            public void Given__Row_5__should_return__false()
            {
                var result = Row.Row5.IsPromotionRow;
                result.ShouldBeFalse();
            }

            [Test]
            public void Given__Row_6__should_return__false()
            {
                var result = Row.Row6.IsPromotionRow;
                result.ShouldBeFalse();
            }

            [Test]
            public void Given__Row_7__should_return__false()
            {
                var result = Row.Row7.IsPromotionRow;
                result.ShouldBeFalse();
            }

            [Test]
            public void Given__Row_8__should_return__true()
            {
                var result = Row.Row8.IsPromotionRow;
                result.ShouldBeTrue();
            }
        }

        [TestFixture]
        public class When_asked_for_its_Index
        {
            [Test]
            public void Given__Row_1__should_return__1()
            {
                var result = Row.Row1.Index;
                result.ShouldBeEqualTo(1);
            }

            [Test]
            public void Given__Row_2__should_return__2()
            {
                var result = Row.Row2.Index;
                result.ShouldBeEqualTo(2);
            }

            [Test]
            public void Given__Row_3__should_return__3()
            {
                var result = Row.Row3.Index;
                result.ShouldBeEqualTo(3);
            }

            [Test]
            public void Given__Row_4__should_return__4()
            {
                var result = Row.Row4.Index;
                result.ShouldBeEqualTo(4);
            }

            [Test]
            public void Given__Row_5__should_return__5()
            {
                var result = Row.Row5.Index;
                result.ShouldBeEqualTo(5);
            }

            [Test]
            public void Given__Row_6__should_return__6()
            {
                var result = Row.Row6.Index;
                result.ShouldBeEqualTo(6);
            }

            [Test]
            public void Given__Row_7__should_return__7()
            {
                var result = Row.Row7.Index;
                result.ShouldBeEqualTo(7);
            }

            [Test]
            public void Given__Row_8__should_return__8()
            {
                var result = Row.Row8.Index;
                result.ShouldBeEqualTo(8);
            }
        }

        [TestFixture]
        public class When_asked_for_its_Symbol
        {
            [Test]
            public void Given__Row_1__should_return__1()
            {
                var result = Row.Row1.Symbol;
                result.ShouldBeEqualTo("1");
            }

            [Test]
            public void Given__Row_2__should_return__2()
            {
                var result = Row.Row2.Symbol;
                result.ShouldBeEqualTo("2");
            }

            [Test]
            public void Given__Row_3__should_return__3()
            {
                var result = Row.Row3.Symbol;
                result.ShouldBeEqualTo("3");
            }

            [Test]
            public void Given__Row_4__should_return__4()
            {
                var result = Row.Row4.Symbol;
                result.ShouldBeEqualTo("4");
            }

            [Test]
            public void Given__Row_5__should_return__5()
            {
                var result = Row.Row5.Symbol;
                result.ShouldBeEqualTo("5");
            }

            [Test]
            public void Given__Row_6__should_return__6()
            {
                var result = Row.Row6.Symbol;
                result.ShouldBeEqualTo("6");
            }

            [Test]
            public void Given__Row_7__should_return__7()
            {
                var result = Row.Row7.Symbol;
                result.ShouldBeEqualTo("7");
            }

            [Test]
            public void Given__Row_8__should_return__8()
            {
                var result = Row.Row8.Symbol;
                result.ShouldBeEqualTo("8");
            }
        }

        [TestFixture]
        public class When_asked_to_GetFor_with_an_index_character
        {
            [Test]
            public void Given__1__should_get__Row_1()
            {
                var result = Row.GetFor('1');
                result.ShouldBeEqualTo(Row.Row1);
            }

            [Test]
            public void Given__2__should_get__Row_2()
            {
                var result = Row.GetFor('2');
                result.ShouldBeEqualTo(Row.Row2);
            }

            [Test]
            public void Given__3__should_get__Row_3()
            {
                var result = Row.GetFor('3');
                result.ShouldBeEqualTo(Row.Row3);
            }

            [Test]
            public void Given__4__should_get__Row_4()
            {
                var result = Row.GetFor('4');
                result.ShouldBeEqualTo(Row.Row4);
            }

            [Test]
            public void Given__5__should_get__Row_5()
            {
                var result = Row.GetFor('5');
                result.ShouldBeEqualTo(Row.Row5);
            }

            [Test]
            public void Given__6__should_get__Row_6()
            {
                var result = Row.GetFor('6');
                result.ShouldBeEqualTo(Row.Row6);
            }

            [Test]
            public void Given__7__should_get__Row_7()
            {
                var result = Row.GetFor('7');
                result.ShouldBeEqualTo(Row.Row7);
            }

            [Test]
            public void Given__8__should_get__Row_8()
            {
                var result = Row.GetFor('8');
                result.ShouldBeEqualTo(Row.Row8);
            }
        }

        [TestFixture]
        public class When_asked_to_GetFor_with_an_index_number
        {
            [Test]
            public void Given__1__should_get__Row_1()
            {
                var result = Row.GetFor(1);
                result.ShouldBeEqualTo(Row.Row1);
            }

            [Test]
            public void Given__2__should_get__Row_2()
            {
                var result = Row.GetFor(2);
                result.ShouldBeEqualTo(Row.Row2);
            }

            [Test]
            public void Given__3__should_get__Row_3()
            {
                var result = Row.GetFor(3);
                result.ShouldBeEqualTo(Row.Row3);
            }

            [Test]
            public void Given__4__should_get__Row_4()
            {
                var result = Row.GetFor(4);
                result.ShouldBeEqualTo(Row.Row4);
            }

            [Test]
            public void Given__5__should_get__Row_5()
            {
                var result = Row.GetFor(5);
                result.ShouldBeEqualTo(Row.Row5);
            }

            [Test]
            public void Given__6__should_get__Row_6()
            {
                var result = Row.GetFor(6);
                result.ShouldBeEqualTo(Row.Row6);
            }

            [Test]
            public void Given__7__should_get__Row_7()
            {
                var result = Row.GetFor(7);
                result.ShouldBeEqualTo(Row.Row7);
            }

            [Test]
            public void Given__8__should_get__Row_8()
            {
                var result = Row.GetFor(8);
                result.ShouldBeEqualTo(Row.Row8);
            }
        }
    }
}