using Instacart_BusinessLogic.IBusinessLogics;
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
            if(ModelState.IsValid)
            {
               var result = await _merchantBLL.ShopLogin(username, password);
                return Ok(result);
            }
            return BadRequest(ModelState);
        }

        [HttpPut]
        [Route("UpdateShopDetails")]
        public async Task<IActionResult> UpdateShopDetails([FromQuery]UpdateShopVM model)
        {
            if (ModelState.IsValid)
            {
                var result =await _merchantBLL.UpdateShopDetails(model);
                return Ok(result);
            }
            return BadRequest(ModelState);
        }
    }
}
