using Instacart_BusinessLogic.IBusinessLogics;
using Instacart_BusinessLogic.SupportModels;
using Instacart_BusinessLogic.ViewModels;
using Instacart_DataAccess.Models;
using Microsoft.AspNetCore.Mvc;

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
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _auth.userRegister(model);
                    return Ok(result);
                }
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                ApiLog.Log("LogFile", ex.Message, ex.StackTrace, 10);
                return BadRequest(new FailureResponse<object>
                {
                    Error = ex.Message,
                    IsreponseSuccess = false
                });
            }
        }

        [HttpPost]
        [Route("UserLogin")]
        public async Task<IActionResult> UserLogin(LoginVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _auth.UserLogin(model.username, model.password);
                    return Ok(result);
                }
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                ApiLog.Log("LogFile", ex.Message, ex.StackTrace, 10);
                return BadRequest(new FailureResponse<object>
                {
                    Error = ex.Message,
                    IsreponseSuccess = false
                });
            }
        }

        [HttpPost]
        [Route("Forgetpassword")]
        public async Task<IActionResult> Forgetpassword(string Email)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _auth.Forgetpassword(Email);
                    return Ok(result);
                }
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                ApiLog.Log("LogFile", ex.Message, ex.StackTrace, 10);
                return BadRequest(new FailureResponse<object>
                {
                    Error = ex.Message,
                    IsreponseSuccess = false
                });
            }
        }

        [HttpPost]
        [Route("ValidateOTP")]
        public async Task<IActionResult> ValidateOTP(string Email, string Otp)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _auth.ValidateOTP(Email, Otp);
                    return Ok(result);
                }
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                ApiLog.Log("LogFile", ex.Message, ex.StackTrace, 10);
                return BadRequest(new FailureResponse<object>
                {
                    Error = ex.Message,
                    IsreponseSuccess = false
                });
            }
        }

        [HttpPost]
        [Route("UpdatePassword")]
        public async Task<IActionResult> UpdatePassword(string Email, string password)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _auth.UpdatePassword(Email, password);
                    return Ok(result);
                }
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                ApiLog.Log("LogFile", ex.Message, ex.StackTrace, 10);
                return BadRequest(new FailureResponse<object>
                {
                    Error = ex.Message,
                    IsreponseSuccess = false
                });
            }
        }


        [HttpPost]
        [Route("Refreshtoken")]

        public async Task<IActionResult> Refreshtoken(TokenModel model)
        {
            if(ModelState.IsValid)
            {
                var result = await _auth.RefreshToken(model);
                return Ok(result);
            }
            return BadRequest(ModelState);  
        }
    }
}

