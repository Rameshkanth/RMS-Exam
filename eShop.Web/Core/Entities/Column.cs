using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eShop.Web.Core.Entities
{
    public class Column
    {
        public string Data { get; set; }
        public string Name { get; set; }

        [FromForm(Name = "searchable")]
        public bool IsSearchable { get; set; }

        [FromForm(Name = "orderable")]
        public bool IsOrderable { get; set; }

        public Search Search { get; set; }
    }
}
