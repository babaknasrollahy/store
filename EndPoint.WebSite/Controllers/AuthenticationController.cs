using EndPoint.WebSite.Models.ViewModels;
using EndPoint.WebSite.Units;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Store.Application.Interfaces.FacadPattern;
using Store.Application.Services.User.Commands.AddUserService;
using Store.Application.Services.User.Queries.LoginUserService;
using Store.Common;
using System.Net.Mail;
using System.Security.Claims;

namespace EndPoint.WebSite.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IUserFacadPattern _userFacadPattern;
        private readonly CookieManeger _cookie;
        public AuthenticationController(IUserFacadPattern userFacadPattern)
        {
            _userFacadPattern = userFacadPattern;
            _cookie = new CookieManeger();
        }

        [HttpGet]
        public IActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Signup(SingUpViewModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Name) ||
            string.IsNullOrWhiteSpace(model.Email) ||
                string.IsNullOrWhiteSpace(model.Password) ||
                string.IsNullOrWhiteSpace(model.RePassword))
            {
                return Json(new ResultDTO { isSuccess = false, Message = "لطفا تمامی موارد رو ارسال نمایید" });
            }

            if (User.Identity.IsAuthenticated == true)
            {
                return Json(new ResultDTO { isSuccess = false, Message = "شما به حساب کاربری خود وارد شده اید! و در حال حاضر نمیتوانید ثبت نام مجدد نمایید" });
            }
            if (model.Password != model.RePassword)
            {
                return Json(new ResultDTO { isSuccess = false, Message = "رمز عبور و تکرار آن برابر نیست" });
            }
            if (model.Password.Length < 8)
            {
                return Json(new ResultDTO { isSuccess = false, Message = "رمز عبور باید حداقل 8 کاراکتر باشد" });
            }
            if (!EmailIsValid(model.Email))
            {
                return Json(new ResultDTO { isSuccess = true, Message = "ایمیل خودرا به درستی وارد نمایید" });
            }


            var signeupResult = _userFacadPattern.AddUser.Execute(new RequestAddUserDTO()
            {
                Email = model.Email,
                FullName = model.Name,
                Password = model.Password,
                Roles = new List<RoleInAddUserDTO>
                {
                    new RoleInAddUserDTO()
                    {
                        Id = (int)RolesList.Customer
                    }
                }
            },
            _cookie.GetBrowserId(HttpContext)
            );

            if (signeupResult.isSuccess)
            {
                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier,signeupResult.Data.Id.ToString()),
                    new Claim(ClaimTypes.Email, model.Email),
                    new Claim(ClaimTypes.Name, model.Name),
                    new Claim(ClaimTypes.Role, "Customer"),
                };


                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                var properties = new AuthenticationProperties()
                {
                    IsPersistent = true
                };
                HttpContext.SignInAsync(principal, properties);

            }
            return Json(signeupResult);
        }
        private bool EmailIsValid(string emailaddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailaddress);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        [HttpGet]
        public IActionResult Signin(string ReturnUrl = "/")
        {
            ViewBag.url = ReturnUrl;
            return View();
        }

        [HttpPost]
        public IActionResult Signin(string Email, string Password, string url = "/")
        {
            var signIn = _userFacadPattern.LoginUser.Execute(new LoginRequestDTO()
            {
                Email = Email,
                Password = Password,
            }, _cookie.GetBrowserId(HttpContext));

            if (signIn.isSuccess)
            {
                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier,signIn.Data.Id.ToString()),
                    new Claim(ClaimTypes.Email, Email),
                    new Claim(ClaimTypes.Name, signIn.Data.Name),
                };
                foreach (var item in signIn.Data.Roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, item));
                }

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                var properties = new AuthenticationProperties()
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTime.Now.AddDays(5),
                };
                HttpContext.SignInAsync(principal, properties);

            }
            return Json(signIn);
        }


        public IActionResult Signout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Home");
        }
    }
}
