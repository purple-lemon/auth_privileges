using Microsoft.AspNetCore.Mvc;
using PrivilegesAuth.Models;
using PrivilegesAuth.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrivilegesAuth.Controllers
{
	[Route("api/[controller]")]
	public class AuthController : Controller
	{
		public AuthService AuthService { get; set; }
		public AuthController(AuthService service)
		{
			AuthService = service;
		}
		[HttpPost]
		public async Task<string> Login([FromBody] LoginModel model)
		{
			return await AuthService.Login(model.Email, model.Password);
		}
    }
}
