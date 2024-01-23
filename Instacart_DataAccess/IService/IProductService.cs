using Instacart_DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instacart_DataAccess.IService
{
    public interface IProductService
    {
        public int AddProduct(Product model);
        public int UpdateProduct(int id, Product model);
        public int DeleteProduct(Guid productId);
        //public int GetProductByID (Guid productId);
        //public List<Product> GetAllProducts();
    }
}
