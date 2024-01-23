using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instacart_BusinessLogic.SupportModels
{
    public class TokenModel
    {
        public string Token { get; set; }

        public string RefreshToken { get; set; }
    }
    public class AuthendicateResult : TokenModel
    {
        public bool Success { get; set; }

        public IEnumerable<string> Error { get; set; }
    }
}
