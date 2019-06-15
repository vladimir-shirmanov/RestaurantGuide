using System;

namespace RestaurantGuide.Domain
{
    public class Restaurant : BaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }
    }
}