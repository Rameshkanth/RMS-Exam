using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eShop.Web.Entities
{
    public enum OrderStatusType
    {
        OrderPlaced = 1,
        OrderInProgress,
        PreparingToShip,
        Shipped,
        Delivered
    };

    public enum SortDirection
    {
        Ascending,
        Descending
    };
}
