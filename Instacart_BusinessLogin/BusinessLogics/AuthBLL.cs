using Instacart_BusinessLogic.IBusinessLogics;
using Instacart_BusinessLogic.SupportModels;
using Instacart_DataAccess.IService;
using Instacart_DataAccess.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Instacart_BusinessLogic.BusinessLogics
{
    public class AuthBLL : IAuthBLL
    {
        private readonly IAuthservice _authservice;
        private readonly IConfiguration _configuration;
        public AuthBLL(IAuthservice authservice, IConfiguration configuration) 
        {
            _authservice = authservice;
            _configuration = configuration; 
        }

        public async Task<ResponseStatus<UserRegisterModel>> userRegister(UserRegisterModel userRegister)
        {
            ResponseStatus<UserRegisterModel> response= new ResponseStatus<UserRegisterModel>();
            var data = _authservice.UserRegistraion(userRegister);
            switch (data)
            {
                case 1:
                    response.Status = true;
                    response.Message = "Register Successfully";
                    response.StatusCode = StatusCodes.Status200OK;
                    break;

                case 0:
                    response.Status = false;
                    response.Message = "Email Already Exist";
                    response.StatusCode = StatusCodes.Status400BadRequest;
                    break;

            }
            return response;
        }

        public async Task<ResponseStatus<TokenModel>> UserLogin(string username, string password)
        {
            ResponseStatus<TokenModel> response = new ResponseStatus<TokenModel>();
            var data = _authservice.UserLogin(username, password);
            switch (data) 
            { 
                case 0:
                    response.Status= false;
                    response.StatusCode= StatusCodes.Status400BadRequest;
                    response.Message = "Invalid username";
                    break;
                    case 1:
                    var generateToken =await Authenticateuser(username);
                    response.Data = new TokenModel
                    {
                        Token = generateToken.Token,
                        RefreshToken= generateToken.RefreshToken
                    };
                    response.Status = true;
                    response.StatusCode = StatusCodes.Status200OK;
                    response.Message = "Login successfully";
                    break; 
                    case -1:
                    response.Status = false;
                    response.StatusCode = StatusCodes.Status400BadRequest;
                    response.Message = "Worng password";
                    break;
            }
            return response;
        }

        public async Task<AuthendicateResult> Authenticateuser(string username)
        {
            var getuserDetails = _authservice.GetUserDetails(username);
            var tokenHandler = new JwtSecurityTokenHandler();

            string apikey = _configuration["JwtSettings:Secret"];
            var key = Encoding.ASCII.GetBytes(apikey);
            ClaimsIdentity subject = new ClaimsIdentity(new Claim[]
            {
                new Claim("UserId",getuserDetails.UserID.ToString()),
                new Claim(ClaimTypes.Name, getuserDetails.FullName),
                new Claim(ClaimTypes.Email, getuserDetails.Email),
                new Claim(ClaimTypes.MobilePhone,getuserDetails.Mobile),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            });

            var tokenDescripter = new SecurityTokenDescriptor
            {
                Subject = subject,
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescripter);
            AuthendicateResult authendicate = new AuthendicateResult();
            authendicate.Token = tokenHandler.WriteToken(token);
            var refreshtoken = new RefreshtokenModel
            {
                UserId = getuserDetails.UserID,
                JwtId = token.Id,
                RefToken = Guid.NewGuid().ToString(),
                Createdat = DateTime.UtcNow,
                Expiredat = DateTime.UtcNow.AddDays(2),
            };
            authendicate.RefreshToken = refreshtoken.RefToken;
            _authservice.setToken(refreshtoken);
            return authendicate;
        }
    }
}
 