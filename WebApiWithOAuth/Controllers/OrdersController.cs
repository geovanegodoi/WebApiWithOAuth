using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApiWithOAuth.BOs;
using WebApiWithOAuth.TOs;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiWithOAuth.Controllers
{
    [Route("api/[controller]")]
    public class OrdersController : Controller
    {
        private readonly OrderBO DefaultBO = new OrderBO();

        // GET: api/values
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(DefaultBO.GetAll());
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(DefaultBO.Get(id));
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        [HttpGet("{id}/users")]
        public IActionResult OrdersByUser([FromRoute] int id)
        {
            return Ok(DefaultBO.Get(id).User);
        }
    }
}
