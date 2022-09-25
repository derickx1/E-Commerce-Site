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
    public class StoreController : ControllerBase
    {
        private readonly ILogger<StoreController> _logger;
        private readonly IRepository _repo;

        public StoreController(ILogger<StoreController> logger, IRepository repo)
        {
            _logger = logger;
            _repo = repo;
        }

        [HttpGet("/store/{startrow}/{endrow}")]
        public async Task<ActionResult<List<Jewelry>>> GetJewelryList([FromRoute] int startrow, int endrow)
        {
            List<Jewelry> list = new List<Jewelry>();
            try
            {
                list = await _repo.ListJewelry(startrow, endrow);
                _logger.LogInformation("Retrieving Jewelry List ...");
            }catch(Exception e)
            {
                // Minor error checking for now
                _logger.LogError(e, e.Message);
                return StatusCode(500);
            }
            return list;
        }

        [HttpGet("/store/filter/{material}/{item_type}")]
        public async Task<ActionResult<List<Jewelry>>> GetJewelryList([FromRoute] string material, string item_type)
        {
            List<Jewelry> list = new List<Jewelry>();
            try
            {
                list = await _repo.ListFilteredJewelry(material, item_type);
                _logger.LogInformation("Sending Filtered Jewelry List...");
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(500);
            }
            return list;
        }
    }
}
