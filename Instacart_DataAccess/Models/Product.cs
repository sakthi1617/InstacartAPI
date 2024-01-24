using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instacart_DataAccess.Models
{
    public class Product
    {
        public Guid ProductId { get; set; }
        public int CategoryID { get; set; }    
        public int SubCategoryID { get; set; }
        public int BrandID { get; set;}
        public string? ProductName { get; set; } 
        public decimal Price { get; set; }
        public int MeasurementID { get; set; }
        public decimal Measurement { get; set; }
        public string? Description { get; set; }
        public string? Directions { get; set; }
        public string? Warning { get; set; }
        public string? Ingredients { get; set; }
        public int StockCount { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set;}
        public bool IsActive { get; set; }
        public bool InStock { get; set; }
        public int ShopID { get; set; }
        public bool IsOfferAvailable { get; set; }
        public int OfferID { get; set; }
        public decimal DiscountAmount { get; set; }

        
    }
}
