using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Security
{
	public class PrivilegesHandler : AuthorizationHandler<PrivilegeRequirement>
	{
		protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, 
			PrivilegeRequirement requirement)
		{
			if (context.User.HasPrivilege(requirement.Privilege))
			{
				context.Succeed(requirement);
			}			

			return Task.CompletedTask;
		}
	}
}
