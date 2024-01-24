using Instacart_BusinessLogic.IBusinessLogics;
using Instacart_BusinessLogic.SupportModels;
using Instacart_DataAccess.IService;
using Instacart_DataAccess.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instacart_BusinessLogic.BusinessLogics
{
    public class ProductBLL : IProductBLL
    {
        private readonly IProductService _productService;
        public ProductBLL(IProductService productService)
        {
            _productService = productService;
        }
        public async Task<ResponseStatus<Product>> CreateProduct(Product product)
        {
            ResponseStatus<Product> response = new ResponseStatus<Product>();

            try
            {
                var result =  _productService.AddProduct(product);

                switch (result)
                {
                    case 1:
                        response.Status = true;
                        response.Message = "Product Successfully Created.";
                        response.StatusCode = StatusCodes.Status200OK;
                        break;
                    case 0:
                        response.Status = false;
                        response.Message = "Something went wrong!";
                        response.StatusCode = StatusCodes.Status400BadRequest;
                        break;                    
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return response;
        }


        public async Task<ResponseStatus<Product>> UpdateProducts(Guid productid, Product model)
        {
            ResponseStatus<Product> response = new ResponseStatus<Product>();
            try
            {
                var result = _productService.UpdateProduct(productid, model);
                switch (result)
                {
                    case 1:
                        response.Status = true;
                        response.Message = "Product Updated Successfully";
                        response.StatusCode = StatusCodes.Status200OK;
                        break;
                    case 0:
                        response.Status = false;
                        response.Message = "Something went worng...";
                        response.StatusCode = StatusCodes.Status400BadRequest;
                        break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return response;
        }

        public async Task<ResponseStatus<int>>DeleteProduct(Guid productId)
        {
            ResponseStatus<int> response = new ResponseStatus<int>();
            try
            {
                var result = _productService.DeleteProduct(productId);
                switch (result)
                {
                    case 1:
                        response.Status = true;
                        response.Message = "Product was Deleted.";
                        response.StatusCode = StatusCodes.Status200OK;
                        break;
                    case 0:
                        response.Status = false;
                        response.Message = "Something went worng";
                        response.StatusCode = StatusCodes.Status400BadRequest;
                        break;
                }
            }
            catch(Exception ex) 
            { 
                throw ex; 
            }

            return response;
        }

        public async Task<ResponseStatus<Product>> GetProductByID(Guid productid)
        {
            ResponseStatus<Product> response = new ResponseStatus<Product>();
            try
            {
                var data =  _productService.GetProductByID(productid);
                if (data.Count > 0)
                {
                    response.Status = true;
                    response.Message = "Get the Product data's.";
                    response.Listdata = (List<Product>)data;
                    response.StatusCode = StatusCodes.Status200OK;
                    
                }
                else
                {
                    response.Status = false;
                    response.Message = "Please check your ProductID";
                    response.StatusCode = StatusCodes.Status400BadRequest;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return response;
        }

        public async Task<ResponseStatus<Product>> GetProducts()
        {
            ResponseStatus<Product> response = new ResponseStatus<Product>();

            try
            {
                var data = _productService.GetAllProducts();
                if (data.Count > 0)
                {
                    response.Status = true;
                    response.Listdata = (List<Product>)data;
                    response.Message = "Successfully Executed Get all Products.";
                    response.StatusCode = StatusCodes.Status200OK;
                }
                else
                {
                    response.Status = false;
                    response.Message = "Error";
                    response.StatusCode = StatusCodes.Status400BadRequest;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;
        }
    }
}
