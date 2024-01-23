using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instacart_DataAccess.Models
{
    public class Shop
    {
        public Guid ShopID { get; set; }
        public string ShopName { get; set; }
        public string Location { get; set; }
        public string UserName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public byte[] ShopImage { get; set; }
        public int DeliveryOptionID {  get; set; }
        public int ShopCategoryID {  get; set; }

    }
}
