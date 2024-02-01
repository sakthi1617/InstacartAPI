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

        public Task<ResponseStatus<string>> Forgetpassword(string username);
        public Task<ResponseStatus<string>> ValidateOTP(string Email, string OTP);

        public Task<ResponseStatus<string>> UpdatePassword(string Email, string password);
        public Task<ResponseStatus<TokenModel>> RefreshToken(TokenModel model);
    }
}
