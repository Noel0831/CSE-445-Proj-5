using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace Project3Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        public class LatLongResults //class with objects we need from resulting JSON
        {
            public double Latitude { get; set; }
            public double Longitude { get; set; }
        }
        
        public class CrimeResults // child class with objects we need from resulting JSON
        {
            public int violent_crime { get; set; }
        }

        public class CrimeData //parent class with objects we need from resulting JSON
        {
            public CrimeResults[] results { get; set; }
        }

        public class WindData //class with objects we need from resulting JSON
        {
            public double wind_speed { get; set; }
        }

        public class DniAvg //class with objects we need from resulting JSON
        {
            public double annual { get; set; }
        }

        public class SolarData //parent class with objects we need from resulting JSON
        {
            public DniAvg avg_dni { get; set; }
        }

        public class SolarOutputs //super class with objects we need from resulting JSON
        {
            public SolarData outputs { get; set; }
        }


        public async Task<double[]> GetLatLongAsync(string addr1, string zip)
        {
            double[] getResults = new double[2];

            String[] address1Words = addr1.Split(' '); //splits the string by spaces
            addr1 = address1Words[0]; //assigns first group of non-space chars back to the original string
            for (int i = 1; i < address1Words.Length; i++)
            {
                addr1 = addr1 + "%20" + address1Words[i]; //concatenates every subsequent char group back to the string seperated by %20 formatting
            }

            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://www.yaddress.net/api/address?AddressLine1={addr1}&AddressLine2={zip}"), //forming address to Web API
            };

            LatLongResults results;
            using (var response = await client.SendAsync(request)) //uses the response from the api call
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                Console.WriteLine(body);
                //deserializes the Json object and gets the information we need as defined in the LatLongResults class above
                results = JsonConvert.DeserializeObject<LatLongResults>(body.ToString());
            }


            if (results?.Longitude == null || results?.Latitude == null) //checks if results were null or non-existent
            {
                getResults[0] = 9999; //if null or non-existent, give error code 9999
            }
            else //move resulting data into the array to be returned
            {
                getResults[0] = results.Longitude;
                getResults[1] = results.Latitude;
                Console.WriteLine(getResults[0] + ", " + getResults[1]);
            }

            return getResults; //return array
        }

        public async Task<int> GetCrimeDataAsync(string stateAbbr, int fromYear, int toYear)
        {
            int numViolentCrimes;

            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://api.usa.gov/crime/fbi/sapi/api/estimates/states/{stateAbbr}/{fromYear}/{toYear}?API_KEY=UuufzrJcLkpnfLavPuDqoY8ewleefZPsSKljRfhm"),
            };

            CrimeData crimeData;
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                Console.WriteLine(body);
                crimeData = JsonConvert.DeserializeObject<CrimeData>(body.ToString());
            }

            if (crimeData?.results[0]?.violent_crime == null)
            {
                numViolentCrimes = -1;
            }
            else
            {
                numViolentCrimes = crimeData.results[0].violent_crime;
                Console.WriteLine(numViolentCrimes);
            }

            return numViolentCrimes;
        }

        public async Task<double> GetWindDataAsync(double longitude, double latitude)
        {
            double index;

            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://weather-by-api-ninjas.p.rapidapi.com/v1/weather?lat={latitude}&lon={longitude}"),
                Headers =
                {
                    { "X-RapidAPI-Key", "acaf2e3e17msha39b9b650311c63p1c7c14jsn24b28d8c5755" },
                    { "X-RapidAPI-Host", "weather-by-api-ninjas.p.rapidapi.com" },
                },
            };

            WindData windData;
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                Console.WriteLine(body);
                windData = JsonConvert.DeserializeObject<WindData>(body.ToString());
            }



            if (windData?.wind_speed == null)
            {
                index = 9999;
            }
            else
            {
                index = windData.wind_speed;
                Console.WriteLine(index);
            }

            return index;
        }

        public async Task<double> GetSolarData(double longitude, double latitude)
        {
            double index;

            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://developer.nrel.gov/api/solar/solar_resource/v1.json?api_key=0egQkRpYc4jXn7tAA5jeu3rORhvGzfHSOe5LzQjh&lat={latitude}&lon={longitude}"),
            };

            SolarData solarData;
            SolarOutputs solarOutputs;
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                Console.WriteLine(body);
                solarOutputs = JsonConvert.DeserializeObject<SolarOutputs>(body.ToString());
                solarData = solarOutputs.outputs;
            }


            if (solarData?.avg_dni?.annual == null)
            {
                index = 9999;
            }
            else
            {
                index = solarData.avg_dni.annual;
                Console.WriteLine(index);
            }

            return index;
        }

    }
}
