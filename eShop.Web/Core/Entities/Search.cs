using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eShop.Web.Core.Entities
{
    public class Search
    {
        public string Value { get; set; }

        [FromForm(Name = "regex")]
        public bool RegularExpression { get; set; }
    }
}
