using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DemoAuthenticationClaims01.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Secret()
        {
            return View();
        }

        public IActionResult Authenticate()
        {
            var myClaims1 = new List<Claim>()
            {
                   new Claim(ClaimTypes.Name, "Maria"),
                   new Claim(ClaimTypes.Email, "maria@email.com")

            };

            var myClaims2 = new List<Claim>()
            {
                   new Claim(ClaimTypes.Name, "Joao"),
                   new Claim(ClaimTypes.Email, "joao@email.com")

            };

            var myIdentity1 = new ClaimsIdentity(myClaims1, "My Identity 1");
            var myIdentity2 = new ClaimsIdentity(myClaims2, "My Identity 2");

            var userPrincipal = new ClaimsPrincipal(new[] { myIdentity1, myIdentity2 });

            HttpContext.SignInAsync(userPrincipal);

            return RedirectToAction("Index");
        }

    }
}
