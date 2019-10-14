using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Security
{
	public class PrivilegeRequirement : IAuthorizationRequirement
	{
		public PrivilegeRequirement(string privilegeName)
		{
			var name = privilegeName ?? throw new ArgumentNullException(nameof(privilegeName));
			Privilege = (Privilege)Enum.Parse(typeof(Privilege), name);
		}

		public Privilege Privilege { get; }
	}
}
