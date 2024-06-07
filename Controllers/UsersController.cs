using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SignalRChat.Models;
using SignalRChat.Shared.Models.Dto;
using WebApi.Helpers;

namespace SignalRChat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly DataContext _context;

        public UsersController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet("{userId}")]
        public async Task<ActionResult<IEnumerable<UserDto>>> Getusers(string userId)
        {
            List<UserDto> userDtos = new List<UserDto>();
            var users = await _context.Users.Where(x=>x.Id != userId).ToListAsync();

            foreach (var user in users)
            {
                var userdto = new UserDto()
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    ConnectionId = user.ConnectionId,
                    IsOnline = user.IsOnline,
                    
                };

                userDtos.Add(userdto);
            }

            return Ok(userDtos);
        }



    }
}
