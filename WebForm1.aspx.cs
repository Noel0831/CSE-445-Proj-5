using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static System.Net.WebRequestMethods;

namespace Project3WebApp
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)

        { 

        }

        protected void FirstName_TextChanged(object sender, EventArgs e)
        {

        }

        protected void LastName_TextChanged(object sender, EventArgs e)
        {

        }

        protected void PhoneNumber_TextChanged(object sender, EventArgs e)
        {

        }

        protected void Email_TextChanged(object sender, EventArgs e)
        {

        }

        protected void AddrLine1_TextChanged(object sender, EventArgs e)
        {

        }

        protected void City_TextChanged(object sender, EventArgs e)
        {

        }

        protected void State_TextChanged(object sender, EventArgs e)
        {

        }

        protected void WindRating_Click(object sender, EventArgs e)
        {
            double windLong = Convert.ToDouble(Longitude.Text);
            double windLat = Convert.ToDouble(Latitude.Text);
            double index;

            PropertyInfoRef.Service1Client windService = new PropertyInfoRef.Service1Client();
            if (windLat >= -90 && windLat <= 90 && windLong >= -180 && windLong <= 180)//valid longitude and latitude coordinates
            {
                try
                {
                    index = windService.GetWindData(windLong, windLat);
                    windRatingOut.Text = index.ToString();
                }
                catch
                {
                    index = 9999;
                }
            }
            else
            {
                index = 9999;
            }

            if (index == 9999)
            {
                windResponse.Text = "Failed: No data or invalid coordinates. Please input a latitude between -90 and 90 and a longitude -180 and 180.";
            }
            else
            {
                windResponse.Text = "Success- Index retrieved.";
            }
        }

        protected void SolarRating_Click(object sender, EventArgs e)
        {
            double solarLong = Convert.ToDouble(Longitude.Text);
            double solarLat = Convert.ToDouble(Latitude.Text);
            double index;

            PropertyInfoRef.Service1Client solarService = new PropertyInfoRef.Service1Client();
            if (solarLat >= -90 && solarLat <= 90 && solarLong >= -180 && solarLong <= 180)
            {
                try
                {
                    index = solarService.GetSolarData(solarLong, solarLat);
                    solarRatingOut.Text = index.ToString();
                }
                catch
                {
                    index = 9999;
                }

            }
            else
            {
                index = 9999;
            }

            if (index == 9999)
            {
                solarResponse.Text = "Failed: No data or invalid coordinates. Please input a latitude between -90 and 90 and a longitude -180 and 180.";
            }
            else
            {
                solarResponse.Text = "Success- Index retrieved.";
            }
        }

        protected void Longitude_TextChanged(object sender, EventArgs e)
        {

        }

        protected void Latitude_TextChanged(object sender, EventArgs e)
        {

        }

        protected void windRatingOut_TextChanged(object sender, EventArgs e)
        {

        }

        protected void windResponse_TextChanged(object sender, EventArgs e)
        {

        }

        protected void solarRatingOut_TextChanged(object sender, EventArgs e)
        {

        }

        protected void solarResponse_TextChanged(object sender, EventArgs e)
        {

        }

        protected void CrimeData_Click(object sender, EventArgs e)
        {
            string crimeState = State.Text;
            int fromYear = 2020;
            int toYear = 2021;
            int numOfViolentCrimes;

            PropertyInfoRef.Service1Client crimeService = new PropertyInfoRef.Service1Client();
            if (crimeState.Length == 2)
            {
                try
                {
                    numOfViolentCrimes = crimeService.GetCrimeData(crimeState, fromYear, toYear);
                    crimeDataOut.Text = numOfViolentCrimes.ToString();
                }
                catch
                {
                    numOfViolentCrimes = -1;
                }

            }
            else
            {
                numOfViolentCrimes = -1;
            }

            if (numOfViolentCrimes == -1)
            {
                crimeResponse.Text = "Failed: No data or invalid state abbreviation. Please input a valid 2-digit U.S. State Abbreviation.";
            }
            else
            {
                crimeResponse.Text = "Success- number of violent crimes retrieved.";
            }
        }

        protected void crimeDataOut_TextChanged(object sender, EventArgs e)
        {

        }

        protected void crimeResponse_TextChanged(object sender, EventArgs e)
        {

        }

        protected void LatLongData_Click(object sender, EventArgs e)
        {
            string address1 = AddrLine1.Text;
            string zipcode = Zip.Text;
            double[] LatLong;

            PropertyInfoRef.Service1Client GeocodeService = new PropertyInfoRef.Service1Client();
            if (address1 != " " && address1 != null && zipcode != " " && zipcode != null)
            {
                try
                {
                    LatLong = GeocodeService.GetLatLong(address1, zipcode);
                    Latitude.Text = LatLong[1].ToString();
                    Longitude.Text = LatLong[0].ToString();
                }
                catch
                {
                    ErrorBox.Text = "Error: Address and/or zipcode were invalid.";
                }

            }
            else
            {
                ErrorBox.Text = "Error: Address and/or zipcode were invalid.";
            }
        }

        protected void ErrorBox_TextChanged(object sender, EventArgs e)
        {

        }

        protected void SqFeet_TextChanged(object sender, EventArgs e)
        {

        }

        protected void PropListing_Click(object sender, EventArgs e)
        {
            string fullAddress = AddrLine1.Text + " " + City.Text + ", " + State.Text + " " + Zip.Text;
            int sqft = Convert.ToInt32(SqFeet.Text);
            int crimeNumber = Convert.ToInt32(crimeDataOut.Text);
            double solarRating = Convert.ToDouble(solarRatingOut.Text);
            double windRating = Convert.ToDouble(windRatingOut.Text);
            double PropertyCost = 222; //$222 is the average cost per sqft to buy a house in the U.S.

            if (sqft >= 0)
            {
                try //uses HTTP Client to get call RESTful service and return the base cost of the property and the adjusted cost of the property.
                {
                    Uri baseUri = new Uri("http://localhost:55289/Service1.svc");
                    UriTemplate myTemplate = new UriTemplate("GetPriceAdjusted?sqft={sqft}&crimeNum={crimeNumber}");

                    Uri completeUri = myTemplate.BindByPosition(baseUri, sqft.ToString(), crimeNumber.ToString());
                    WebClient channel = new WebClient();
                    byte[] abc = channel.DownloadData(completeUri);
                    Stream strm = new MemoryStream(abc);
                    DataContractSerializer obj = new DataContractSerializer(typeof(string));
                    string rand = obj.ReadObject(strm).ToString();

                    PropertyCost = PropertyCost * sqft; //Getting base cost.

                    BaseCost.Text = PropertyCost.ToString();
                    AdjustedCost.Text = rand;

                }
                catch
                {
                    userResponse.Text = "Error: Square Footage information was not correct.";
                }
            }
            else
            {
                userResponse.Text = "Error: Square Footage information was not correct.";
            }
        }

        protected void BaseCost_TextChanged(object sender, EventArgs e)
        {

        }

        protected void AdjustedCost_TextChanged(object sender, EventArgs e)
        {

        }

        protected void userResponse_TextChanged(object sender, EventArgs e)
        {

        }

        protected void FilePath_TextChanged(object sender, EventArgs e)
        {

        }

        protected void PropListing2_Click(object sender, EventArgs e)
        {
            if (userResponse.Text.ToLower() == "yes") //event handler to create Property listing text file only when user acceptance is achieved.
            {
                string targetFilePath = FilePath.Text; //Get rid of spaces!!!!!!!!!!!!!!!!!!
                string fullAddress = AddrLine1.Text + " " + City.Text + ", " + State.Text + " " + Zip.Text;
                int sqft = Convert.ToInt32(SqFeet.Text);
                int crimeNumber = Convert.ToInt32(crimeDataOut.Text);
                double basePrice = Convert.ToDouble(BaseCost.Text);
                double adjustedPrice = Convert.ToDouble(AdjustedCost.Text);
                double solarRating = Convert.ToDouble(solarRatingOut.Text);
                double windRating = Convert.ToDouble(windRatingOut.Text);

                if (crimeNumber > 25000) //set crime number difference from national average.
                {
                    crimeNumber = crimeNumber - 25000;
                }
                else if (crimeNumber < 25000)
                {
                    crimeNumber = 25000 - crimeNumber;
                }
                else
                {
                    crimeNumber = 0;
                }

                //checks for valid parameter values
                if (targetFilePath != null && targetFilePath != " "
                    && fullAddress != null && fullAddress != " "
                    && sqft >= 0
                    && basePrice >= 0
                    && adjustedPrice >= 0
                    && solarRating != 9999 && solarRating >= 0
                    && windRating != 9999 && solarRating >= 0)
                {
                    //Getting access denied for my file path for some reason???
                    //uses Web Client to call RESTful service and return the property listing as a text file inside the specified FilePath.
                    Uri baseUri = new Uri("http://localhost:55289/Service1.svc");
                    UriTemplate myTemplate = new UriTemplate("GetPriceListing?targetFilePath={targetFilePath}&fullAddress={fullAddress}&sqft={sqft}&basePrice={basePrice}&adjustedPrice={adjustedPrice}&solarRating={solarRating}&windRating={windRating}&numCrimes={crimeNumber}");

                    Uri completeUri = myTemplate.BindByPosition(baseUri, targetFilePath, fullAddress, sqft.ToString(), basePrice.ToString(), adjustedPrice.ToString(), solarRating.ToString(), windRating.ToString(), crimeNumber.ToString());
                    WebClient channel = new WebClient();
                    byte[] abc = channel.DownloadData(completeUri);
                    Stream strm = new MemoryStream(abc);
                    DataContractSerializer obj = new DataContractSerializer(typeof(string));
                    string rand = obj.ReadObject(strm).ToString();

                    userResponse.Text = rand;
                }
                else
                {
                    userResponse.Text = "Error: Some text boxes above may not have valid values.";
                }
            }
        }
    }
}