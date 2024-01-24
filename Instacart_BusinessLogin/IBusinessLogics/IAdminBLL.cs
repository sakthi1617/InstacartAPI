using Instacart_BusinessLogic.SupportModels;
using Instacart_BusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instacart_BusinessLogic.IBusinessLogics
{
    public interface IAdminBLL
    {
        public Task<ResponseStatus<TokenModel>> AdminLogin(string username, string password);
        public Task<ResponseStatus<AddShopVM>> AddShop(AddShopVM model);
    }
}
