using Instacart_BusinessLogic.IBusinessLogics;
using Instacart_BusinessLogic.SupportModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instacart_BusinessLogic.BusinessLogics
{
    public class AdminBusinessLogic : IAdminBusinessLogic
    {
        public Task<ResponseStatus<TokenModel>> AdminLogin(string username, string password)
        {
            throw new NotImplementedException();
        }
    }
}
