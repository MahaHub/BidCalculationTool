using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;

namespace BidCalculationTool.BusinessLogic
{
    public class Vehicle
    {
        public enum VehicleType
        {
            Common,
            Luxury
        }

        public decimal BasePrice { get; set; }
        public VehicleType Type { get; set; }

        public Vehicle() {}

        public Vehicle(decimal basePrice, VehicleType type)
        {
            BasePrice = basePrice;
            Type = type;
        }
    }
}
