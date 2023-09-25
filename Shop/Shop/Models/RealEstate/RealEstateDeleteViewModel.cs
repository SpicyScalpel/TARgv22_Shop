﻿namespace Shop.Models.RealEstate
{
    public class RealEstateDeleteViewModel
    {
        public Guid? id { get; set; }
        public string Address { get; set; }
        public float SizeSqrM { get; set; }
        public int RoomCount { get; set; }
        public int Floor { get; set; }
        public string BuildingType { get; set; }

        //database
        public DateTime BuiltInYear { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
