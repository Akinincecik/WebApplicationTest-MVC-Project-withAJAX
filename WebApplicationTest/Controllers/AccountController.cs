using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NETCore.Encrypt.Extensions;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Security.Claims;
using WebApplicationTest.Entities;
using WebApplicationTest.Helpers;
using WebApplicationTest.Models;

namespace WebApplicationTest.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly DataBaseContext _dataBaseContext;
        private readonly IConfiguration _configuration;
        private readonly IHasher _hasher;

        public AccountController(DataBaseContext dataBaseContext, IConfiguration configuration)
        {
            _dataBaseContext = dataBaseContext;
            _configuration = configuration;
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                string hashedPassword = _hasher.DoMD5HashedString(model.Password);

                User user = _dataBaseContext.Users.SingleOrDefault(x => x.Username.ToLower() == model.Username.ToLower() && x.Password == hashedPassword);

                if (user != null)
                {
                    if (user.Locked)
                    {
                        ModelState.AddModelError(nameof(model.Username), "User locked");
                        return View(model);
                    }

                    List<Claim> claims = new List<Claim>();
                    claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
                    claims.Add(new Claim(ClaimTypes.Name, user.FullName ?? string.Empty));
                    claims.Add(new Claim(ClaimTypes.Role, user.Role));
                    claims.Add(new Claim("Username", user.Username));

                    ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    ClaimsPrincipal principal = new ClaimsPrincipal(identity);

                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "user or password incorrect");
                }
            }

            return View(model);
        }

        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {

                if (_dataBaseContext.Users.Any(x => x.Username.ToLower() == model.Username.ToLower()))
                {
                    ModelState.AddModelError(nameof(model.Username), "User exist");
                    return View(model);
                }

                string hashedPassword = _hasher.DoMD5HashedString(model.Password);

                User user = new()
                {
                    Username = model.Username,
                    Password = hashedPassword
                };

                _dataBaseContext.Users.Add(user);
                int affectedRowCount = _dataBaseContext.SaveChanges();

                if (affectedRowCount == 0)
                {
                    ModelState.AddModelError("", "User can not be added.");
                }
                else
                {
                    return RedirectToAction(nameof(Login));
                }
            }

            return View(model);
        }

        public IActionResult Profile()
        {
            ProfileInfoLoader();

            return View();
        }

        private void ProfileInfoLoader()
        {
            Guid userId = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier));

            User user = _dataBaseContext.Users.SingleOrDefault(x => x.Id == userId);

            ViewData["FullName"] = user.FullName;
            ViewData["ProfileImage"] = user.ProfileChangeFileName;
        }

        public IActionResult ProfileChangeFullName([Required][StringLength(50)] string? fullname)
        {

            if (ModelState.IsValid)
            {
                Guid userId = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier));

                User user = _dataBaseContext.Users.SingleOrDefault(x => x.Id == userId);
                user.FullName = fullname;
                _dataBaseContext.SaveChanges();

                return RedirectToAction(nameof(Profile));
            }

            ProfileInfoLoader();
            return View("Profile");
        }


        public IActionResult ProfileChangePassword([Required][MinLength(6)][MaxLength(16)] string? password)
        {

            if (ModelState.IsValid)
            {
                Guid userId = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier));
                User user = _dataBaseContext.Users.SingleOrDefault(x => x.Id == userId);

                string hashedPassword = _hasher.DoMD5HashedString(password);

                user.Password = hashedPassword;
                _dataBaseContext.SaveChanges();

                ViewData["result"] = "PasswordChanged";

            }

            ProfileInfoLoader();
            return View("Profile");
        }

        public IActionResult ProfileChangeImage([Required] IFormFile file)
        {

            if (ModelState.IsValid)
            {
                Guid userId = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier));
                User user = _dataBaseContext.Users.SingleOrDefault(x => x.Id == userId);

                //string fileName = $"p_{userId}.png";
                string fileName = $"p_{userId}{file.ContentType.Split('/')[1]}";
                Stream stream = new FileStream($"wwwroot/uploads/{fileName}", FileMode.OpenOrCreate);

                file.CopyTo(stream);
                stream.Close();
                stream.Dispose();

                user.ProfileChangeFileName = fileName;
                _dataBaseContext.SaveChanges();

                //user.FullName = fullname;
                //_dataBaseContext.SaveChanges();

                //return RedirectToAction(nameof(Profile));
            }

            ProfileInfoLoader();
            return View("Profile");
        }

        public IActionResult Logout()
        {

            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction(nameof(Login));
        }

        
    }
}
