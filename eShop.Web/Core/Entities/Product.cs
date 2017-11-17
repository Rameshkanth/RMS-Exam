using System;

namespace eShop.Web.Entities
{
    public class Product
    {
        public Guid Id { get; set; }

        public string ProcuctName { get; set; }

        public string ShortDescription { get; set; }

        public float Price { get; set; }

        public string ImageUrl
        {
            get
            {
                return $"/{this.Id}.jpg";
            }
        }
    }
}
