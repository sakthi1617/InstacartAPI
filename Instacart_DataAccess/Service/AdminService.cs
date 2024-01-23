using Instacart_DataAccess.Data;
using Instacart_DataAccess.IService;
using Instacart_DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instacart_DataAccess.Service
{
    public class AdminService : IAdminService
    {
        private readonly DB_context _context;
        public AdminService(DB_context context)
        {
            _context = context;   
        }
        public int AddShop(Shop model)
        {
            return 1;
        }
    }
}
