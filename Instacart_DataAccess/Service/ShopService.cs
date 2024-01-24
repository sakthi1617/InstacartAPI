using Dapper;
using Instacart_DataAccess.Data;
using Instacart_DataAccess.IService;
using Instacart_DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instacart_DataAccess.Service
{
    public class ShopService : IShopService
    {
        private readonly DB_context _context;
        private readonly IPasswordservice _passwordservice;
        public ShopService(DB_context context, IPasswordservice passwordservice)
        {
            _context = context;
            _passwordservice = passwordservice;
        }
        public int ShopLogin(string username, string password)
        {
            var MerchantData = GetMerchantDetailsByUserName(username);
            if(MerchantData != null)
            {
               bool verify = _passwordservice.VerifyPassword(password,MerchantData.PasswordHash,MerchantData.PasswordSalt);
                if(verify)
                {
                    return 1;
                }
                return 0;
            }
            return 0;
        }
        public Shop GetMerchantDetailsByUserName(string userName)
        {
            using(var connection = _context.Createconnection())
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@username", userName);
                var users = connection.Query<Shop>("SP_GetShopDetails", parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return users;
            }
        }

        public int UpdateShopDetails(Shop model)
        {
           using( var connection = _context.Createconnection())
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@ShopID",model.ShopID);
                parameters.Add("@ShopName",model.ShopName);
                parameters.Add("@Location",model.Location);
                parameters.Add("@ShopImage",model.ShopImage);
                parameters.Add("@DeliveryOptionID",model.DeliveryOptionID);
                parameters.Add("@ShopCategoryID",model.ShopCategoryID);
                parameters.Add("@Action","UPDATE");

                parameters.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output);
                connection.Execute("SP_UpsertShop", parameters, commandType: CommandType.StoredProcedure);
                var responce = parameters.Get<int>("@Result");
                return responce;
            }
        }
    }
}
