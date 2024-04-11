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
    public Task Checkout(AddVoucherRequest request);
    public string[] AddProductToStripe(AddVoucherRequest request);
    public Task<string> CreateRefund(UserVoucherDTO userVoucher);
    public bool ValidateStripeId(string id);
}
