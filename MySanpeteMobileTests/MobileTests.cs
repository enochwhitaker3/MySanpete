using FluentAssertions;
using MySanpeteMobile.Components.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MySanpeteMobileTests
{
    public class MobileTests
    {
        [Fact]
        public void Adding1and2Gives3()
        {
            var page = SetUpTest();

            var result = page.AddNumbers(1.0, 2.0);

            result.Should().Be(3.0);
        }

        [Fact]
        public void Subtract1and2GivesNegative1()
        {
            var page = SetUpTest();

            var result = page.MinusNumbers(1.0, 2.0);

            result.Should().Be(-1.0);
        }

        [Fact]
        public void Multiplying2and3Gives6()
        {
            var page = SetUpTest();

            var result = page.MultiplyNumbers(2.0, 3.0);

            result.Should().Be(6.0);
        }

        [Fact]
        public void Dividing2and2Gives1()
        {
            var page = SetUpTest();

            var result = page.DivideNumbers(2.0, 2.0);

            result.Should().Be(1.0);
        }

        [Fact]
        public void MultipyByZeroGivesZero()
        {
            var page = SetUpTest();

            var result = page.MultiplyNumbers(3.0, 0.0);

            result.Should().Be(0.0);
        }

        [Fact]
        public void MultipyByADecimalGivesExpectedResult()
        {
            var page = SetUpTest();

            var result = page.MultiplyNumbers(3.0, 0.5);

            result.Should().Be(1.5);
        }

        public Calculator SetUpTest()
        {
            var page = new Calculator();
            return page;
        }
    }
}