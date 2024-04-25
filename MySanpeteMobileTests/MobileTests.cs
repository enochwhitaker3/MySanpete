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
        public void Test1()
        {
            var page = SetUpTest();

            var result = page.AddNumbers(1.0, 2.0);

            result.Should().Be(3.0);
        }

        public Calculator SetUpTest()
        {
            var page = new MySanpeteMobile.Components.Pages.Calculator();
            return page;
        }
    }
}