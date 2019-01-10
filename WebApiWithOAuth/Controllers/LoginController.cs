using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WebApiWithOAuth.Security;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiWithOAuth.Controllers
{
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        [AllowAnonymous]
        [HttpPost]
        public IActionResult DoLogin([FromServices] AuthenticationManager manager,
                                     [FromBody] UserCredentials user)
        {
            IActionResult response = Unauthorized();

            if (manager.ValidateCredentials(user))
            {
                response = Ok(manager.GenerateToken(user));
            }
            return response;
        }
    }
}
