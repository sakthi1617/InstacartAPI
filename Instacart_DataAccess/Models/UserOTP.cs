using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instacart_DataAccess.Models
{
    public class UserOTP
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string OTP { get; set; } 
        public bool isActive{ get; set; }
        public bool isTimeout { get; set; } 

        public bool isExpired { get; set; } 

        public DateTime CreatedOn { get; set; }
    }
}
