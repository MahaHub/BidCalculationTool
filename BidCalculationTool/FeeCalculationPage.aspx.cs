using BidCalculationTool.BusinessLogic;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BidCalculationTool
{
    public partial class FeeCalculationPage : Page
    {
        const decimal STORAGE_FEE = 100;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                RecalculateTotalCost();
            }
        }

        protected void txtBasePrice_TextChanged(object sender, EventArgs e)
        {
            RecalculateTotalCost();
        }

        protected void ddlVehicleType_SelectedIndexChanged(object sender, EventArgs e)
        {
            RecalculateTotalCost();
        }

        private void RecalculateTotalCost()
        {
            if (decimal.TryParse(txtBasePrice.Text, out decimal basePrice))
            {
                FeeCalculator feeCalculator = new FeeCalculator();

                Vehicle.VehicleType vehicleType = (Vehicle.VehicleType)Enum.Parse(typeof(Vehicle.VehicleType), ddlVehicleType.SelectedValue);
                Vehicle vehicle = new Vehicle(basePrice, vehicleType);

                var feeResults = GetFees(vehicle, feeCalculator);

                GridViewResults.DataSource = feeResults;
                GridViewResults.DataBind();

                decimal totalCost = feeCalculator.CalculateTotalCost(vehicle);
                lblTotalCost.Text = totalCost.ToString("C");
            }
        }

        private Object GetFees(Vehicle vehicle, FeeCalculator feeCalculator)
        {
            decimal basicBuyerFee = feeCalculator.CalculateBasicBuyerFee(vehicle);
            decimal sellersSpecialFee = feeCalculator.CalculateSellersSpecialFee(vehicle);
            decimal AssociationFee = feeCalculator.CalculateAssociationFee(vehicle);

            var feeResults = new[]
            {
                    new { FeeType = "Basic Buyer Fee", Amount = basicBuyerFee },
                    new { FeeType = "Seller's Special Fee", Amount = sellersSpecialFee },
                    new { FeeType = "Association Fee", Amount = AssociationFee },
                    new { FeeType = "Storage fee", Amount = STORAGE_FEE },
                };

            return feeResults;
        }
    }
}