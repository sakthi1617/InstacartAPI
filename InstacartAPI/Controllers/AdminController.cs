using Instacart_BusinessLogic.IBusinessLogics;
using Instacart_BusinessLogic.SupportModels;
using Instacart_BusinessLogic.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InstacartAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminBLL _adminbll;
        public AdminController(IAdminBLL adminBusinessLogic)
        {
            _adminbll = adminBusinessLogic;
        }

        [HttpPost]
        [Route("AdminLogin")]
        public async Task<IActionResult> AdminLogin(string username , string password)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _adminbll.AdminLogin(username, password);
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
        [Route("AddShop")]
        public async Task<IActionResult> AddShop([FromQuery]AddShopVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _adminbll.AddShop(model);
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
    }
}
