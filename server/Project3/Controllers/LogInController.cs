using Basic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Project3.Data;
using Project3.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Project3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogInController : ControllerBase
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly IRepository _repo;

        public LogInController(ILogger<CustomerController> logger, IRepository repo)
        {
            _logger = logger;
            _repo = repo;
        }

        [HttpGet("/login")]
        public async Task<ActionResult<Dictionary<string, string>>> LogIn()
        {
            Customer customer;
            try
            {
                string Info = Request.Headers.Authorization;
                string EString = Info.Split(' ')[1];

                byte[] data = Convert.FromBase64String(EString);
                string DString = Encoding.UTF8.GetString(data);

                string[] cred = DString.Split(':');

                customer = await _repo.LogInCustomer(cred[0], cred[1]);

                if (customer.id != 0) {
                    var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, $"{customer.id}")
                    };

                    var secretBytes = Encoding.UTF8.GetBytes(Constants.Secret);
                    var key = new SymmetricSecurityKey(secretBytes);
                    var algorithm = SecurityAlgorithms.HmacSha256;

                    var signingCredentials = new SigningCredentials(key, algorithm);
                    var token = new JwtSecurityToken(
                        Constants.Issuer,
                        Constants.Audience,
                        claims,
                        DateTime.Now,
                        // For now, the token will last for a day. Once refresh tokens are included, this will be shorten down.
                        DateTime.Now.AddDays(1),
                        signingCredentials
                        );
                    var tokenJson = new JwtSecurityTokenHandler().WriteToken(token);
                    Dictionary<string, string> response = new Dictionary<string, string>();
                    response.Add("CustomerID", $"{customer.id}");
                    response.Add("Access-Token", tokenJson);
                    response.Add("CustomerName", $"{customer.name}");
                    response.Add("CustomerAddress", $"{customer.shipping_address}");
                    

                    return response;
                }
                else
                {
                    _logger.LogError("Failed sign-in attempt ...");
                    return StatusCode(401);
                }

            }catch(Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(500);
            }
           
        }

        [HttpPost("/login")]
        public async Task<ActionResult<Dictionary<string, string>>> Register()
        {
            Customer customer;
            try
            {
                string Info = Request.Headers.Authorization;
                string EString = Info.Split(' ')[1];

                byte[] data = Convert.FromBase64String(EString);
                string DString = Encoding.UTF8.GetString(data);

                string[] cred = DString.Split(':');

                using StreamReader reader = new StreamReader(Request.Body);
                string json = await reader.ReadToEndAsync();

                JsonObject person = (JsonObject)JsonSerializer.Deserialize(json, typeof(JsonObject));
                customer = await _repo.RegisterCustomer(person["name"].ToString(), person["address"].ToString(), cred[0], cred[1]);
                var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, $"{customer.id}")
                    };

                var secretBytes = Encoding.UTF8.GetBytes(Constants.Secret);
                var key = new SymmetricSecurityKey(secretBytes);
                var algorithm = SecurityAlgorithms.HmacSha256;

                var signingCredentials = new SigningCredentials(key, algorithm);
                var token = new JwtSecurityToken(
                    Constants.Issuer,
                    Constants.Audience,
                    claims,
                    DateTime.Now,
                    // For now, the token will last for a day. Once refresh tokens are included, this will be shorten down.
                    DateTime.Now.AddDays(1),
                    signingCredentials
                    );
                var tokenJson = new JwtSecurityTokenHandler().WriteToken(token);
                Dictionary<string, string> response = new Dictionary<string, string>();
                response.Add("CustomerID", $"{customer.id}");
                response.Add("Access-Token", tokenJson);
                response.Add("CustomerName", $"{customer.name}");
                response.Add("CustomerAddress", $"{customer.shipping_address}");


                return response;

            }
            catch(Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(500);
            }
        }

    }
}
