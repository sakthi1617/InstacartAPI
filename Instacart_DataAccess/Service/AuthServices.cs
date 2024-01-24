using Dapper;
using Instacart_DataAccess.Data;
using Instacart_DataAccess.IService;
using Instacart_DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Instacart_DataAccess.Service
{
    public class AuthServices : IAuthservice
    {
        private readonly DB_context _context;
        private readonly IPasswordservice _passwordservice;
        private readonly EmailServices _emailservice;
        public AuthServices(DB_context context, IPasswordservice passwordservice, EmailServices emailservice) 
        {
            _context= context;
            _passwordservice= passwordservice;
            _emailservice= emailservice;
        }

        public int UserLogin(string Email, string password)
        {
            var getuser = GetUserDetails(Email);
            if (getuser != null)
            {
                var checkPassword = _passwordservice.VerifyPassword(password, getuser.PasswordHash, getuser.PasswordSalt);
                if(checkPassword == true)
                {
                    return 1;
                }
                else
                {
                    return -1;
                }
            }
            return 0;
        }
        public int UserRegistraion(UserRegisterModel model)
        {
           using(var connection = _context.Createconnection())
            {
                using (SqlCommand command = new SqlCommand("SP_UserRegister", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    var fullname = model.Firstname + " " + model.Lastname;
                    Guid guid= Guid.NewGuid();
                    model.UserId = guid;

                    // convert iform file to byte[]
                    var userimage = model.UserImage;
                    byte[] data;
                    using (MemoryStream ms = new MemoryStream()) {
                        userimage.CopyTo(ms);
                         data = ms.ToArray();
                    }
                    command.Parameters.AddWithValue("@UserID", model.UserId);
                    command.Parameters.AddWithValue("@Firstname", model.Firstname);
                    command.Parameters.AddWithValue("@Lastname", model.Lastname);
                    command.Parameters.AddWithValue("@Fullname",fullname);
                    command.Parameters.AddWithValue("@Email", model.Email);
                    command.Parameters.AddWithValue("@Mobile", model.Mobile);
                    _passwordservice.PasswordHash(model.Password, out byte[] passwordHash, out byte[] passwordSalt);
                    command.Parameters.AddWithValue("@PasswordHash", passwordHash);
                    command.Parameters.AddWithValue("@PasswordSalt",passwordSalt);
                    command.Parameters.AddWithValue("@ProfileImage", data);
                    command.Parameters.AddWithValue("@CreatedOn", DateTime.Now);
                    SqlParameter outputparameter = new SqlParameter("@Result", SqlDbType.Int);
                    outputparameter.Direction = ParameterDirection.Output;
                    command.Parameters.Add(outputparameter);
                    connection.Open();
                    command.ExecuteNonQuery();

                    int result = (int)outputparameter.Value;
                    return result;
                }
            }
        }
        public Userdetails GetUserDetails(string Email)
        {
            using (var connection = _context.Createconnection())
            {
                DynamicParameters parameters= new DynamicParameters();
                parameters.Add("@Email", Email);
                var users = connection.Query<Userdetails>("SP_GetUserDetails",parameters, commandType :CommandType.StoredProcedure).FirstOrDefault();
                return users;
            }
        }

        public int setToken(RefreshtokenModel model)
        {
            using (var connection = _context.Createconnection()) 
            {
                try
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@JWT_ID", model.JwtId);
                    parameters.Add("@UserID", model.UserId);
                    parameters.Add("@Ref_TokenID", model.RefTokenId);
                    parameters.Add("@Ref_Token", model.RefToken);
                    parameters.Add("@CreatedOn", DateTime.UtcNow);
                    parameters.Add("@ExpierAt", DateTime.UtcNow.AddDays(2));
                    parameters.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output);
                    connection.Execute("SP_RefreshToken", parameters, commandType: CommandType.StoredProcedure);
                    int result = parameters.Get<int>("@Result");
                    return result;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
               
            }
        }

        public int sendOtpforuser(string Email)
        {
            if(Email!=null)
            {
                var checkvalidmail = GetUserDetails(Email); 
                if (checkvalidmail != null)
                {
                    string OTP = GenerateOTP();
                    using (var connection = _context.Createconnection())
                    {
                        DynamicParameters parameters = new DynamicParameters();
                        parameters.Add("@username", Email);
                        parameters.Add("@otp", OTP);
                        parameters.Add("@isActive", true);
                        parameters.Add("@isTimeout", false);
                        parameters.Add("@isExpired", false);
                        parameters.Add("@Createdon", DateTime.UtcNow);
                        parameters.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output);
                        connection.Execute("SP_Setotp", parameters, commandType: CommandType.StoredProcedure);
                        int result = parameters.Get<int>("@Result");
                        // Send otp to user email
                        var sendedMessage = "Your One Time password(OTP) is" + OTP + ". Your otp is valid for 2 minutes";
                        var message = new Message(new string[] { Email }, "OTP Verification", sendedMessage.ToString(), null);
                        _emailservice.sendEmail(message);
                        return result;
                    }
                }
                return 0;
            }
            return -1;
        }

        public int Updatepassword(string Email, string password)
        {
            using(var connection = _context.Createconnection())
            {
                _passwordservice.PasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Username", Email);
                parameters.Add("@PasswordHash", passwordHash);
                parameters.Add("@PasswordSalt", passwordSalt);
                parameters.Add("@Result",dbType: DbType.Int32, direction: ParameterDirection.Output);
                connection.Execute("SP_UpdatePassword", parameters, commandType: CommandType.StoredProcedure);
                int result = parameters.Get<int>("@Result");
                return result;
            }
        }


        public string GenerateOTP()
        {
            Random random = new Random();
            string otp = random.Next(0, 1000000).ToString();
            return otp;
        }

        public int validateOTP(string Email, string OTP)
        {
            using (var connection = _context.Createconnection())
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Username", Email);
                parameters.Add("@OTP", OTP);
                parameters.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output);
                connection.Execute("SP_ValidateOTP", parameters, commandType: CommandType.StoredProcedure);
                int result = parameters.Get<int>("@Result");
                return result;

            }
        }
    }

}
