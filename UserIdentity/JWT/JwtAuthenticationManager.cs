﻿using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace UserIdentity.JWT
{
    public class JwtAuthenticationManager : IJwtAuthenticationManager
    {
        private readonly IDictionary<string, string> users = new Dictionary<string, string> { { "test1", "password1" }, { "test2", "password1" } };
        private readonly string _key;
        public JwtAuthenticationManager(string key)
        {
            this._key = key;
        }

        public string Authenticate(string username, string password)
        {
            if(users.Any(u=>u.Key == username && u.Value == password))
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(_key);
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, username)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
