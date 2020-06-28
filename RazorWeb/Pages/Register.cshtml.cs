using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Piranha;
using Piranha.AspNetCore.Identity.Data;

namespace RazorWeb.Pages
{
    public class RegisterPageModel : Microsoft.AspNetCore.Mvc.RazorPages.PageModel
    {
        public enum PasswordResult
        {
            None,
            PasswordEmpty,
            PasswordError
        }

        private readonly ISecurity _security;
        private readonly UserManager<User> _userManager;

        public IdentityResult Result { get; set; } = null;
        public PasswordResult PasswordError { get; set; }

        [BindProperty]
        public string Username { get; set; }
        [BindProperty]
        public string Firstname { get; set; }
        [BindProperty]
        public string Lastname { get; set; }
        [BindProperty]
        public string Email { get; set; }
        [BindProperty]
        public string Password { get; set; }
        [BindProperty]
        public string PasswordConfirm { get; set; }

        public RegisterPageModel(ISecurity security, UserManager<User> userManager) : base()
        {
            _security = security;
            _userManager = userManager;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // Password validation
            if (string.IsNullOrWhiteSpace(Password))
            {
                PasswordError = PasswordResult.PasswordEmpty;
                return Page();
            }
            if (Password != PasswordConfirm)
            {
                PasswordError = PasswordResult.PasswordError;
                return Page();
            }

            var user = new User
            {
                Id = Guid.NewGuid(),
                UserName = Username,
                Email = Email,
                EmailConfirmed = true,
            };

            // Create the user
            Result = await _userManager.CreateAsync(user, Password);

            if (Result.Succeeded)
            {
                // Add the "WebUser" role
                await _userManager.AddToRoleAsync(user, "WebUser");
            }
            return Page();
        }
    }
}