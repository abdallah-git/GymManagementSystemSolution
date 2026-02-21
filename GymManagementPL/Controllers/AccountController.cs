using GymManagmentBLL.Services.Interfaces;
using GymManagmentBLL.ViewModels.AccountViewModel;
using GymMangementDAL.Entities;
using Humanizer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Tasks;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Mono.TextTemplating;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using static System.Collections.Specialized.BitVector32;
using static System.Net.Mime.MediaTypeNames;

namespace GymManagementPL.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public AccountController(IAccountService accountService, SignInManager<ApplicationUser> signInManager )
        {
            _accountService = accountService;
            _signInManager = signInManager;
        }

        #region Login 


        public ActionResult Login()
        {
            return View();
        }




        [HttpPost]
        public ActionResult Login (AccouontViewModel accouontView)
        {
            if (!ModelState.IsValid) return View(accouontView);
            var User = _accountService.ValidateUser(accouontView);
            if (User is null)
            {
                ModelState.AddModelError("InvalidLogin", "Invalid Email Or Password");

                return View(accouontView);
            }

            var Result = _signInManager.PasswordSignInAsync(User, accouontView.Password, accouontView.RememberMe, false).Result;

            if (Result.IsNotAllowed)
                ModelState.AddModelError("InvalidLogin", "Your Account Not Allowed ");
            if (Result.IsLockedOut)
                ModelState.AddModelError("InvalidLogin", "Your Account is Locekd");
            if (Result.Succeeded)
                return RedirectToAction("Index", "Home");
            return View(accouontView); 




        }



        #endregion


        #region Logout

        public ActionResult Logout()
        {

            _signInManager.SignOutAsync().GetAwaiter().GetResult();

            return RedirectToAction(nameof(Login));
        }



        #endregion


        public ActionResult AccessDenied()
        {
            return View(); 
        }





    }
}







