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
    public class RefreshtokenModel1
    {
        public int Ref_TokenID { get; set; }
        public string? JWT_ID { get; set; }
        public Guid UserID { get; set; }


        public string? Ref_Token { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ExpierAt { get; set; }
    }
}
