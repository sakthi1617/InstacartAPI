using Instacart_BusinessLogic.IBusinessLogics;
using Instacart_BusinessLogic.SupportModels;
using Instacart_BusinessLogic.ViewModels;
using Instacart_DataAccess.IService;
using Instacart_DataAccess.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instacart_BusinessLogic.BusinessLogics
{
    public class AdminBusinessLogic : IAdminBusinessLogic
    {
        private readonly IAdminService _adminservice;
        public AdminBusinessLogic(IAdminService adminService)
        {
            _adminservice = adminService;
        }
        public async Task<ResponseStatus<TokenModel>> AdminLogin(string username, string password)
        {
            ResponseStatus<TokenModel> response = new ResponseStatus<TokenModel>();
            if (username != null && password != null)
            {
                if (username == "Admin@123" && password == "Admin@123")
                {
                    response.Status = true;
                    response.StatusCode = StatusCodes.Status200OK;
                    response.Data = new TokenModel
                    {
                        Token = "JWT_token",
                        RefreshToken = "RefreshToken",
                    };
                    response.Message = "Login Successfully";
                }
                else
                {
                    response.Status = false;
                    response.StatusCode = StatusCodes.Status400BadRequest;
                    response.Message = "Invalid credentials";
                }
            }
            else
            {
                response.Status = false;
                response.StatusCode = StatusCodes.Status400BadRequest;
                response.Message = "Invalid credentials";
            }
            return response;
        }

        public async Task<ResponseStatus<AddShopVM>> AddShop(AddShopVM model)
        {
            ResponseStatus<AddShopVM> response = new ResponseStatus<AddShopVM>();
            Shop shop = new Shop();
            shop.ShopID = Guid.NewGuid();
            shop.ShopName = model.ShopName;
            shop.Location = model.Location;
            shop.UserName = model.UserName;
            //shop.PasswordHash = ;
            //shop.PasswordSalt = ;
            if (model.ShopImage != null && model.ShopImage.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    model.ShopImage.CopyTo(memoryStream);
                    shop.ShopImage = memoryStream.ToArray();                    
                }
            }
            shop.DeliveryOptionID = model.DeliveryOption;
            shop.ShopCategoryID = model.shopCategory;
            var data = _adminservice.AddShop(shop);
            return response;
        }
    }
}
