using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ApiOne.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SecretController : ControllerBase
    {
        [HttpGet]
        [Authorize]
        public string GetSecret() 
        {
            var claims = User.Claims.ToList();
            return "secret message from Api One";
        }
    }
}
