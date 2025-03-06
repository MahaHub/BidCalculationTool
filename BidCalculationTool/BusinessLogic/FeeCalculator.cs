using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BidCalculationTool.BusinessLogic
{
    public class FeeCalculator
    {
        private const decimal STORAGE_FEE = 100;

        public decimal CalculateBasicBuyerFee(Vehicle vehicle)
        {
            if (vehicle.Type == Vehicle.VehicleType.Common)
                return Math.Min(Math.Max(vehicle.BasePrice * 0.1m, 10), 50);
            else
                return Math.Min(Math.Max(vehicle.BasePrice * 0.1m, 25), 200);
        }

        public decimal CalculateSellersSpecialFee(Vehicle vehicle)
        {
            return vehicle.Type == Vehicle.VehicleType.Common ? vehicle.BasePrice * 0.02m : vehicle.BasePrice * 0.04m;
        }

        public decimal CalculateAssociationFee(Vehicle vehicle)
        {
            if (vehicle.BasePrice > 3000) return 20;
            else if (vehicle.BasePrice > 1000) return 15;
            else if (vehicle.BasePrice > 500) return 10;
            else if (vehicle.BasePrice >= 1) return 5;
            return 0;
        }

        public decimal CalculateTotalCost(Vehicle vehicle)
        {
            decimal basicBuyerFee = CalculateBasicBuyerFee(vehicle);
            decimal sellersSpecialFee = CalculateSellersSpecialFee(vehicle);
            decimal vehiclePriceBasedFee = CalculateAssociationFee(vehicle);
            return vehicle.BasePrice + basicBuyerFee + sellersSpecialFee + vehiclePriceBasedFee + STORAGE_FEE;
        }
    }
}
