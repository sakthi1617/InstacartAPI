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
    public class ShopBLL : IShopBLL
    {
        private readonly IShopService _merchantService;

        public ShopBLL(IShopService merchantService)
        {
            _merchantService = merchantService;
        }
        public async Task<ResponseStatus<TokenModel>> ShopLogin(string username, string password)
        {
            ResponseStatus<TokenModel> response = new ResponseStatus<TokenModel>();
            var data = _merchantService.ShopLogin(username, password);
            switch (data)
            {
                case 0:
                    response.Status = false;
                    response.StatusCode = StatusCodes.Status404NotFound;
                    response.Message = "Invalid UserName";
                    break;
                case 1:
                    response.Status = true;
                    response.StatusCode = StatusCodes.Status200OK;
                    response.Message = "Login Successfully";
                    response.Data = new TokenModel()
                    {
                        Token = "JWT_token",
                        RefreshToken = "RefreshToken",
                    };
                    break;
                case -1:
                    response.Status = false;
                    response.StatusCode = StatusCodes.Status400BadRequest;
                    response.Message = "Incorrect password";
                    break;
            }
            return response;
        }
        public async Task<ResponseStatus<UpdateShopVM>> UpdateShopDetails(UpdateShopVM model)
        {
            ResponseStatus<UpdateShopVM> response = new ResponseStatus<UpdateShopVM>();
            Shop shop = new Shop();
            shop.ShopID = model.ShopId;
            shop.ShopName = model.ShopName;
            shop.Location = model.Location;
            if (model.ShopImage != null && model.ShopImage.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    model.ShopImage.CopyTo(memoryStream);
                    shop.ShopImage = memoryStream.ToArray();
                }
            }
            var data = _merchantService.UpdateShopDetails(shop);
            switch (data)
            {
                case 0:
                    response.Status = false;
                    response.StatusCode = StatusCodes.Status404NotFound;
                    response.Message = "Not found";
                    break;
                case 1:
                    response.Status = true;
                    response.StatusCode = StatusCodes.Status200OK;
                    response.Data = model;
                    response.Message = "Data Updated Successfully...";
                    break;
                case -1:
                    response.Status = false;
                    response.StatusCode = StatusCodes.Status400BadRequest;
                    response.Message = "Somthing went wrong please try again later ...";
                    break;
            }
            return response;
        }
    }
}
