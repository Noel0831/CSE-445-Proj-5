using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace Project3Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {

        [OperationContract]
        Task<double> GetSolarData(double longitude, double latitude);

        [OperationContract]
        Task<double> GetWindDataAsync(double longitude, double latitude);

        [OperationContract]
        Task<int> GetCrimeDataAsync(string stateAbbr, int fromYear, int toYear);

        [OperationContract]
        Task<double[]> GetLatLongAsync(string addr1, string zip);


    }
}
