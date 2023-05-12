using JWTAuthenticationManager.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace JWTAuthenticationManager
{
    public class JWTTokenHander
    {
        public const string JWT_Security_Key = "DweFEfqEfweEFefwefwef33423sdfEDFWEf";
        private const int JWT_Token_Validity_Mins = 20;
        private readonly List<UserAccount> _userAccountsList;

        //public JWTTokenHander()
        //{
        //    _userAccountsList = new List<UserAccount>
        //    {
        //        new UserAccount { UserName = "seller", Password = "Seller@123", Role = "Seller" },
        //        new UserAccount { UserName = "buyer", Password = "Buyer@123", Role = "Buyer" }
        //    };
        //}

        public AuthenticationResponse? GenerateJWTToken(UserAccount userAccount)//(AuthenticationRequest authenticationRequest)
        {
            //if (string.IsNullOrWhiteSpace(authenticationRequest.UserName) || string.IsNullOrWhiteSpace(authenticationRequest.Password))
            //    return null;

            ///*Validation*/
            //var userAccount = _userAccountsList.Where(x => x.UserName == authenticationRequest.UserName && x.Password == authenticationRequest.Password).FirstOrDefault();
            if (userAccount == null) return null;

            var tokenExpiryTimeStamp = DateTime.Now.AddMinutes(JWT_Token_Validity_Mins);
            var tokenKey = Encoding.ASCII.GetBytes(JWT_Security_Key);
            var claimsIdentity = new ClaimsIdentity(new List<Claim>
            {
                //new Claim(JwtRegisteredClaimNames.Name, authenticationRequest.UserName),
                //new Claim(ClaimTypes.Role, userAccount.Role)

                new Claim(JwtRegisteredClaimNames.Name, userAccount.UserName)
            });
            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(tokenKey),
                SecurityAlgorithms.HmacSha256Signature
            );
            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Expires=tokenExpiryTimeStamp,
                SigningCredentials = signingCredentials
            };

            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            var token = jwtSecurityTokenHandler.WriteToken(securityToken);

            return new AuthenticationResponse
            {
                UserName = userAccount.UserName,
                ExpiresIn = (int)tokenExpiryTimeStamp.Subtract(DateTime.Now).TotalSeconds,
                JWTToken = token
            };
        }
    }
}
