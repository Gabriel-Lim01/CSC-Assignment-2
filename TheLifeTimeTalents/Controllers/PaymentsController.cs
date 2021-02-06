using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Stripe;
using Stripe.Checkout;
using TheLifeTimeTalents.Models;

namespace TheLifeTimeTalents.Controllers
{
    public class PaymentsController : Controller
    {
        private readonly IStripeClient client;

        [HttpPost("create-checkout-session")]
        public async Task<IActionResult> CreateCheckoutSession([FromBody] CreateCheckoutSessionRequest req)
        {
            // Set your secret key. Remember to switch to your live secret key in production!
            // See your keys here: https://dashboard.stripe.com/account/apikeys
            StripeConfiguration.ApiKey = "sk_test_51IGePsJJ9St9PCyi89pKljE6RXy8YU0EGYNd6kNH87EOtKSUOZO7FN2YFgFMxZoq0QdfyQ8HRsY5sLVwDJi9Tv7g00yWj8gs3c";

            var options = new SessionCreateOptions
            {
                // See https://stripe.com/docs/api/checkout/sessions/create
                // for additional parameters to pass.
                // {CHECKOUT_SESSION_ID} is a string literal; do not change it!
                // the actual Session ID is returned in the query parameter when your customer
                // is redirected to the success page.
                SuccessUrl = "https://example.com/success.html?session_id={CHECKOUT_SESSION_ID}",
                CancelUrl = "https://example.com/canceled.html",
                PaymentMethodTypes = new List<string>
        {
            "card",
        },
                Mode = "subscription",
                LineItems = new List<SessionLineItemOptions>
        {
            new SessionLineItemOptions
            {
                Price = req.PriceId,
                // For metered billing, do not pass quantity
                Quantity = 1,
            },
        },
            };
            var service = new SessionService(this.client);
            try
            {
                var session = await service.CreateAsync(options);
                return Ok(new CreateCheckoutSessionResponse
                {
                    SessionId = session.Id,
                });
            }
            catch (StripeException e)
            {
                Console.WriteLine(e.StripeError.Message);
                return BadRequest(new ErrorResponse
                {
                    ErrorMessage = new ErrorMessage
                    {
                        Message = e.StripeError.Message,
                    }
                });
            }
        }
    }