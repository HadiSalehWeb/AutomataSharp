using System;
using Xunit;

namespace AutomataSharp.Test
{
    public class TransitionFunctionTest
    {
        readonly TransitionFunction<int, int> function;

        public TransitionFunctionTest()
        {
            function = new TransitionFunction<int, int>((state, action) => state / action);
        }

        [Fact]
        public void TestAccepts()
        {
            Assert.True(function.Accepts(0, 1));
            Assert.True(function.Accepts(2, 1));
            Assert.True(function.Accepts(20, 3));
            Assert.False(function.Accepts(3, 0));
        }

        [Fact]
        public void TestTransition()
        {
            Assert.Equal(0, function.Transition(0, 1));
            Assert.Equal(2, function.Transition(2, 1));
            Assert.Equal(6, function.Transition(20, 3));
            Assert.Throws<DivideByZeroException>(() => function.Transition(3, 0));
        }
    }
}
