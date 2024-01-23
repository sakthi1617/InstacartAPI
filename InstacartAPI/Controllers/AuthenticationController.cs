using Instacart_BusinessLogic.IBusinessLogics;
using Instacart_DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace InstacartAPI.Controllers
{
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthBLL _auth;
        public AuthenticationController(IAuthBLL auth)
        {
            _auth = auth;   
        }

        [HttpPost]
        [Route("UserRegistration")]
        public async Task<IActionResult> UserRegister(UserRegisterModel model)
        {
           if(ModelState.IsValid)
            {
                var result = await _auth.userRegister(model);
                return Ok(result);
            }
           return BadRequest(); 
        }

        [HttpPost]
        [Route("UserLogin")]
        public async Task<IActionResult> UserLogin(string Email, string password)
        {
            if(ModelState.IsValid)
            {
                var result = await _auth.UserLogin(Email, password);
                return Ok(result);
            }
            return BadRequest();
        }
    }
}

