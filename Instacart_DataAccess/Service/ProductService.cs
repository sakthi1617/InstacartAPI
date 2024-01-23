using Instacart_DataAccess.Data;
using Instacart_DataAccess.IService;
using Instacart_DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instacart_DataAccess.Service
{
    public class ProductService : IProductService
    {
        private readonly DB_context _Context;

        public ProductService (DB_context context)
        {
            _Context = context;
        }   

        public int AddProduct(Product product)
        {
            using (var connection = _Context.Createconnection())
            {
                using (SqlCommand command = new SqlCommand("CreateProducts", connection))
                {
                    try
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Action", "Add");
                        Guid guid= Guid.NewGuid();
                        product.ProductId= guid;
                        command.Parameters.AddWithValue("@ProductId", product.ProductId);
                        command.Parameters.AddWithValue("@CategoryID", product.CategoryID);
                        command.Parameters.AddWithValue("@SubCategoryID", product.SubCategoryID);
                        command.Parameters.AddWithValue("@BrandID", product.BrandID);
                        command.Parameters.AddWithValue("@ProductName", product.ProductName);
                        command.Parameters.AddWithValue("@Price", product.Price);
                        command.Parameters.AddWithValue("@MeasurementID", product.MeasurementID);
                        command.Parameters.AddWithValue("@Measurement", product.Measurement);
                        command.Parameters.AddWithValue("@Description", product.Description);
                        command.Parameters.AddWithValue("@Directions", product.Directions);
                        command.Parameters.AddWithValue("@Warning", product.Warning);
                        command.Parameters.AddWithValue("@Ingredients", product.Ingredients);
                        command.Parameters.AddWithValue("@StockCount", product.StockCount);
                        command.Parameters.AddWithValue("@IsActive", product.IsActive);
                        command.Parameters.AddWithValue("@InStock", product.InStock);
                        command.Parameters.AddWithValue("@ShopID", product.ShopID);
                        command.Parameters.AddWithValue("@IsOfferAvailable", product.IsOfferAvailable);
                        command.Parameters.AddWithValue("@OfferID", product.OfferID);
                        command.Parameters.AddWithValue("@DiscountAmount", product.DiscountAmount);
                        SqlParameter outputparameter = new SqlParameter("@Response", SqlDbType.Int);
                        outputparameter.Direction = ParameterDirection.Output;
                        command.Parameters.Add(outputparameter);
                        connection.Open();

                        command.ExecuteScalar();
                        int result = Convert.ToInt32(outputparameter.Value);
                        return result;
                    }
                    catch(Exception ex) 
                    {
                        throw;
                    }
                }
            }
        }

        public int UpdateProduct(int id, Product product)
        {
            using (var connection = _Context.Createconnection())
            {
                using (SqlCommand command = new SqlCommand("CreateProducts", connection))
                {
                    try
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Action", "Update");
                        Guid guid = Guid.NewGuid();
                        product.ProductId = guid;
                        command.Parameters.AddWithValue("@ProductId", id);
                        command.Parameters.AddWithValue("@CategoryID", product.CategoryID);
                        command.Parameters.AddWithValue("@SubCategoryID", product.SubCategoryID);
                        command.Parameters.AddWithValue("@BrandID", product.BrandID);
                        command.Parameters.AddWithValue("@ProductName", product.ProductName);
                        command.Parameters.AddWithValue("@Price", product.Price);
                        command.Parameters.AddWithValue("@MeasurementID", product.MeasurementID);
                        command.Parameters.AddWithValue("@Measurement", product.Measurement);
                        command.Parameters.AddWithValue("@Description", product.Description);
                        command.Parameters.AddWithValue("@Directions", product.Directions);
                        command.Parameters.AddWithValue("@Warning", product.Warning);
                        command.Parameters.AddWithValue("@Ingredients", product.Ingredients);
                        command.Parameters.AddWithValue("@StockCount", product.StockCount);
                        command.Parameters.AddWithValue("@IsActive", product.IsActive);
                        command.Parameters.AddWithValue("@InStock", product.InStock);
                        command.Parameters.AddWithValue("@ShopID", product.ShopID);
                        command.Parameters.AddWithValue("@IsOfferAvailable", product.IsOfferAvailable);
                        command.Parameters.AddWithValue("@OfferID", product.OfferID);
                        command.Parameters.AddWithValue("@DiscountAmount", product.DiscountAmount);
                        SqlParameter outputparameter = new SqlParameter("@Response", SqlDbType.Int);
                        outputparameter.Direction = ParameterDirection.Output;
                        command.Parameters.Add(outputparameter);
                        connection.Open();

                        command.ExecuteScalar();
                        int result = Convert.ToInt32(outputparameter.Value);
                        return result;
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                }
            }
        }

        public int DeleteProduct(Guid productId)
        {
            using (var connection = _Context.Createconnection())
            {
                using(SqlCommand command = new SqlCommand("DeleteProducts", connection)) 
                {                   
                    try
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ProductID", productId);

                        connection.Open();

                        int rowsAffected = command.ExecuteNonQuery();

                        return rowsAffected; 
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                }
            }
        }

        //public int GetProductByID(Guid productId)
        //{
        //    using (var connection = _Context.Createconnection())
        //    {
        //        using (SqlCommand command = new SqlCommand("GetProductByID", connection))
        //        {
        //            try
        //            {
        //                connection.Open();
        //                // Use Dapper to execute the stored procedure with parameter binding
        //                var result = connection.Query<Product>("GetProductByID", new { ProductID = productId }, commandType: CommandType.StoredProcedure).ToList();

        //                return result;
        //            }
        //            catch (Exception ex)
        //            {
        //                throw;
        //            }
        //        }
        //    }
        //}

        //public List<Product> GetProductList()
        //{
        //    using (var connection = _Context.Createconnection())
        //    {
        //        try
        //        {
        //            connection.Open();

        //            // Use Dapper to execute the stored procedure
        //            var productList = connection.Query<Product>("GetProducts", commandType: CommandType.StoredProcedure).ToList();

        //            return productList;
        //        }
        //        catch(Exception ex)
        //        {
        //            throw;
        //        }
        //    }
        //}
    }
}
