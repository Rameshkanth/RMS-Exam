using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eShop.Web.Core.Entities
{
    public class Order
    {
        [FromForm(Name = "dir")]
        public string Direction { get; set; }

        public int Column { get; set; }
    }
}
