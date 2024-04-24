using FluentAssertions;
using MySanpeteWeb.Components.Pages.BusinessPages;
using RazorClassLibrary.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySanpeteTests.UnitTests
{
    public class BusinessViewVouchersTests
    {
        [Fact]
        public void SortVouchersDoesNothingWithNoSort()
        {
            //Arrange
            var page = SetUpPage();

            //Act
            page.Sort();

            //Assert
            page.SortedVouchers.ElementAt(0).Promo_Code.Should().Be("2");
            page.SortedVouchers.ElementAt(1).Promo_Code.Should().Be("1");
        }

        [Fact]
        public void SortVouchersWorksWhenSortingByCode()
        {
            //Arrange
            var page = SetUpPage();

            //Act
            page.SortByCode(new Microsoft.AspNetCore.Components.ChangeEventArgs() { Value = "1" });

            //Assert
            page.SortedVouchers.ElementAt(0).Promo_Code.Should().Be("1");
            page.SortedVouchers.ElementAt(1).Promo_Code.Should().Be("2");
        }

        [Fact]
        public void SortVouchersWorksWhenSortingByUsername()
        {
            //Arrange
            var page = SetUpPage();

            //Act
            page.SortByUsername(new Microsoft.AspNetCore.Components.ChangeEventArgs() { Value = "Test1" });

            //Assert
            page.SortedVouchers.ElementAt(0)!.User!.Username.Should().Be("Test1");
            page.SortedVouchers.ElementAt(1)!.User!.Username.Should().Be("Test2");
        }

        private ViewVouchers SetUpPage()
        {
            var page = new ViewVouchers();
            var vouchers = new List<UserVoucherDTO>()
            {
                new UserVoucherDTO()
                {
                    Promo_Code = "2",
                    User = new UserDTO()
                    {
                        Username = "Test2",
                    }
                },
                new UserVoucherDTO()
                {
                    Promo_Code = "1",
                    User = new UserDTO()
                    {
                        Username = "Test1",
                    }
                }
            };
            page.vouchers = vouchers;
            return page;
        }
    }
}
