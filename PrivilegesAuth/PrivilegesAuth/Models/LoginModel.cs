using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrivilegesAuth.Models
{
    public class LoginModel
    {
		public string Email { get; set; }
		public string Password { get; set; }
		public LoginType Type { get; set; }
	}

	public enum LoginType
	{
		Simple
	}
}
