﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SOL.Identity.SOL.Identity.Application.Interfaces;
using SOL.Identity.SOL.Identity.Domain.Entities;

namespace SOL.Identity.SOL.Identity.Infrastructure.Security
{
	public class JwtGenerator : IJwtGenerator
	{
		private readonly SymmetricSecurityKey _key;

		public JwtGenerator(IConfiguration config)
		{
			_key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
		}

		public string CreateToken(User user)
		{
			var claims = new List<Claim> { new Claim(JwtRegisteredClaimNames.NameId, user.UserName) };

			var credentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(claims),
				Expires = DateTime.Now.AddMinutes(5), // DateTime.Now.AddDays(7),
				SigningCredentials = credentials
			};
			var tokenHandler = new JwtSecurityTokenHandler();

			var token = tokenHandler.CreateToken(tokenDescriptor);

			return tokenHandler.WriteToken(token);
		}
	}
}
