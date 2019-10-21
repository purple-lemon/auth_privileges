using Infrastructure.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PrivilegesAuth.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PrivilegesAuth.Utils
{
	public class AuthService
	{
		public IConfiguration Config;
		public AuthService(IConfiguration config)
		{
			Config = config;
		}

		public async Task<string> Login(string email, string password)
		{
			return await Task.FromResult(LoginSimpleUser(email));
		}
		public string LoginSimpleUser(string email)
		{
			return StringifyToken(BuildToken("Some user", email, "User", null));
		}
		public async Task<string> Login(LoginModel model)
		{
			JwtSecurityToken token = null;
			switch (model.Type)
			{
				case LoginType.CanDelete:
					token = BuildToken("Some user", model.Email, "User", new List<Privilege> { Privilege.CanDeleteData });
					break;
				default:
					token = BuildToken("Some user", model.Email, "User", null);
				break;
			}

			return await Task.FromResult(StringifyToken(token));
		}


		public string StringifyToken(JwtSecurityToken token)
		{
			return new JwtSecurityTokenHandler().WriteToken(token);
		}

		private JwtSecurityToken BuildToken(string userName, string email, string role, IEnumerable<Privilege> privileges)
		{
			var claims = new[] {
				new Claim(ClaimTypes.Role, role),
				new Claim(JwtRegisteredClaimNames.Email, email),
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
				new Claim(ClaimTypes.Name, userName),
				new Claim(ClaimTypes.NameIdentifier, userName),
				new Claim(JwtRegisteredClaimNames.GivenName, userName),
				new Claim(PrivilegeConstants.PRIVILEGES_CLAIM_NAME, PrivilegesPacker.Pack(privileges))
			};

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Config["Jwt:Key"]));
			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

			var token = new JwtSecurityToken(Config["Jwt:Issuer"],
			  Config["Jwt:Issuer"],
			  claims,
			  expires: DateTime.UtcNow.AddDays(1),
			  signingCredentials: creds);

			return token;
		}
	}
}
