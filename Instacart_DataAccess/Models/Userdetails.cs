using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instacart_DataAccess.Models
{
    public class Userdetails
    {
        public Guid UserID { get; set; } 
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string FullName { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Mobile { get; set; }
        public byte[] ProfileImage { get; set; }

        public DateTime CreatedOn { get; set; } 
        public DateTime LastUpdatedOn { get; set;}

        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        public int DefaultAddressID { get; set; }   

    }
}
