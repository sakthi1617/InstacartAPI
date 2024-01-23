using Instacart_DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instacart_DataAccess.IService
{
    public interface IAuthservice
    {
        public int UserRegistraion(UserRegisterModel model);

        public int UserLogin(string Email, string password);
        public Userdetails GetUserDetails(string Email);

        public int setToken(RefreshtokenModel model);
    }
}
