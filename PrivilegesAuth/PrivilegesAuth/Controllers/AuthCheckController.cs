using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PrivilegesAuth.Controllers
{
    [Route("api/[controller]")]
    public class AuthCheckController : Controller
    {
        // GET: api/<controller>
        [HttpGet]
		[Authorize]
        public string Get()
        {
            return User.Identity.Name;
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        [HttpDelete("{id}")]
		[HasPrivilege(Privilege.CanDeleteData)]
        public void Delete(int id)
        {
			// implementation for delete data
        }
    }
}
