using System;

namespace eShop.Web.Entities
{
    public class UserOrder
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string ProductId { get; set; }

        public int Quantity { get; set; }

        public float Price { get; set; }

        public bool OrderStatus { get; set; }
    }
}
