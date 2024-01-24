using Instacart_BusinessLogic.IBusinessLogics;
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
            if(ModelState.IsValid)
            {
                var result = await _adminbll.AdminLogin(username, password);
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpPost]
        [Route("AddShop")]
        public async Task<IActionResult> AddShop([FromQuery]AddShopVM model)
        {
            if(ModelState.IsValid)
            {
                var result = await _adminbll.AddShop(model);
                return Ok(result);
            }
            return BadRequest();
        }
    }
}
