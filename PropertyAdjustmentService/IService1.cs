using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace PropertyAdjustmentService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {

        [OperationContract]
        [WebGet(UriTemplate = "GetPriceAdjusted?sqft={sqft}&crimeNum={crimeNum}")]
        string GetPriceAdjusted(int sqft, double crimeNum);

        [OperationContract]
        [WebGet(UriTemplate = "GetPriceListing?targetFilePath={targetFilePath}&fullAddress={fullAddress}&sqft={sqft}&basePrice={basePrice}&adjustedPrice={adjustedPrice}&solarRating={solarRating}&windRating={windRating}&numCrimes={numCrimes}")]
        string GetPriceListing(string targetFilePath, string fullAddress, int sqft, double basePrice, double adjustedPrice, double solarRating, double windRating, int numCrimes);
    }

}
