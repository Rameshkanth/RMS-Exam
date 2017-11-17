using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eShop.Web.Core.Entities
{
    public class DataTableResponse<T> where T : class
    {
        public IEnumerable<T> Data { get; set; }
        public int RecordsTotal { get; set; }
        public int RecordsFiltered { get; set; }
        public int Draw { get; set; }
    }
}
