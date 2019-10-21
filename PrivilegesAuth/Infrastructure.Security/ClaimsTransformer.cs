using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Security
{
	public class ClaimsTransformer : IClaimsTransformation
	{

		// inject required services here
		public ClaimsTransformer()
		{
		}

		/// <summary>
		/// Packs claims to existing context
		/// </summary>
		/// <param name="principal"></param>
		/// <returns></returns>
		public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
		{
			var existingClaimsIdentity = (ClaimsIdentity)principal.Identity;
			var currentUserName = existingClaimsIdentity.Name;

			// Initialize a new list of claims for the new identity
			var claims = new List<Claim>(existingClaimsIdentity.Claims);
			var userId = 0;
			if (int.TryParse(currentUserName, out userId))
			{
				// here is logic where you select all "Actual" privileges and add it to user 
				var newClaim = new Claim("UserPrivileges", "CanDeleteUsers;CanCreateCategories");
				claims.Add(newClaim);
			}

			// Build and return the new principal
			var newClaimsIdentity = new ClaimsIdentity(claims, existingClaimsIdentity.AuthenticationType);
			return new ClaimsPrincipal(newClaimsIdentity);
		}
	}
}
