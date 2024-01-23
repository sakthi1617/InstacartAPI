using Instacart_BusinessLogic.SupportModels;
using Instacart_DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instacart_BusinessLogic.IBusinessLogics
{
    public interface IAuthBLL
    {
        public  Task<ResponseStatus<UserRegisterModel>> userRegister(UserRegisterModel userRegister);
        public  Task<ResponseStatus<TokenModel>> UserLogin(string username, string password);
    }
}
