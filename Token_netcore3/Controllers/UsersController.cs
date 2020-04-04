using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Token_netcore3.Models;
using Token_netcore3.Services;

namespace Token_netcore3.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;
        /// <summary>
        /// đã đăng ký DI trong startup
        /// </summary>
        /// <param name="userService"></param>
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]AuthenticateModel model)
        {
            //eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjEiLCJuYmYiOjE1ODU5NjkyOTcsImV4cCI6MTU4NjU3NDA3NiwiaWF0IjoxNTg1OTY5Mjk3fQ.yos0gPOaGQEOsvSZeW6VZGqcDw3UL_CVRVgFfsT2F94
            //eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjEiLCJuYmYiOjE1ODU5NjkyOTcsImV4cCI6MTU4NjU3NDA3NiwiaWF0IjoxNTg1OTY5Mjk3fQ.yos0gPOaGQEOsvSZeW6VZGqcDw3UL_CVRVgFfsT2F94
            var user = _userService.Authenticate(model.Username, model.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
        }

        [HttpGet]       
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            return Ok(users);
        }
    }
}