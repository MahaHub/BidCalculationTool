<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FeeCalculationPage.aspx.cs" Inherits="BidCalculationTool.FeeCalculationPage" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        function validateBasePrice() {
            var basePrice = document.getElementById('<%= txtBasePrice.ClientID %>').value;
            var errorMessage = document.getElementById('basePriceError');

            if (isNaN(basePrice) || basePrice.trim() === "" || parseFloat(basePrice) <= 0) {
                errorMessage.style.display = 'inline';
            } else {
                errorMessage.style.display = 'none';
            }
        }
    </script>
    <main>
        <h2>Fee Calculation Results</h2>

        <label for="txtBasePrice">Vehicle Base Price: </label>
        <asp:TextBox ID="txtBasePrice" runat="server" Width="100px" AutoPostBack="true" OnTextChanged="txtBasePrice_TextChanged" oninput="validateBasePrice()"></asp:TextBox><br />
        <span id="basePriceError" style="color: red; display: none;">Invalid entry. Please enter a valid number.</span>
        <br />

        <label for="ddlVehicleType">Select Vehicle Type: </label>
        <asp:DropDownList ID="ddlVehicleType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlVehicleType_SelectedIndexChanged">
            <asp:ListItem Text="Common" Value="Common"></asp:ListItem>
            <asp:ListItem Text="Luxury" Value="Luxury"></asp:ListItem>
        </asp:DropDownList><br />
        <br />

        <asp:GridView ID="GridViewResults" runat="server" AutoGenerateColumns="false" CssClass="table" BorderWidth="1px" BorderColor="black" CellPadding="5">
            <Columns>
                <asp:BoundField DataField="FeeType" HeaderText="Fee Type" SortExpression="FeeType" />
                <asp:BoundField DataField="Amount" HeaderText="Amount" SortExpression="Amount" />
            </Columns>
        </asp:GridView>
        <br />
        <br />

        <label for="lblTotalCost">Total Cost: </label>
        <asp:Label ID="lblTotalCost" runat="server"></asp:Label>
    </main>

</asp:Content>
