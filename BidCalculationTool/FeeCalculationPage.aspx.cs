using BidCalculationTool.BusinessLogic;
using System;
using System.Collections.Generic;
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
                BindGrid(new Vehicle(), new FeeCalculator());
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

                BindGrid(vehicle, feeCalculator);

                decimal totalCost = feeCalculator.CalculateTotalCost(vehicle);
                lblTotalCost.Text = totalCost.ToString("C");
            }
        }

        private void BindGrid(Vehicle vehicle, FeeCalculator feeCalculator)
        {
            decimal basicBuyerFee = 0.0m;
            decimal sellersSpecialFee = 0.0m;
            decimal AssociationFee = 0.0m;

            if (txtBasePrice.Text != "")
            {
                basicBuyerFee = feeCalculator.CalculateBasicBuyerFee(vehicle);
                sellersSpecialFee = feeCalculator.CalculateSellersSpecialFee(vehicle);
                AssociationFee = feeCalculator.CalculateAssociationFee(vehicle);
            }

            var feeResults = new List<object>
            {
                new { FeeType = "Basic Buyer Fee", Amount = basicBuyerFee > 0 ? basicBuyerFee : 0.00m },
                new { FeeType = "Seller's Special Fee", Amount = sellersSpecialFee > 0 ? sellersSpecialFee : 0.00m },
                new { FeeType = "Association Fee", Amount = AssociationFee > 0 ? AssociationFee : 0.00m },
                new { FeeType = "Storage fee", Amount = txtBasePrice.Text != "" ? STORAGE_FEE : 0.00m }
            };

            GridViewResults.DataSource = feeResults;
            GridViewResults.DataBind();
        }
    }
}