using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace Fishery.Areas.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index(LoginModel model)
        {
            if (User.Identity.IsAuthenticated) return RedirectToAction(actionName: "Index", controllerName: "Food");

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Account == "admin" && model.Password == "admin")
                {
                    var claims = new List<Claim>();

                    claims.Add(new Claim(ClaimTypes.NameIdentifier, model.Account));
                    claims.Add(new Claim(ClaimTypes.Name, "管理者"));

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(
                            new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.NameIdentifier, ClaimTypes.Role)));
                    return RedirectToAction(actionName: "Index", controllerName: "Food");
                }
                else
                {
                    ModelState.AddModelError("Account", "帳號不存在或密碼不正確");
                    return View("~/Areas/Admin/Views/User/Index.cshtml", model);
                }

            }
            else
            {
                return View("~/Areas/Admin/Views/User/Index.cshtml", model);
            }
        }

        public class LoginModel
        {
            [Required(ErrorMessage = "{0}必填")]
            [Display(Name = "帳號")]
            public string Account { get; set; }
            [Required(ErrorMessage = "{0}必填")]
            [Display(Name = "密碼")]
            public string Password { get; set; }
        }
    }
}
