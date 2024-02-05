using Instacart_DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instacart_DataAccess.IService
{
    public interface IAdminService
    {
        public int AddShop(Shop model);
        public int RemoveShop(Guid ShopId);
        public int DeactiveShop(Guid ShopId,bool IsActive);
    }
}
