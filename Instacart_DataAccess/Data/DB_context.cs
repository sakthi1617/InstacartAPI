using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instacart_DataAccess.Data
{
    public class DB_context
    {
        private readonly IConfiguration _configuration;
        private readonly string? _connectionstring;
        public DB_context(IConfiguration configuration)
        {
            _configuration= configuration;
            _connectionstring = _configuration.GetConnectionString("DefaultConnection");
        }
        public SqlConnection Createconnection() => new SqlConnection(_connectionstring);
    }
}
