using Microsoft.AspNetCore.Mvc;
using Medictionary.Models;
using Medictionary.Services.Interfaces;
using System.Threading.Tasks;

namespace Medictionary.ViewComponents
{
    public class HomeViewComponent : ViewComponent
    {
        private readonly IUserService _userService;

        public HomeViewComponent(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userService.GetUserDetailsAsync(User.Identity.Name);
            return View("/Views/Shared/Components/Home.cshtml", user);
        }
    }
}