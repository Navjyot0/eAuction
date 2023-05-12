using AuthenticationWebApi.Models;
using AuthenticationWebApi.Repository;
using JWTAuthenticationManager;
using JWTAuthenticationManager.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AuthenticationWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : Controller
    {
        public UserManager<ApplicationUser> _userManager;
        private readonly JWTTokenHander _jWTTokenHander;
        private readonly IUnitOfWork _userDb;

        public AccountController(UserManager<ApplicationUser> userManager, JWTTokenHander jWTTokenHander, IUnitOfWork userDb)
        {
            this._userManager = userManager;
            this._jWTTokenHander = jWTTokenHander;
            this._userDb = userDb;
        }

        // POST api/<UserController>
        //[AllowAnonymous]
        [HttpPost]
        [Route("CreateUser")]
        public async Task<ActionResult> CreateUser(User user)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser applicationUser = new ApplicationUser()
                {
                    UserName = user.Email,
                    Email = user.Email
                };
                this._userDb.user.Add(user);
                IdentityResult result = await _userManager.CreateAsync(applicationUser, user.Password);
                if (result.Succeeded)
                    return StatusCode(StatusCodes.Status201Created);
                return StatusCode(StatusCodes.Status400BadRequest, result.Errors);
            }
            return StatusCode(StatusCodes.Status400BadRequest);
        }



        [HttpPost]
        public ActionResult<AuthenticationResponse> Authenticate(AuthenticationRequest authenticationRequest)
        {
            var account = _userManager.Users.Where(u => u.UserName == authenticationRequest.UserName).FirstOrDefault();
            var PasswordHasher = new PasswordHasher<ApplicationUser>();
            var checkPwd = PasswordHasher.VerifyHashedPassword(account, account.PasswordHash, authenticationRequest.Password);
            
            if (checkPwd == PasswordVerificationResult.Failed) return Unauthorized();
            
            UserAccount userAccount = account != null ? new UserAccount() { UserName = account?.UserName, Role = "" } : null;
            var authenticationResponse = _jWTTokenHander.GenerateJWTToken(userAccount);
            if (authenticationResponse == null) return Unauthorized();

            var response = HttpContext.Response;
            response.Cookies.Delete("jwtToken");
            response.Cookies.Append("jwtToken", authenticationResponse.JWTToken, new CookieOptions() { SameSite = SameSiteMode.None, Secure = true });
            response.Cookies.Delete("username");
            response.Cookies.Append("username", authenticationResponse.UserName, new CookieOptions() { SameSite = SameSiteMode.None, Secure = true });

            return authenticationResponse;
        }
    }
}
