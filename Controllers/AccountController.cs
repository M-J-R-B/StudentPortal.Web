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
                if (string.IsNullOrWhiteSpace(model.Username) ||
        string.IsNullOrWhiteSpace(model.Password) ||
        string.IsNullOrWhiteSpace(model.ConfirmPassword))
                {
                    ModelState.AddModelError("", "All fields are required");
                    return View(model);
                }

                if (ModelState.IsValid)
                {
                    // Check if passwords match
                    if (model.Password != model.ConfirmPassword)
                    {
                        ModelState.AddModelError("ConfirmPassword", "Passwords do not match.");
                        return View(model);
                    }

                    // Check if username already exists
                    if (dbContext.UserAccounts.Any(u => u.Username == model.Username))
                    {
                        ViewBag.Message = "Username already exists.";
                        return View(model);
                    }

                    UserAccount user = new UserAccount
                    {
                        Username = model.Username,
                        Password = model.Password // Note: Ensure you're hashing this password!
                    };

                    try
                    {
                        dbContext.UserAccounts.Add(user);
                        dbContext.SaveChanges();
                        ViewBag.Message = $"{model.Username} successfully registered.";
                        return View();
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Message = "An error occurred while registering. Please try again.";
                    }
                }

                // If we got this far, something failed, redisplay form
                return View(model);
            }
            else
            {
                // If ModelState is invalid, we need to ensure we're not showing a success message
                ViewBag.Message = null;
            }

            return View(model);
           
        }
        
    }
}

