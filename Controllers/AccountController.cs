using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentPortal.Web.Data;
using StudentPortal.Web.Models;
using StudentPortal.Web.Models.Entities;
using System.Security.Claims;

namespace StudentPortal.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        public AccountController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public IActionResult LogIn(LogInViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = dbContext.UserAccounts.Where(u => u.Username == model.Username && u.Password == model.Password).FirstOrDefault();
                if (user != null)
                {
                    //return RedirectToAction("Index", "Home");
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.Username),
                        new Claim("Name", user.Username),
                        new Claim(ClaimTypes.Role, "User")

                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                    return RedirectToAction("SecurePage");
                }
                else
                {
                    ModelState.AddModelError("Username", "Invalid username or password.");
                    return View(model);
                }
            }
            return View(model);
        }

        public IActionResult LogOut()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("LogIn");
        }

        [Authorize]
        public IActionResult SecurePage()
        {
            ViewBag.Name = HttpContext.User.Identity.Name;
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegistrationViewModel model)
        {
            if (ModelState.IsValid)
            {
                UserAccount user = new UserAccount
                {
                    Username = model.Username,
                    Password = model.Password
                };
                //test
                try
                {
                    dbContext.UserAccounts.Add(user);
                    dbContext.SaveChanges();
                    ModelState.Clear();
                    ViewBag.Message = $"{model.Username} successfully registered.";
                    return View();
                }
                catch (DbUpdateException ex)
                {
                    // This will catch unique constraint violations
                    ModelState.AddModelError("", "Username already exists.");

                }
                catch (Exception ex)
                {
                    // This will catch any other unexpected errors
                    ModelState.AddModelError("", "An error occurred while registering. Please try again.");
                    // Log the exception here for debugging purposes
                    // logger.LogError(ex, "Error occurred while registering user");
                }
            }

            // If we get here, either ModelState was invalid or an exception was caught
            return View(model);
        }
    }
}

