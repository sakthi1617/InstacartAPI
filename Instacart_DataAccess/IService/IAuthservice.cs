using Instacart_DataAccess.Models;
using Microsoft.AspNetCore.Identity;
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

        public int sendOtpforuser(string Email);

        public int setToken(RefreshtokenModel model);

       public int validateOTP(string Email, string OTP);

        public int Updatepassword(string Email, string password);

        public RefreshtokenModel1 GetToken(string userId);

        public void Checktime();
    }
}
