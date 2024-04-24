using FluentAssertions;
using MySanpeteWeb.Components.Pages.AdminPages;
using MySanpeteWeb.Components.Pages.BusinessPages;
using RazorClassLibrary.DTOs;
using RazorClassLibrary.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySanpeteTests.UnitTests
{
    public class BusinessesComponentTests
    {
        [Fact]
        public void FilterBusinessByFullName()
        {
            var page = SetUpPage();

            page.FilterBusinesses(new Microsoft.AspNetCore.Components.ChangeEventArgs() { Value = "Test Business 1" });

            page.SearchedBusinesses.Count.Should().Be(1);
        }

        [Fact]
        public void FilterBusinessByPartialName()
        {
            var page = SetUpPage();

            page.FilterBusinesses(new Microsoft.AspNetCore.Components.ChangeEventArgs() { Value = "Test Business" });

            page.SearchedBusinesses.Count.Should().Be(2);
        }

        [Fact]
        public void FilterBusinessByBusinesThatDoesntExist()
        {
            var page = SetUpPage();

            page.FilterBusinesses(new Microsoft.AspNetCore.Components.ChangeEventArgs() { Value = "Example" });

            page.SearchedBusinesses.Count.Should().Be(0);
        }

        [Fact]
        public void FilterBusinessWhereOnlyOneExists()
        {
            var page = SetUpPage();

            page.FilterBusinesses(new Microsoft.AspNetCore.Components.ChangeEventArgs() { Value = "Different" });

            page.SearchedBusinesses.Count.Should().Be(1);
        }

        [Fact]
        public void FilterBusinessByShortString()
        {
            var page = SetUpPage();

            page.FilterBusinesses(new Microsoft.AspNetCore.Components.ChangeEventArgs() { Value = "u" });

            page.SearchedBusinesses.Count.Should().Be(2);
        }


        private Businesses SetUpPage()
        {
            var page = new Businesses();
            var AllBusinesses = new List<BusinessDTO>()
            {
                new BusinessDTO()
                {
                    BusinessName = "Test Business 1",
                    Address = "150 College Ave E, Ephraim, UT 84627",
                    Description = "Test Description",
                    Email = "email@email.com",
                    PhoneNumber = "1234567890",
                    LogoURL = "1234567890",
                },
                new BusinessDTO()
                {
                    BusinessName = "Test Business 2",
                    Address = "150 College Ave E, Ephraim, UT 84627",
                    Description = "Test Description",
                    Email = "email@email.com",
                    PhoneNumber = "1234567890",
                    LogoURL = "1234567890",
                },
                new BusinessDTO()
                {
                    BusinessName = "A Different One",
                    Address = "150 College Ave E, Ephraim, UT 84627",
                    Description = "Test Description",
                    Email = "email@email.com",
                    PhoneNumber = "1234567890",
                    LogoURL = "1234567890",
                },
            };
            page.AllBusinesses = AllBusinesses;
            return page;
        }
    }
}
