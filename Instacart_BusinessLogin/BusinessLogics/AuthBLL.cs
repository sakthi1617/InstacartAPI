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
using static System.Net.WebRequestMethods;

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
            ResponseStatus<UserRegisterModel> response = new ResponseStatus<UserRegisterModel>();
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
                    response.Status = false;
                    response.StatusCode = StatusCodes.Status400BadRequest;
                    response.Message = "Invalid username";
                    break;
                case 1:
                    var generateToken = await Authenticateuser(username);
                    response.Data = new TokenModel
                    {
                        Token = generateToken.Token,
                        RefreshToken = generateToken.RefreshToken
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

        public async Task<ResponseStatus<string>> Forgetpassword(string username)
        {
            ResponseStatus<string> response = new ResponseStatus<string>();
            var data = _authservice.sendOtpforuser(username);
            switch (data)
            {
                case 0:
                    response.Status = false;
                    response.StatusCode = StatusCodes.Status400BadRequest;
                    response.Message = "Otp send failed... Please try again";
                    break;
                case 1:
                    response.Status = true;
                    response.StatusCode = StatusCodes.Status200OK;
                    response.Message = "Otp sended successfully";
                    break;
                case -1:
                    response.Status = false;
                    response.StatusCode = StatusCodes.Status400BadRequest;
                    response.Message = "Invalid username";
                    break;
                case -2:
                    response.Status = false;
                    response.StatusCode = StatusCodes.Status400BadRequest;
                    response.Message = "Username is empty";
                    break;
            }
            return response;
        }

        public async Task<AuthendicateResult> Authenticateuser(string? username)
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
                Audience = _configuration["JwtSettings:Audience"],
                Issuer = _configuration["JwtSettings:Issuer"],
                Expires = DateTime.Now.AddMinutes(10),
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

        public async Task<ResponseStatus<TokenModel>> RefreshToken(TokenModel model)
        {
            ResponseStatus<TokenModel> response = new ResponseStatus<TokenModel>();
            string JWTToken = model.Token.ToString();
            string RefToken = model.RefreshToken.ToString();

            var priciple = GetPrincipalFromExpiredToken(JWTToken);

            if (priciple == null)
            {
                response.Status = false;
                response.Message = "Invalid token";
                response.StatusCode = StatusCodes.Status400BadRequest;
                return response;
            }
            var userName = priciple.Identity.Name;
            var emailClaim = priciple.FindFirst(ClaimTypes.Email).Value;
            string userId = priciple.FindFirst("UserId").ToString().ToUpper();
            string user = userId;
            int n = 7;
            user = user.Substring(n);
            var currentToken = _authservice.GetToken(user);

            if (currentToken == null || currentToken.Ref_Token != RefToken || currentToken.ExpierAt <= DateTime.Now)
            {
                response.Status = false;
                response.Message = "Invalid refresh token";
                response.StatusCode = StatusCodes.Status400BadRequest;
                return response;
            }
            var newtoken = Authenticateuser(emailClaim.ToString());
            if (newtoken.Result.Success = false)
            {
                response.Status = false;
                response.StatusCode = StatusCodes.Status400BadRequest;
                response.Message = "Error";
                return response;
            }
            else
            {
                response.Status = true;
                response.StatusCode = StatusCodes.Status400BadRequest;
                response.Message = "Success";
                response.Data = new TokenModel
                {
                    Token = newtoken.Result.Token,
                    RefreshToken = newtoken.Result.RefreshToken
                };
                return response;
            }

        }

        private ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token)
        {
            try
            {
                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Secret"])),
                    ValidateLifetime = true
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
                if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                    throw new SecurityTokenException("Invalid token");

                return principal;
            }
            catch (Exception ex)
            {
                ApiLog.Log("LogFile", ex.Message, ex.StackTrace, 10);
                throw ex;
            }

        }


        public async Task<ResponseStatus<string>> ValidateOTP(string Email, string OTP)
        {
            ResponseStatus<string> response = new ResponseStatus<string>();
            var data = _authservice.validateOTP(Email, OTP);
            if (data == 1)
            {
                response.Status = true;
                response.StatusCode = StatusCodes.Status200OK;
                response.Message = "Valid OTP";
            }
            else
            {
                response.Status = false;
                response.StatusCode = StatusCodes.Status400BadRequest;
                response.Message = "InValid OTP";
            }
            return response;
        }

        public async Task<ResponseStatus<string>> UpdatePassword(string Email, string password)
        {
            ResponseStatus<string> response = new ResponseStatus<string>();
            var data = _authservice.Updatepassword(Email, password);
            if (data == 1)
            {
                response.Status = true;
                response.StatusCode = StatusCodes.Status200OK;
                response.Message = "Password updated successfully";
            }
            else
            {
                response.Status = false;
                response.StatusCode = StatusCodes.Status400BadRequest;
                response.Message = "Password updated not completely...some thing went wrong";
            }
            return response;
        }
    }
}
