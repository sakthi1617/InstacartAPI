using Instacart_BusinessLogic.SupportModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instacart_BusinessLogic.IBusinessLogics
{
    public interface IAdminBusinessLogic
    {
        public Task<ResponseStatus<TokenModel>> AdminLogin(string username, string password);
    }
}
