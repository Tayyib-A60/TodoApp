using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Server.Data.Entities;
using Server.Data.Interfaces;
using Server.Dtos;

namespace Server.Controllers
{
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserRepository _userRepo;

        public UserController(ILogger<UserController> logger, IUserRepository userRepo)
        {
            _logger = logger;
            _userRepo = userRepo;
        }

        [HttpPost("create")]
        public IActionResult CreateUser([FromBody] UserDTO userDTO) 
        {
            var  newUser = new UserEntity
            {
                ID = Guid.NewGuid(),
                Email = userDTO.email, 
                Password = userDTO.password, 
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                Todos = new List<TodoEntity>()
            };

            _userRepo.CreateUser(newUser);

            return Ok(newUser);
        }
    }
}