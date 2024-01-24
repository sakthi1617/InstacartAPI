using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instacart_DataAccess.Models
{
    public class EmailModel
    {
      public string From { get; set; } = null!;
      public string SmtpServer { get; set; } = null!;
      public int Port { get; set; }
      public string userName { get; set; } = null!;
      public string Password { get; set; } = null!;
        
    }
}
