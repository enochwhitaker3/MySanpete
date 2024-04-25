using FluentAssertions;
using MySanpeteWeb.Components.Pages.AdminPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySanpeteTests.UnitTests
{
    public class AddEventTests
    {
        [Fact]
        public void IfTitleIsNullExceptionIsThrown()
        {
            var page = SetupPage();
            page.Title = null;

            page.CreateRequest();

            page.ErrorMessage.Should().Be("The event needs to have a title");
        }

        [Fact]
        public void IfTitleIsDefaultTitleExceptionIsThrown()
        {
            var page = SetupPage();
            page.Title = "Default Title";

            page.CreateRequest();

            page.ErrorMessage.Should().Be("The event needs to have a title");
        }

        [Fact]
        public void IfTitleIsEmptyStringExceptionIsThrown()
        {
            var page = SetupPage();
            page.Title = "";

            page.CreateRequest();

            page.ErrorMessage.Should().Be("The event needs to have a title");
        }

        [Fact]
        public void IfStartDateIsPastEndDateExceptionIsThrown()
        {
            var page = SetupPage();
            page.Title = "Test";
            page.EndDate = DateTime.MinValue;
            page.StartDate = DateTime.MaxValue;

            page.CreateRequest();

            page.ErrorMessage.Should().Be("The end date needs to be further in the future than the start date");
        }

        [Fact]
        public void IfSelectedBusinessnameDoesntExistInListExceptionIsThrown()
        {
            var page = SetupPage();
            page.Title = "Test";
            page.EndDate = DateTime.MaxValue;
            page.StartDate = DateTime.MinValue;
            page.BusinessName = "";

            page.CreateRequest();

            page.ErrorMessage.Should().Be("You need to select a business for this event");
        }

        [Fact]
        public void IfSelectedBusinessnameIsNullExceptionIsThrown()
        {
            var page = SetupPage();
            page.Title = "Test";
            page.EndDate = DateTime.MaxValue;
            page.StartDate = DateTime.MinValue;
            var business1 = new RazorClassLibrary.DTOs.BusinessDTO();
            page.AllBusinesses = [business1];
            page.BusinessName = null;

            page.CreateRequest();

            page.ErrorMessage.Should().Be("You need to select a business for this event");
        }

        [Fact]
        public void IDescriptionIsNullExceptionIsThrown()
        {
            var page = SetupPage();
            page.Title = "Test";
            page.EndDate = DateTime.MaxValue;
            page.StartDate = DateTime.MinValue;
            var business1 = new RazorClassLibrary.DTOs.BusinessDTO() { BusinessName = "figma?"};
            page.AllBusinesses = [business1];
            page.BusinessName = "figma?";
            page.Description = null;

            page.CreateRequest();

            page.ErrorMessage.Should().Be("The event needs to have a description");
        }

        [Fact]
        public void IDescriptionIsDefaultExceptionIsThrown()
        {
            var page = SetupPage();
            page.Title = "Test";
            page.EndDate = DateTime.MaxValue;
            page.StartDate = DateTime.MinValue;
            var business1 = new RazorClassLibrary.DTOs.BusinessDTO() { BusinessName = "figma?" };
            page.AllBusinesses = [business1];
            page.BusinessName = "figma?";
            page.Description = "Default Description";

            page.CreateRequest();

            page.ErrorMessage.Should().Be("The event needs to have a description");
        }

        [Fact]
        public void IDescriptionIsEmptyExceptionIsThrown()
        {
            var page = SetupPage();
            page.Title = "Test";
            page.EndDate = DateTime.MaxValue;
            page.StartDate = DateTime.MinValue;
            var business1 = new RazorClassLibrary.DTOs.BusinessDTO() { BusinessName = "figma?" };
            page.AllBusinesses = [business1];
            page.BusinessName = "figma?";
            page.Description = "";

            page.CreateRequest();

            page.ErrorMessage.Should().Be("The event needs to have a description");
        }

        private AdminAddEvent SetupPage()
        {
            return new AdminAddEvent();
        }
    }
}
