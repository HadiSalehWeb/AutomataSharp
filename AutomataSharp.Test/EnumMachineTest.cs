using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace AutomataSharp.Test
{
    public class EnumMachineTest
    {
        enum TestEnum
        {
            State1,
            State2,
            State3,
            State4
        }

        readonly EnumMachine<TestEnum, bool> machine;

        public EnumMachineTest()
        {
            machine = new EnumMachine<TestEnum, bool>(
                TestEnum.State1,
                new TransitionFunction<TestEnum, bool>((s, a) => a ? (TestEnum)((int)(s + 1) % 4) : s),
                new List<TestEnum> { TestEnum.State4 }
            );
        }

        [Fact]
        public void TestEnumMachine()
        {
            Assert.Equal(new List<TestEnum> { TestEnum.State1, TestEnum.State2, TestEnum.State3, TestEnum.State4 }, machine.States);
        }
    }
}
