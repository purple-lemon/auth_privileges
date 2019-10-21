using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace Infrastructure.Security
{
    public static class UserExtensions
    {
		public static T PerformInGroupScope<T>(this ClaimsPrincipal principal, int groupid, 
			IEnumerable<Privilege> privileges, Func<T> action)
		{
			foreach (var p in privileges)
			{
				if(!HasGroupPrivilege(principal, groupid, p))
				{
					throw new InsufficientPrivilegeException($"User has no privilege {p}, which required to perform action");
				}
			}
			return action();
		}

		public static bool HasGroupPrivilege(this ClaimsPrincipal principal, int groupId, Privilege privilege)
		{
			// logic that unpack privileges and check one for existing
			return true;
		}

		public static bool HasPrivilege(this ClaimsPrincipal principal, string privilege)
		{
			var priv = (Privilege)Enum.Parse(typeof(Privilege), privilege);
			return HasPrivilege(principal, priv);
		}

		public static bool HasPrivilege(this ClaimsPrincipal principal, Privilege privilege)
		{
			var claim = principal.Claims.SingleOrDefault(c => c.Type == PrivilegeConstants.PRIVILEGES_CLAIM_NAME);
			if (claim == null) return false;

			return claim.Value.Unpack().Contains(privilege);
		}
	}
}
