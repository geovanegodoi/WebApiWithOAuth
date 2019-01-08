using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiWithOAuth.BOs;
using WebApiWithOAuth.TOs;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiWithOAuth.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly UserBO DefaultBO = new UserBO();

        // GET: api/values
        [HttpGet, Authorize]
        public IEnumerable<UserTO> Get()
        {
            return DefaultBO.GetAll();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public UserTO Get([FromRoute] int id)
        {
            return DefaultBO.Get(id);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        [HttpGet("{id}/orders")]
        public IEnumerable<OrderTO> OrdersByUser([FromRoute] int id)
        {
            return DefaultBO.Get(id).Orders;
        }
    }
}
