using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore.Metadata;
using RazorClassLibrary.Data;
using RazorClassLibrary.DTOs;
using RazorClassLibrary.Requests;
using RazorClassLibrary.Services;
using Stripe;
using Stripe.Checkout;

namespace MySanpeteWeb.Services;

public class StripeService : IStripeService
{
    private readonly NavigationManager NavMan;
    public StripeService(NavigationManager NavMan)
    {
        this.NavMan = NavMan;
    }

    public StripeService()
    {
        this.NavMan = null!;
    }
    public async Task Checkout(AddVoucherRequest request)
    {
        var domain = "https://localhost:7059"; // TODO change when we post to azure
        var options = new SessionCreateOptions
        {
            LineItems = new List<SessionLineItemOptions>
        {
            new()
            {
                // Provide the exact Price ID (for example, price_1234) of the product you want to sell
                //Price = "price_1OyLIuDb4weiXajfjif0Porn",
                Price = request.PriceId,
                Quantity = 1,
            },
        },
            Mode = "payment",
            SuccessUrl = domain + "/OrderComplete",
            CancelUrl = domain + "/OrderAbandoned"
        };
        try
        {

            var service = new SessionService();
            var session = await service.CreateAsync(options);
            NavMan.NavigateTo(session.Url);
        }
        catch (Exception)
        {

        }
    }

    public string[] AddProductToStripe(AddVoucherRequest request)
    {
        string[] stripeIds = new string[2];

        long numberOfCents = Convert.ToInt32(request.RetailPrice * 100);

        var options = new ProductCreateOptions 
        { 
            Name = request.PromoName, 
            Description = request.PromoDescription, 
            DefaultPriceData = new() 
            { 
                Currency = "usd", 
                UnitAmount = numberOfCents
            }, 
        };
        var service = new ProductService();
        var result = service.Create(options);

        stripeIds[0] = result.Id;
        stripeIds[1] = result.DefaultPriceId;

        return stripeIds;
    }

    public bool ValidateStripeId(string id)
    {
        var service = new ProductService();
        var result = service.Get(id);

        if (result is null)
        {
            return false;
        }
        return true;
    }

    public async Task<string> CreateRefund(UserVoucherDTO userVoucher)
    {
        if (userVoucher.Charge_Id == "")
        {
            throw new Exception("No payment intent was found");
        }
        if (userVoucher.Is_Used == true)
        {
            throw new Exception("Voucher has already been used");
        }
        if (userVoucher.Times_Claimed != 0)
        {
            if (userVoucher.Times_Claimed >= userVoucher.Total_Reclaimable)
            {
                throw new Exception("Voucher has already been used");
            }
        }

        var options = new RefundCreateOptions { PaymentIntent = userVoucher.Charge_Id };
        var service = new RefundService();
        var refund = await service.CreateAsync(options);

        if (refund.Status == "succeeded")
        {
            return refund.Status;
        }
        if (refund.Status == "canceled")
        {
            throw new Exception("Refund was canceled");
        }
        if (refund.Status == "failed")
        {
            throw new Exception("Refund failed... give it a second and try again");
        }
        else
        {
            throw new Exception($"Refund has status of {refund.Status}");
        }
    }
}
