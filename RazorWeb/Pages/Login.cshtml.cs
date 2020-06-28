using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Piranha;

namespace RazorWeb.Pages
{
    public class LoginPageModel : Microsoft.AspNetCore.Mvc.RazorPages.PageModel
    {
        private readonly ISecurity _security;

        [BindProperty]
        public string Username { get; set; }
        [BindProperty]
        public string Password { get; set; }
        [BindProperty]
        public string ReturnUrl { get; set; }

        public LoginPageModel(ISecurity security) : base()
        {
            _security = security;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid && await _security.SignIn(HttpContext, Username, Password))
            {
                if (!string.IsNullOrEmpty(ReturnUrl))
                {
                    return new RedirectResult(ReturnUrl);
                }
                return new RedirectResult("/");
            }
            return Page();
        }
    }
}