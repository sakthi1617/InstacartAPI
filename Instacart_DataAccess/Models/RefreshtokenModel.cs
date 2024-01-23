using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instacart_DataAccess.Models
{
    public class RefreshtokenModel
    {
        public int RefTokenId { get; set; }
        public string? JwtId { get; set; }
        public Guid UserId { get; set; }

       
        public string? RefToken { get; set; }
        public DateTime? Createdat { get; set; }
        public DateTime? Expiredat { get; set; }
    }
}
