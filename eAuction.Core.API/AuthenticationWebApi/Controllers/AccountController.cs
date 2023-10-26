using AuthenticationWebApi.Models;
using AuthenticationWebApi.Repository;
using JWTAuthenticationManager;
using JWTAuthenticationManager.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
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
            response.Cookies.Append("jwtToken", authenticationResponse.JWTToken, new CookieOptions() { SameSite = SameSiteMode.None, Secure = false, HttpOnly = false, IsEssential = true });
            response.Cookies.Delete("username");
            response.Cookies.Append("username", authenticationResponse.UserName, new CookieOptions() { SameSite = SameSiteMode.None, Secure = false, HttpOnly = false, IsEssential = true });

            return authenticationResponse;
        }

        [HttpPost]
        [Route("Logout")]
        public IActionResult Logout()
        {
            HttpContext.Response.Cookies.Delete("jwtToken");
            HttpContext.Response.Cookies.Delete("username");
            return SignOut();
        }

        [HttpPost]
        [Route("CreateRecipe")]
        public async Task<ActionResult> CreateRecipe()
        {
            try
            {
                var mongoUri = "mongodb+srv://user001:3BPrSrGqUHYs7K6t@cluster0.nrqja1p.mongodb.net/?authSource=admin";
                IMongoClient client1 = new MongoClient(mongoUri);
                IMongoCollection<Recipe> collection;

                var dbName = "myDatabase";
                var collectionName = "recipes";

                collection = client1.GetDatabase(dbName)
                   .GetCollection<Recipe>(collectionName);

                collection.InsertOne(new Recipe("elotes", new List<string>() { "corn", "mayonnaise", "cotija cheese", "sour cream", "lime" }, 35));
            }
            catch (Exception e)
            {
                Console.WriteLine($"Something went wrong trying to insert the new documents." +
                    $" Message: {e.Message}");
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            return StatusCode(StatusCodes.Status201Created);
        }
    }
}
