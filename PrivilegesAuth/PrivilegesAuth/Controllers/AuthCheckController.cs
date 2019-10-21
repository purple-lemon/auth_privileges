using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrivilegesAuth.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PrivilegesAuth.Controllers
{
    [Route("api/[controller]")]
    public class AuthCheckController : Controller
    {
        // GET: api/<controller>
        [HttpGet]
		[Authorize("Admin")]
        public string Get()
        {
			// code
			return "here we go";
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpPost]
		[HasPrivilege(Privilege.CanCreateProducts)]
		public ActionResult Post([FromBody]CreateProductModel model)
        {
			var newProductId = User.PerformInGroupScope(model.GroupId, 
				new Privilege[] { Privilege.CanCreateProducts }, () =>
			{
				// call to Business logic to create product
				int createdProductId = 12;
				return createdProductId;
			});
			return Created("", newProductId);
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
