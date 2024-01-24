using Instacart_DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instacart_DataAccess.IService
{
    public interface IShopService
    {
        public int ShopLogin(string username, string password);
        public int UpdateShopDetails(Shop model);
    }
}
