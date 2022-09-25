using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project3.Data;
using Project3.Model;
using Stripe;
using Stripe.Checkout;


namespace Project3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly ILogger<OrdersController> _logger;
        private readonly IRepository _repo;

        public OrdersController(ILogger<OrdersController> logger, IRepository repo)
        {
            StripeConfiguration.ApiKey = System.Environment.GetEnvironmentVariable("StripeApiKey") ?? throw new ArgumentNullException(nameof(StripeConfiguration.ApiKey));
            _logger = logger;
            _repo = repo;
        }

        [HttpGet("/orders/{user_id}")]
        public async Task<ActionResult<List<Order>>> ListOrders([FromRoute]int user_id)
        {
            List<Order> orders;
            
            try
            {

                orders = await _repo.ListOrders(user_id);
                _logger.LogInformation($"Giving list of orders for user #{user_id} ...");
            }catch(Exception e)
            {
                // Minor error checking for now
                _logger.LogError(e, e.Message);
                return StatusCode(500);
            }

            return orders;
        }

        [HttpPost("/orders/{product_id}&{customer_id}")]
        public async Task<ActionResult> MakePurchase(int product_id, int customer_id)
        {
            try
            {
                await _repo.MakePurchase(customer_id);
                _logger.LogInformation($"Customer #{customer_id} purchased Product # ...");
                return StatusCode(201);
            }catch(Exception e)
            {
                // Minor error checking for now
                _logger.LogError(e, e.Message);
                return StatusCode(500);
            }
        }
        
        [HttpPost("/orders/checkout/stripe")]
        public async Task<ActionResult> CreateCheckoutSession([FromBody] List<Jewelry> jewelry)
        {
            Dictionary<int, int> jewelryQuantities = new Dictionary<int, int>();
            foreach (Jewelry jewelryItem in jewelry)
            {
                if (jewelryQuantities.ContainsKey(jewelryItem.id))
                {
                    jewelryQuantities[jewelryItem.id]++;
                }
                else
                {
                    jewelryQuantities.Add(jewelryItem.id, 1);
                }
            }
            
            ProductService productService = new ProductService();
            List<SessionLineItemOptions> lineItems = new List<SessionLineItemOptions>();
            foreach (KeyValuePair<int, int> entry in jewelryQuantities)
            {
                Product product = await productService.GetAsync(entry.Key.ToString());
                lineItems.Add
                (
                    new SessionLineItemOptions
                    {
                        Price = product.DefaultPriceId,
                        Quantity = entry.Value,
                    }
                );
            }

            SessionCreateOptions options = new SessionCreateOptions
            {
                LineItems = lineItems,
                Mode = "payment",
                SuccessUrl = "https://team7p2client.azurewebsites.net/",
                CancelUrl = "https://team7p2client.azurewebsites.net/",
            };
            
            SessionService sessionService = new SessionService();
            try
            {
                Session session = await sessionService.CreateAsync(options);
                _logger.LogInformation("success");
                return Ok(new CreateCheckoutSessionResponse
                {
                    SessionUrl = session.Url,
                });
            }
            catch (StripeException ex)
            {
                Console.WriteLine(ex.StripeError.Message);
                return StatusCode(400);
            }
            
        }
    }
    
    public class CreateCheckoutSessionResponse
    {
        public string SessionUrl { get; set; }
    }
}
