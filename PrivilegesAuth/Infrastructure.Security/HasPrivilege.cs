using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.Security
{
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = false)]
	public class HasPrivilegeAttribute : AuthorizeAttribute
	{
		public HasPrivilegeAttribute(Privilege privilege) : base(privilege.ToString())
		{ }
	}

	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = false)]
	public class HasPrivilegesAttribute : AuthorizeAttribute
	{
		public HasPrivilegesAttribute(Privilege[] privileges) 
		{
			this.Policy = "Privileges:" + string.Join(",", privileges.Select(x => x.ToString()));
		}
	}
}
