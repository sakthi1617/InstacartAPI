using Instacart_BusinessLogic.IBusinessLogics;
using Instacart_BusinessLogic.SupportModels;
using Instacart_BusinessLogic.ViewModels;
using Instacart_DataAccess.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InstacartAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShopController : ControllerBase
    {
        private readonly IShopBLL _merchantBLL;

        public ShopController(IShopBLL merchantBLL)
        {
            _merchantBLL = merchantBLL;
        }

        [HttpPost]
        [Route("ShopLogin")]
        public async Task<IActionResult> ShopLogin(string username, string password)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _merchantBLL.ShopLogin(username, password);
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

        [HttpPut]
        [Route("UpdateShopDetails")]
        public async Task<IActionResult> UpdateShopDetails([FromQuery]UpdateShopVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _merchantBLL.UpdateShopDetails(model);
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
