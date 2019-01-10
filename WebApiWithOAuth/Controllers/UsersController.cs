using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiWithOAuth.BOs;
using WebApiWithOAuth.Security;
using WebApiWithOAuth.TOs;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiWithOAuth.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly UserBO DefaultBO = new UserBO();

        // GET: api/values
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Roles.ROLE_API_NORMAL)]
        public IEnumerable<UserTO> Get()
        {
            return DefaultBO.GetAll();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Roles.ROLE_API_NORMAL)]
        public UserTO Get([FromRoute] int id)
        {
            return DefaultBO.Get(id);
        }

        // POST api/values
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Roles.ROLE_API_ADMIN)]
        public void Post([FromBody]string value)
        {
        }

        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Roles.ROLE_API_ADMIN)]
        public void Delete([FromRoute] int id)
        {

        }

        [HttpGet("{id}/orders")]
        public IEnumerable<OrderTO> OrdersByUser([FromRoute] int id)
        {
            return DefaultBO.Get(id).Orders;
        }
    }
}
