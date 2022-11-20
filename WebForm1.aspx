<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="Project3WebApp.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>"Property Cost Adjustment and Listing"</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label runat="server" Text="This web application uses your address to get the Latitude and Longitude location for your property then uses that 
                to calculate a base price for a certain piece of property and adjusts it based on crime data in the state. 
                Pending user acceptance, it will then create a listing via JSON with solar and wind energy recommendations."></asp:Label>
        </div>

        <div>
            <asp:Label runat="server" Text="Please enter the information below:"></asp:Label>
        </div>

        <div>
            <asp:Label runat="server" Text="Name:"></asp:Label>
            <asp:TextBox ID="FirstName" runat="server" Text="First Name" OnTextChanged="FirstName_TextChanged"></asp:TextBox>
            <asp:TextBox ID="LastName" runat="server" Text="Last Name" OnTextChanged="LastName_TextChanged"></asp:TextBox>
        </div>

        <div>
            <asp:Label runat="server" Text="Phone Number:"></asp:Label>
            <asp:TextBox ID="PhoneNumber" runat="server" Text="999-999-9999" OnTextChanged="PhoneNumber_TextChanged"></asp:TextBox>
        </div>

        <div>
            <asp:Label runat="server" Text="Email:"></asp:Label>
            <asp:TextBox ID="Email" runat="server" Text=" " OnTextChanged="Email_TextChanged" Width="200"></asp:TextBox>
        </div>

        <div>
            <asp:Label runat="server" Text="Property Address:"></asp:Label>
            <asp:TextBox ID="AddrLine1" runat="server" Text="Street Address" OnTextChanged="AddrLine1_TextChanged" Width="200"></asp:TextBox>
            <asp:TextBox ID="City" runat="server" Text="City" OnTextChanged="City_TextChanged"></asp:TextBox>
            <asp:TextBox ID="State" runat="server" Text="State" OnTextChanged="State_TextChanged" Width="50"></asp:TextBox>
            <asp:TextBox ID="Zip" runat="server" Text="Zip Code" Width="100"></asp:TextBox>
        </div>

        <div>
            <asp:Label runat="server" Text="Property Square Footage:"></asp:Label>
            <asp:TextBox ID="SqFeet" runat="server" Text=" " OnTextChanged="SqFeet_TextChanged" Width="100"></asp:TextBox>
        </div>

        <div>
            <asp:Label runat="server" Text="Geocoding service to get the Latitude and Longitude of your property:"></asp:Label>
        </div>
        <div>
            <asp:Button ID="LatLongData" runat="server" Text="Get Latitude and Longitude" OnClick="LatLongData_Click"></asp:Button>
            <asp:TextBox ID="Longitude" runat="server" Text="Longitude" OnTextChanged="Longitude_TextChanged" Width="100"></asp:TextBox>
            <asp:TextBox ID="Latitude" runat="server" Text="Latitude" OnTextChanged="Latitude_TextChanged" Width="100"></asp:TextBox>
            <asp:TextBox ID="ErrorBox" runat="server" Text=" " OnTextChanged="ErrorBox_TextChanged" Width="400"></asp:TextBox>
        </div>

        <div>
            <asp:Label runat="server" Text="Crime Data service to get the number of violent crimes in your property's state:"></asp:Label>
        </div>
        <div>
            <asp:Button ID="CrimeData" runat="server" Text="Get Num Violent Crimes" OnClick="CrimeData_Click"></asp:Button>
            <asp:TextBox ID="crimeDataOut" runat="server" Text=" " OnTextChanged="crimeDataOut_TextChanged" Width="100"></asp:TextBox>
            <asp:TextBox ID="crimeResponse" runat="server" Text=" " OnTextChanged="crimeResponse_TextChanged" Width="400"></asp:TextBox>
        </div>

        <div>
            <asp:Label runat="server" Text="Weather service to get the average annual wind speed rating index of your property:"></asp:Label>
        </div>
        <div>
            <asp:Button ID="WindRating" runat="server" Text="Get Wind Rating" OnClick="WindRating_Click"></asp:Button>
            <asp:TextBox ID="windRatingOut" runat="server" Text=" " OnTextChanged="windRatingOut_TextChanged" Width="100"></asp:TextBox>
            <asp:TextBox ID="windResponse" runat="server" Text=" " OnTextChanged="windResponse_TextChanged" Width="400"></asp:TextBox>
        </div>

        <div>
            <asp:Label runat="server" Text="Solar Power service to get the average annual direct sunlight index rating of your property:"></asp:Label>
        </div>
        <div>
            <asp:Button ID="SolarRating" runat="server" Text="Get Solar Rating" OnClick="SolarRating_Click"></asp:Button>
            <asp:TextBox ID="solarRatingOut" runat="server" Text=" " OnTextChanged="solarRatingOut_TextChanged" Width="100"></asp:TextBox>
            <asp:TextBox ID="solarResponse" runat="server" Text=" " OnTextChanged="solarResponse_TextChanged" Width="400"></asp:TextBox>
        </div>

        <div>
            <asp:Label runat="server" Text="Service to calculate the base cost for the property, display it and then adjust the cost based on crime data provided."></asp:Label>
        </div>

        <div>
            <asp:Label runat="server" Text="Once user accepts the adjusted price of the property, then a text file created as the property listing along with a recommendation for solar and wind energy options."></asp:Label>
        </div>

        <div>
            <asp:Label runat="server" Text="Please enter the File Path you would like the listing to be sent to:"></asp:Label>
            <asp:TextBox ID="FilePath" runat="server" Text=" " OnTextChanged="FilePath_TextChanged" Width="400"></asp:TextBox>
        </div>

        <div>
            <asp:Button ID="PropListing" runat="server" Text="Get Property Adjustment" OnClick="PropListing_Click"></asp:Button>
            <asp:TextBox ID="BaseCost" runat="server" Text="Base" OnTextChanged="BaseCost_TextChanged" Width="150"></asp:TextBox>
            <asp:TextBox ID="AdjustedCost" runat="server" Text="Adjusted" OnTextChanged="AdjustedCost_TextChanged" Width="150"></asp:TextBox>
            <asp:TextBox ID="userResponse" runat="server" Text="Please respond yes/no here to accept/deny price." OnTextChanged="userResponse_TextChanged" Width="400"></asp:TextBox>
        </div>
        <div>
            <asp:Button ID="PropListing2" runat="server" Text="Get Property Listing" OnClick="PropListing2_Click"></asp:Button>
        </div>

    </form>
</body>


</html>
