using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore.Metadata;
using RazorClassLibrary.Data;
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
        this.NavMan = null;
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
        catch (Exception ex)
        {

        }
    }

    public string[] AddProductToStripe(AddVoucherRequest request)
    {
        string[] stripeIds = new string[2];

        long numberOfCents = Convert.ToInt32(request.RetailPrice * 100);

        var options = new ProductCreateOptions { Name = request.PromoName, Description = request.PromoDescription, DefaultPriceData = new() { Currency = "usd", UnitAmount = numberOfCents } };
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
}
