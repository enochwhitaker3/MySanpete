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
    private readonly NavigationManager navManager;
    public StripeService(NavigationManager NavMan)
    {
        this.navManager = NavMan;
    }

    public StripeService()
    {
        this.navManager = null!;
    }
    public async Task Checkout(PurchaseVoucherRequest request)
    {
        var domain = "https://localhost:7059"; // TODO change when we post to azure
        var metadata = new Dictionary<string, string>();
        metadata.Add("VoucherId", request.VoucherId.ToString());
        metadata.Add("UserId", request!.UserId!);
        var options = new SessionCreateOptions
        {
            LineItems = new List<SessionLineItemOptions>
            {
                new()
                {
                    Price = request.PriceId,
                    Quantity = 1,
                },
            },
            Mode = "payment",
            SuccessUrl = domain + "/OrderComplete",
            CancelUrl = domain + "/OrderAbandoned",
            Metadata = metadata,
            ExpiresAt = DateTime.Now.AddMinutes(31)
        };
        try
        {

            var service = new SessionService();
            var session = await service.CreateAsync(options);
            navManager.NavigateTo(session.Url);
        }
        catch (Exception)
        {

        }
    }

    public async Task BundleCheckout(PurchaseBundleRequest request)
    {
        var domain = "https://localhost:7059"; // TODO change when we post to azure
        var metadata = new Dictionary<string, string>();
        metadata.Add("UserId", request.UserId!);
        metadata.Add("BundleId", request.BundleId!.ToString());

        var options = new SessionCreateOptions
        {
            LineItems = new List<SessionLineItemOptions>
        {
            new()
            {
                Price = request.PriceId,
                Quantity = 1,
            },
        },
            Mode = "payment",
            SuccessUrl = domain + "/OrderComplete?IsBundle=true",
            CancelUrl = domain + "/OrderAbandoned",
            Metadata = metadata,
            ExpiresAt = DateTime.Now.AddMinutes(31)
        };
        try
        {
            var service = new SessionService();
            var session = await service.CreateAsync(options);
            navManager.NavigateTo(session.Url, true);
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

    public string[] AddBundleToStripe(AddBundleRequest bundle)
    {
        string[] stripeIds = new string[2];

        long numberOfCents = Convert.ToInt32(bundle!.Vouchers!.Select(x => x.RetailPrice).Sum() * 100);

        var options = new ProductCreateOptions
        {
            Name = bundle.Name,
            Description = bundle!.Vouchers!.Select(x => x.PromoDescription).FirstOrDefault(),
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
}
