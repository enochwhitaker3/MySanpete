using RazorClassLibrary.DTOs;
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
        public string[] AddBundleToStripe(AddBundleRequest bundle)
        {
            throw new NotImplementedException();
        }

        public string[] AddProductToStripe(AddVoucherRequest request)
        {
            throw new NotImplementedException();
        }

        public Task Checkout(AddVoucherRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<string> CreateRefund(UserVoucherDTO userVoucher)
        {
            throw new NotImplementedException();
        }

        public bool ValidateStripeId(string id)
        {
            return true;
        }
    }
}
