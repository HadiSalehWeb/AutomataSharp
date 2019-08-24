using System;
using System.Collections.Generic;
using Xunit;

namespace AutomataSharp.Test
{
    public class TransitionDictionaryTest
    {
        readonly TransitionDictionary<int, int> dict;

        public TransitionDictionaryTest()
        {
            dict = new TransitionDictionary<int, int>
            {
                { ( 0, 1 ), 1 },
                { ( 1, 1 ), 2 },
                { ( 2, 1 ), 3 },
                { ( 3, 1 ), 0 },
                { ( 0, 0 ), 3 },
                { ( 1, 0 ), 0 },
                { ( 2, 0 ), 1 },
                { ( 3, 0 ), 2 },
            };
        }

        [Fact]
        public void TestAccepts()
        {
            Assert.True(dict.Accepts(0, 1));
            Assert.True(dict.Accepts(2, 1));
            Assert.True(dict.Accepts(3, 0));
            Assert.False(dict.Accepts(2, 3));
        }

        [Fact]
        public void TestTransition()
        {
            Assert.Equal(1, dict.Transition(0, 1));
            Assert.Equal(3, dict.Transition(2, 1));
            Assert.Equal(2, dict.Transition(3, 0));
            Assert.Throws<KeyNotFoundException>(() => dict.Transition(2, 3));
        }
    }
}
