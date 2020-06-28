using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Piranha;

namespace RazorWeb.Pages
{
    public class LogoutPageModel : Microsoft.AspNetCore.Mvc.RazorPages.PageModel
    {
        private readonly ISecurity _security;

        public LogoutPageModel(ISecurity security) : base()
        {
            _security = security;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            await _security.SignOut(HttpContext);

            return new RedirectResult("/");
        }
    }
}