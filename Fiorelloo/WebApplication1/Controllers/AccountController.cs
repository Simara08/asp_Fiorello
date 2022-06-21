using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;
using WebApplication1.ViewModels;

namespace WebApplication1.Controllers
{
    public class AccountController : Controller
    {

        private UserManager<AppUser> _userManager { get; }
        private SignInManager<AppUser> _signInManager { get; }
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public async Task<IActionResult> Register(RegisterVM user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }
            AppUser newuser = new AppUser
            {
                Fullname = user.Fullname,
                UserName = user.Username,
                Email=user.Email
            };
            var IdentityResult = await _userManager.CreateAsync(newuser, user.Password);
            if (!IdentityResult.Succeeded)
            {
                foreach (var error in IdentityResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(user);
            }
            await _signInManager.SignInAsync(newuser, true);
            return RedirectToAction("Index", "Home");
        }
        public async  Task<IActionResult> Logout()
        {
           await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> SignIn(SignInVM user)
        {
            AppUser userDb = await _userManager.FindByEmailAsync(user.Email);
            if (userDb==null)
            {
                ModelState.AddModelError("", "Email or password is wrong");
                return View(user);
            }
            var signInResult = await _signInManager.PasswordSignInAsync(user.Email,
                                    user.Password, user.isParsistent, lockoutOnFailure: true);
            if (signInResult.IsLockedOut)
            {
                ModelState.AddModelError("", "Please try a few minutes later");
                return View(user);
            }
            if (!signInResult.Succeeded)
            {
                ModelState.AddModelError("", "Email or password is wrong");
                return View(user);
            }
            if (!userDb.IsActivated)
            {
                ModelState.AddModelError("", "Please verify your account");
                return View(user);
            }
            return RedirectToAction("Index", "Home");

        }
    }
}
