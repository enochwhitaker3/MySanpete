using RazorClassLibrary.Requests;
using RazorClassLibrary.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySanpeteTests.Dummies
{
    internal class DummyStripeService : IStripeService
    {
        public string[] AddProductToStripe(AddVoucherRequest request)
        {
            throw new NotImplementedException();
        }

        public Task Checkout(AddVoucherRequest request)
        {
            throw new NotImplementedException();
        }

        public bool ValidateStripeId(string id)
        {
            return true;
        }
    }
}
