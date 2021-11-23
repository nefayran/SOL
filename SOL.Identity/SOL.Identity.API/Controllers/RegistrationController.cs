using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SOL.Identity.SOL.Application.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SOL.Identity.Controllers
{
    [ApiController]
    [Route("/api/v1/")]
    public class RegistrationController : ControllerBase
    {
        private readonly ILogger<RegistrationController> _logger;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public RegistrationController(ILogger<RegistrationController> logger, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        [Route("test")]
        public ActionResult Get()
        {
            _logger.LogInformation("Hello, World!");
            return Ok();
        }
        [HttpPost]
        [Route("registration")]
        public async Task<IActionResult> Register()
        {
            _logger.LogInformation("Request registration");
            if (ModelState.IsValid)
            {
                User user = new() { Email = "nefayran@gmail.com", UserName = "nefayran@gmail.com", Year = 1992 };
                // добавляем пользователя
                var result = await _userManager.CreateAsync(user, "0071221qQ#");
                if (result.Succeeded)
                {
                    // установка куки
                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return BadRequest();
        }
    }
}
