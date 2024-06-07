using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using SignalRChat.Models;
using SignalRChat.Models.Dto;

namespace SignalRChat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        UserManager<ApplicationUser> userManager;
        public LoginController(UserManager<ApplicationUser> userManager) {

            this.userManager = userManager;

        }

        [HttpPost]
        public async Task<ActionResult<LoginResponseDto>> LoginUser(LoginDto loginDto)
        {
            var user = await userManager.Users.Where(x => x.UserName == loginDto.UserName).FirstOrDefaultAsync();


            var result = await userManager.CheckPasswordAsync(user, loginDto.Password);

            if (result == false)
            {
                return BadRequest();

            }


            return new LoginResponseDto()
            {
                LoginUserId = user.Id,
                UserName = user.UserName,
            };
        }


    }
}
