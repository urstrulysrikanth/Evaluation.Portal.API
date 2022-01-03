using Evaluation.Portal.API.Models;
using Evaluation.Portal.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Evaluation.Portal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public ActionResult<List<User>> Get()
        {
            List<User> users;
            try
            {
                users = _userService.Get();

                if (users == null)
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return new ObjectResult(ex)
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }

            return users;
        }

        public ActionResult<User> Get(string id)
        {
            var user = _userService.Get(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }


        [HttpPost]
        public void Post([FromBody] User user)
        {
            try
            {
                _userService.InsertUser(user);
            }
            catch (Exception ex)
            {               
            }
        }


        [HttpPut]
        public void Put([FromBody] User user)
        {
            try
            {
                _userService.UpdateUser(user);
            }
            catch (Exception ex)
            {
               
            }
        }

        [HttpDelete("{userId}")]
        public void Delete(string userId)
        {
            try
            {
                _userService.DeleteUser(userId);
            }
            catch (Exception ex)
            {

            }
        }

        [HttpGet("getTrAndMrUsers")]
        public ActionResult<List<User>> GetTrAndMrUsers()
        {
            List<User> users;
            try
            {
                users = _userService.GetTrAndMrUsers();

                if (users == null)
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return new ObjectResult(ex)
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
            return users;
        }
    }
}
