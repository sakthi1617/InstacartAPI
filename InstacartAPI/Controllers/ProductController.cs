using Instacart_BusinessLogic.IBusinessLogics;
using Instacart_BusinessLogic.SupportModels;
using Instacart_DataAccess.IService;
using Instacart_DataAccess.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InstacartAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductBLL productBLL;

        public ProductController(IProductBLL _productBLL)
        {
            productBLL = _productBLL;
        }

        [HttpPost]
        [Route("CreateProduct")]
        public async Task<IActionResult> CreateProduct(Product model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await productBLL.CreateProduct(model);
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
        [Route("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct(Guid productid, Product model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var updatedData = await productBLL.UpdateProducts(productid, model);
                    return Ok(updatedData);
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

        [HttpDelete]
        [Route("DeleteProduct/{productId}")]
        public async Task<IActionResult> DeleteProduct(Guid productId)
        {
            try
            {
                var result = await productBLL.DeleteProduct(productId);

                if (result.Status)
                {
                    return Ok(result);
                }
                return BadRequest();
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

        [HttpGet]
        [Route("GetProductByID")]
        public async Task<IActionResult> GetProductByID(Guid productid)
        {
            try
            {
                var data = await productBLL.GetProductByID(productid);

                if (data != null)
                {
                    return Ok(data);
                }

                return BadRequest();
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

        [HttpGet]
        [Route("GetAllProduct")]
        public async Task<IActionResult> GetAllProduct()
        {
            try
            {
                var data = await productBLL.GetProducts();
                if (data != null)
                {
                    return Ok(data);
                }
                return BadRequest();

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
