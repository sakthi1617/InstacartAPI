﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instacart_BusinessLogic.ViewModels
{
    public class AddShopVM
    {
        public string ShopName { get; set;}
        public string Location { get; set;}
        public string UserName { get; set;}
        public string Password { get; set;}       
        public int DeliveryOption { get;set;}
        public int shopCategory { get; set;}
        public IFormFile ShopImage { get; set; }
    }
}
