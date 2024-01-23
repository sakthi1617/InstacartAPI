using Instacart_DataAccess.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Instacart_DataAccess.Service
{
    public class PasswordService : IPasswordservice
    {
        public void PasswordHash(string password, out byte[] passwordHash, out byte[] passwordsalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordsalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

            }
        }

        public bool VerifyPassword(string password, byte[] passwordHash , byte[] passwordSalt)
        {
            using (var hmac =new HMACSHA512(passwordSalt))
            {
                var computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return computeHash.SequenceEqual(passwordHash);
            }
        }
    }
}
