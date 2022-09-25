using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project3.Data;
using Project3.Model;

namespace Project3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TransactionController : ControllerBase
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly IRepository _repo;

        
        public TransactionController(ILogger<CustomerController> logger, IRepository repo)
        {
            _logger = logger;
            _repo = repo;
        }

        [HttpGet("/transactions/{CustomerID}")]
        public async Task<ActionResult<List<Item>>> ListCustomerTransactions(int CustomerID)
        {
            List<Item> result = new List<Item>();
            try
            {
                result = await _repo.ListCustomerTransaction(CustomerID);
                _logger.LogInformation("Retrieving Customer Transactions ... ");
            }catch(Exception e)
            {
                _logger.LogError(e, e.Message);
            }
            return result;
        }

        [HttpGet("/transactions")]
        public async Task<ActionResult<List<Jewelry_transaction>>> ListTransactions()
        {
            List<Jewelry_transaction> transactions = new List<Jewelry_transaction>();

            try
            {
                transactions = await _repo.ListTransactions();
                _logger.LogInformation("Retrieving Jewelry Transactions ...");

            }catch(Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(500);
            }
            return transactions;
        }

        [HttpPost("/transactions")]
        public async Task<ActionResult> AddTransaction(int CustomerID, int OrderID, int ItemID)
        {
            try
            {
                await _repo.AddTransaction(CustomerID, OrderID, ItemID);
                _logger.LogInformation("Adding a transaction ...");
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(500);
            }
        }

    }
}
