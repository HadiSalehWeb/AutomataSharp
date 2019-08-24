using System.Collections.Generic;
using Xunit;
using System;

namespace AutomataSharp.Test
{
    public class FiniteStateMachineTest
    {
        readonly FiniteStateMachine<int, bool> machine;

        public FiniteStateMachineTest()
        {
            machine = new FiniteStateMachine<int, bool>(
                0,
                new List<int> { 0, 1, 2, 3 },
                new TransitionFunction<int, bool>((s, a) => a ? (s + 1) % 4 : s),
                new List<int> { 1, 3 }
            );
        }

        [Fact]
        public void TestResetState()
        {
            Assert.Equal(0, machine.CurrentState);

            machine.ResetState();
            Assert.Equal(0, machine.CurrentState);

            machine.Feed(true);
            Assert.Equal(1, machine.CurrentState);

            machine.Feed(new List<bool> { true, false, false, true, false });
            Assert.Equal(3, machine.CurrentState);

            machine.ResetState();
            Assert.Equal(0, machine.CurrentState);
        }

        [Fact]
        public void TestResetHistory()
        {
            Assert.Equal(new List<bool> { }, machine.History);

            machine.ResetHistory();
            Assert.Equal(new List<bool> { }, machine.History);

            machine.Feed(true);
            Assert.Equal(new List<bool> { true }, machine.History);

            machine.Feed(new List<bool> { true, false, false, true, false });
            Assert.Equal(new List<bool> { true, true, false, false, true, false }, machine.History);

            machine.ResetHistory();
            Assert.Equal(new List<bool> { }, machine.History);
        }

        [Fact]
        public void TestFeed()
        {
            Assert.Equal(0, machine.CurrentState);
            machine.Feed(true);
            Assert.Equal(1, machine.CurrentState);
            machine.Feed(false);
            Assert.Equal(1, machine.CurrentState);
            machine.Feed(true);
            Assert.Equal(2, machine.CurrentState);
            machine.Feed(true);
            Assert.Equal(3, machine.CurrentState);
            machine.Feed(false);
            Assert.Equal(3, machine.CurrentState);
            machine.Feed(true);
            Assert.Equal(0, machine.CurrentState);
        }

        [Fact]
        public void TestFeedCollection()
        {
            Assert.Equal(0, machine.CurrentState);
            machine.Feed(new List<bool> { true, false, true });
            Assert.Equal(2, machine.CurrentState);
            machine.Feed(new List<bool> { false, false, true, false });
            Assert.Equal(3, machine.CurrentState);
            machine.Feed(new List<bool> { false, true, true, true, false, false, true });
            Assert.Equal(3, machine.CurrentState);
            machine.Feed(new List<bool> { });
            Assert.Equal(3, machine.CurrentState);
            machine.Feed(new List<bool> { false, false, false, false, false });
            Assert.Equal(3, machine.CurrentState);
            Assert.Throws<NullReferenceException>(() => machine.Feed(null));
        }

        [Fact]
        public void TestAccepts()
        {
            Assert.True(machine.Accepts(new List<bool> { true }));
            Assert.False(machine.Accepts(new List<bool> { }));
            Assert.True(machine.Accepts(new List<bool> { true, true, false, true }));
            Assert.True(machine.Accepts(new List<bool> { true, true, false, true, false, true, false, false, false, true }));
            Assert.False(machine.Accepts(new List<bool> { true, true, false, true, false, true, false, false, false, }));
            Assert.Throws<NullReferenceException>(() => machine.Accepts(null));
        }
    }
}
