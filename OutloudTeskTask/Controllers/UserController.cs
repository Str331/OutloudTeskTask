using Microsoft.AspNetCore.Mvc;
using Outloud.Domain.Entity;
using Outloud.Service.ServiceInterfaces;

namespace OutloudTeskTask.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _log;
        private readonly IUserService _userService;

        public UserController(IUserService userService,ILogger<UserController> log)
        {
            _userService = userService;
            _log = log;
        }

        public async Task<IActionResult> GetUser()
        {
            var response = await _userService.GetUsers();
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> DeleteUser(long id)
        {
            var response = await _userService.DeleteUser(id);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Save() => PartialView();

        [HttpPost]
        public async Task<IActionResult> Save(User model)
        {
            if (ModelState.IsValid)
            {
                var response = await _userService.Create(model);
                return BadRequest(new { errorMessage = response.Description });
            }
            else return RedirectToAction("Index", "Home");
        }
    }
}
