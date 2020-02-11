using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ToDoApp.UI.Pages
{
    [AllowAnonymous]
    public class LogoutModel : PageModel
    {
        public string ReturnUrl { get; set; }
        public async Task<IActionResult> 
            OnGetAsync(string returnUrl)
        {
            
            try
            {
                // Clear the existing external cookie
                await HttpContext
                    .SignOutAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme);
                // Clear the existing external cookie
                await HttpContext
                    .SignOutAsync(
                        OpenIdConnectDefaults.AuthenticationScheme);
            }
            catch { }
           
           

            return null;
            //return LocalRedirect(returnUrl);

        }
    }
}
