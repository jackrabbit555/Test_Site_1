using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Metadata;
using Test_Site_1.Application.Services.Users.Commands.RgegisterUser;
using Test_Site_1.Application.Services.Users.Commands.UserLogin;
using Test_Site.Models.ViewModels.AuthenticationViewModel;
using Test_Site_1.Common.Dto;
using System.Text.RegularExpressions;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Principal;
using Microsoft.Identity.Client;



namespace Test_Site.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IRegisterUserService _registerUserService;
        private readonly IUserLoginService _userLoginService;


        public  AuthenticationController(IRegisterUserService registerUserService, IUserLoginService userLoginService) 
        {
            _registerUserService = registerUserService;
            _userLoginService = userLoginService;
        }



        [HttpGet]
        public ActionResult SignUp() 
        { 
            return View();
        }
        [HttpPost]
        public ActionResult Signup(SignupViewModel request)
        {
            if (string.IsNullOrWhiteSpace(request.FullName) ||
                string.IsNullOrWhiteSpace(request.Email) ||
                string.IsNullOrWhiteSpace(request.Password) ||
                string.IsNullOrWhiteSpace(request.RePassword))
            {
                return Json(new ResultDto { IsSuccess = false, Message = "لطفا تمامی موارد رو ارسال نمایید" });
            }
            if (User.Identity.IsAuthenticated == true)
            {
                return Json(new ResultDto { IsSuccess = false, Message = "شما به حساب کاربری خود وارد شده اید! و در حال حاضر نمیتوانید ثبت نام مجدد نمایید" });
            }
            if (request.Password.Length < 8)
            {
                return Json(new ResultDto { IsSuccess = false, Message = "رمز عبور باید حداقل 8 کاراکتر باشد" });
            }
            string emailRegex = @"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[A-Z0-9.-]+\.[A-Z]{2,}$";

            var match = Regex.Match(request.Email, emailRegex, RegexOptions.IgnoreCase);
            if (!match.Success)
            {
                return Json(new ResultDto { IsSuccess = true, Message = "ایمیل خودرا به درستی وارد نمایید" });
            }

            var signupResult = _registerUserService.Execute(new RequestRegisterUserDto
            {
                Email = request.Email,
                FullName = request.FullName,
                Password = request.Password,
                RePasword = request.RePassword,
                roles = new List<RolesInRegisterUserDto>
                {
                new RolesInRegisterUserDto{Id = 3  },
                }
            });
            if (!signupResult.IsSuccess == true)
            {
                var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,signupResult.Data.UserId.ToString()),
                new Claim(ClaimTypes.Email, request.Email),
                new Claim(ClaimTypes.Name, request.FullName),
                new Claim(ClaimTypes.Role, "Customer"),
            };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var pricipal = new ClaimsPrincipal(identity);
                var properties = new AuthenticationProperties()
                {
                    IsPersistent = true
                };

                HttpContext.SignInAsync(pricipal, properties);
            }
            return Json(signupResult);

        }
        [HttpGet]
         public IActionResult Signin(string ReturnUrl = "/")
        {
                ViewBag.url  = ReturnUrl;
               return View();
        }

        [HttpPost]
        public IActionResult Signin(string Email, string Password, string url = "/")
        {
            var signupresult = _userLoginService.Execute(Email, Password);
            if (signupresult.IsSuccess == true)
            {
                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier,signupresult.Data.UserId.ToString()),
                    new Claim(ClaimTypes.Email, Email),
                    new Claim(ClaimTypes.Name , signupresult.Data.Name ),
                    new Claim(ClaimTypes.Role , signupresult.Data.Roles)
                };
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var pricipal = new ClaimsPrincipal(identity);
                var properties = new AuthenticationProperties()
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTime.Now.AddDays(5),
                };
                HttpContext.SignInAsync(pricipal, properties);
            }
            return Json(signupresult);
        }
        public IActionResult SignOut() 
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}
