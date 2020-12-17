using Identity.Context;
using Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class HomeController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public HomeController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Index()
        {
            return View(new UserSignInViewModel());
        }
        [HttpPost]
      
        public async Task<IActionResult> GirisYap(UserSignInViewModel model)
        {
            if (ModelState.IsValid)
            {
                var identityResult = await _signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, true);

                if (identityResult.IsLockedOut)
                {

                    var gelen = await _userManager.GetLockoutEndDateAsync(await _userManager.FindByNameAsync(model.Username));
                    var kisitlananSure = gelen.Value;
                    var kalanDakika =kisitlananSure.Minute - DateTime.Now.Minute;

                    ModelState.AddModelError("", $"3 kere yanlış şifre girdiğiniz için hesabınız kitlenmiştir {kalanDakika} dakika boyunca kiltlenmiştir");
                    return View("Index", model);
                }
                //if (identityResult.IsNotAllowed)
                //{
                //    ModelState.AddModelError("", "Lütfen Email adresinizi doğrulayın");
                //    return View("Index", model);
                //}

                if (identityResult.Succeeded)
                {
                    return RedirectToAction("Index", "Panel");
                }
                var yanlisGirilmeSayisi = await _userManager.GetAccessFailedCountAsync(await _userManager.FindByNameAsync(model.Username));
                ModelState.AddModelError("", $"Kullanıcı adı veya şifre hatalı {3 - yanlisGirilmeSayisi} kere yanlış girerseniz hesabınız bloklanacaktır");
            }
            return View("Index", model);
        }
        public IActionResult KayıtOl()
        {
            return View(new UserSignUpViewModel());
        }
        [HttpPost]
        public async Task<IActionResult> KayıtOl(UserSignUpViewModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser
                {
                    Email = model.Email,
                    Name = model.Name,
                    Surname = model.Surname,
                    UserName = model.UserName

                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
