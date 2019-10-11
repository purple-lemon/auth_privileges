using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
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


		public string StringifyToken(JwtSecurityToken token)
		{
			return new JwtSecurityTokenHandler().WriteToken(token);
		}

		private JwtSecurityToken BuildToken(string userName, string email, string role, string[] privileges)
		{
			var claims = new[] {
				new Claim("role", role),
				new Claim(JwtRegisteredClaimNames.Email, email),
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
				new Claim(ClaimTypes.Name, userName),
				new Claim(ClaimTypes.NameIdentifier, userName),
				new Claim(JwtRegisteredClaimNames.GivenName, userName),
				//new Claim(PermissionConstants.PackedPermissionClaimType, InterviewService.Models.DBModels.Access.Permission.AssignInterview.ToString())
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
