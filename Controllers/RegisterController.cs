using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SignalRChat.Models;
using SignalRChat.Models.Dto;

namespace SignalRChat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private UserManager<ApplicationUser> userManager;
        public RegisterController(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        [HttpPost]
        public async Task<ActionResult<RegisterResponseDto>> RegisterUser(RegisterDto registerDto)
        {

            var user = userManager.Users?.Where(x => x.UserName == registerDto.UserName).FirstOrDefault();

            if(user != null)
            {
                return BadRequest();
            }

            user = new ApplicationUser()
            {
                UserName = registerDto.UserName,
                Email = registerDto.Email,  
                NormalizedUserName = registerDto.UserName
                
            };

            var result = await userManager.CreateAsync(user, registerDto.Password);

            if(!result.Succeeded)
            {
                return BadRequest();
            }


            return new RegisterResponseDto()
            {
                LoginUserId = user.Id,
                UserName = user.UserName,
            };
        }
    }
}
