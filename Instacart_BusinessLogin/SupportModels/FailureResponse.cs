using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instacart_BusinessLogic.SupportModels
{
    public class FailureResponse<T>
    {
        public string? Error { get; set; }
        public bool IsreponseSuccess { get; set; } = false;
    }
}
