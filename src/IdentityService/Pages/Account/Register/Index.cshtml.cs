using System.Security.Claims;
using Duende.IdentityModel;
using IdentityService.Models;
using IdentityService.Pages.Account.Register;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IdentityService.Pages.Register
{
    [SecurityHeaders]
    [AllowAnonymous]
    public class Index(UserManager<ApplicationUser> userManager) : PageModel
    {
        [BindProperty] public RegisterViewModel Input { get; set; } = default!;
        [BindProperty] public bool RegisterSuccess { get; set; }

        public IActionResult OnGet(string returnUrl)
        {
            Input = new RegisterViewModel
            {
                ReturnUrl = returnUrl
            };
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (Input?.Button != "register") return Redirect("~/");

            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values
                    .SelectMany(v => v.Errors))
                {
                    Console.WriteLine(error.ErrorMessage);
                }
            }


            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = Input.Username,
                    Email = Input.Email,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(user, Input.Password!);

                if (result.Succeeded)
                {
                    await userManager.AddClaimsAsync(user, [
                        new Claim(JwtClaimTypes.Name, Input.FullName!),
                    ]);

                    RegisterSuccess = true;
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        Console.WriteLine($"Identity error: {error.Description}");
                    }
                }
            }

            return Page();
        }
    }
}
