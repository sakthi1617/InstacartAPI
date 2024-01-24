using Instacart_BusinessLogic.SupportModels;
using Instacart_DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instacart_BusinessLogic.IBusinessLogics
{
    public interface IProductBLL 
    {
        public Task<ResponseStatus<Product>>CreateProduct(Product product);
        public Task<ResponseStatus<Product>>UpdateProducts(Guid productid, Product model);
        public Task<ResponseStatus<int>> DeleteProduct(Guid productid);
        public Task<ResponseStatus<Product>> GetProductByID(Guid productid);
        public Task<ResponseStatus<Product>> GetProducts();
    }
}
