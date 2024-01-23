using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instacart_DataAccess.IService
{
    public  interface IPasswordservice
    {
        public void PasswordHash(string password, out byte[] passwordHash, out byte[] passwordsalt);
        public bool VerifyPassword(string password, byte[] passwordHash, byte[] passwordSalt);
    }
}
