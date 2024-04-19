using RazorClassLibrary.DTOs;
using RazorClassLibrary.Requests;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorClassLibrary.Services;

public interface IStripeService
{
    public Task Checkout(PurchaseVoucherRequest request);
    public Task BundleCheckout(PurchaseBundleRequest request);
    public string[] AddProductToStripe(AddVoucherRequest request);

    public string UpdateStripeProduct(VoucherDTO newVoucher);
    public string[] AddBundleToStripe(AddBundleRequest bundle);
    public Task<string> CreateRefund(UserVoucherDTO userVoucher);
    public bool ValidateStripeId(string id);
}
