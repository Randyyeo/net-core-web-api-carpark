using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using carpark.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace carpark.Controllers
{
    public class HomeController : Controller
    {
        private readonly iJWTAuthenticationManager JWTAuthenticationManager;
        private readonly ILogger<HomeController> _logger;

        

        public HomeController(ILogger<HomeController> logger, iJWTAuthenticationManager JWTAuthenticationManager)
        {
            _logger = logger;
            this.JWTAuthenticationManager = JWTAuthenticationManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        
        public IActionResult Secured()
        {
            return View();
        }

        [HttpGet("login")]
        public IActionResult Login(string returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost("login")]
        public IActionResult Validate(string username, string password, string returnUrl)
        {
            /* if (username=="bob" && password == "pizza")
            {
                var claims = new List<Claim>();
                claims.Add(new Claim("username", username));
                claims.Add(new Claim(ClaimTypes.NameIdentifier, username));
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                await HttpContext.SignInAsync(claimsPrincipal);
                return Redirect(returnUrl);
            }
            return BadRequest(); */
            var token = JWTAuthenticationManager.Authenticate(username, password);
            if (token == null)
                return BadRequest();
            return Redirect("/Home/Secured");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
