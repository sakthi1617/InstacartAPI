using Dapper;
using Instacart_DataAccess.Data;
using Instacart_DataAccess.IService;
using Instacart_DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
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
            using(var connection = _context.Createconnection())
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@ShopID", model.ShopID);
                parameters.Add("@ShopName", model.ShopName);
                parameters.Add("@Location", model.Location);
                parameters.Add("@UserName", model.UserName);
                parameters.Add("@PasswordHash", model.PasswordHash);
                parameters.Add("@PasswordSalt", model.PasswordSalt);
                parameters.Add("@ShopImage", model.ShopImage);
                parameters.Add("@DeliveryOptionID", model.DeliveryOptionID);
                parameters.Add("@ShopCategoryID", model.ShopCategoryID);
                parameters.Add("@Action","ADD");

                parameters.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output);
                connection.Execute("SP_UpsertShop", parameters, commandType: CommandType.StoredProcedure);
                var responce = parameters.Get<int>("@Result");

                return responce;
            }
            
        }

        public int DeactiveShop(Guid ShopId, bool IsActive)
        {
            using( var connection = _context.Createconnection())
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@ShopId", ShopId);
                parameters.Add("@IsActive", IsActive);
                parameters.Add("@Action", "UPDATE");
                parameters.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output);
                connection.Execute("SP_UpsertShop", parameters, commandType: CommandType.StoredProcedure);
                var responce = parameters.Get<int>("@Result");

                return responce;
            }
        }

        public int RemoveShop(Guid ShopId)
        {
           using(var connection = _context.Createconnection())
            {
                DynamicParameters parameters= new DynamicParameters();
                parameters.Add("@ShopId", ShopId);
                parameters.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output);
                connection.Execute("SP_RemoveShop", parameters, commandType: CommandType.StoredProcedure);
                var responce = parameters.Get<int>("@Result");

                return responce;
            }
        }
    }
}
