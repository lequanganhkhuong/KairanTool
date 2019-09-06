using System.Security.Claims;
using System.Threading.Tasks;
using KairanTool.Data.Repositories;
using KairanTool.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KairanTool.Controllers
{
   
    public class LoginController : Controller
    {
        private readonly UnitOfWork _unitofwork;

        public LoginController(UnitOfWork context)
        {
            _unitofwork = context;
        }
        // GET
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
         
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                ViewBag.Message = "Username or password cannot be empty";
                return RedirectToAction("Login");
            }

            var emp = _unitofwork.UserRepository
                .Where(x => x.Username.Equals(username) && x.Password.Equals(password)).Include(x => x.Role)
                .FirstOrDefault();
            if (emp == null)
            {
                ViewBag.Message = "Wrong username or password";
                return RedirectToAction("Login");
            }
            else if (!emp.IsActive)
            {
                ViewBag.Message = "Wrong username or password";
                return RedirectToAction("Login");
            }
            else
            {
              

                var identity = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, emp.Id.ToString()),
                    new Claim("Username", emp.Name),
                    new Claim(ClaimTypes.Role, emp.Role.Name),
                }, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme, principal);
                if (emp.RoleId == 3)
                {
                    return RedirectToAction("Confirm", "Home");
                }
                return RedirectToAction("Index", "Home");
                
                

            }
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}