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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        public double basePrice;
        public double updatedPrice;
        public int avgCostPerSqft = 222;
        public double GetBasePrice(int sqft)
        {
            if(sqft >= 0)
            {
                try
                {
                    basePrice = sqft * avgCostPerSqft;
                }
                catch
                {
                    basePrice = -1;
                }
            }
            else
            {
                basePrice = -1;
            }
            return basePrice;
        }

        public string GetPriceAdjusted(int sqft, double crimeNum)
        {
            string results;

            basePrice = GetBasePrice(sqft);
            
            //average number of violent crimes per state from 2020 to 2021 was 25,000 according to FBI reports
            if(crimeNum > 25000)
            {
                crimeNum = crimeNum - 25000;
                updatedPrice = basePrice - (crimeNum * 10);
            }
            else if (crimeNum < 25000)
            {
                crimeNum = 25000 - crimeNum;
                updatedPrice = basePrice + (crimeNum * 10);
            }
            else
            {
                updatedPrice = basePrice;
            }

            results = updatedPrice.ToString();
            return results;
        }

        public string GetPriceListing(string targetFilePath, string fullAddress, int sqft, double basePrice, double adjustedPrice, double solarRating, double windRating, int numCrimes)
        {
            string response = "File Not Made. Possible error in parameter values.";

            
            //The group of lines below create a text file at the targetFilePath location using the CreateText() method.
            using (StreamWriter write = File.CreateText(targetFilePath))
            {
                write.WriteLine("This beautiful " + sqft + " square foot home located at " + fullAddress + " had an original price of:");
                write.WriteLine("$" + basePrice);
                write.WriteLine();
                if (numCrimes > 0)
                {
                    write.WriteLine("After discovering that this state has had " + numCrimes + " number of violent crimes more than the national average, we have adjusted this property's price to:");
                    write.WriteLine("$" + adjustedPrice);
                }
                else if (numCrimes < 0)
                {
                    write.WriteLine("After discovering that this state has had " + Math.Abs(numCrimes) + " number of violent crimes less than the national average, we have adjusted this property's price to:");
                    write.WriteLine("$" + adjustedPrice);
                }
                else
                {
                    write.WriteLine("After discovering that this state has had the same number of violent crimes as the national average, we have adjusted this property's price to:");
                    write.WriteLine("$" + adjustedPrice);
                }
                write.WriteLine();
                write.WriteLine("This stunning piece of property gets an annual solar index rating of " + solarRating + " out of 10.");
                if (solarRating > 5)
                {
                    write.WriteLine("Because of this, solar paneling is a feasible option for this piece of property.");
                }
                else
                {
                    write.WriteLine("Because of this, solar paneling is not a feasible option for this piece of property.");
                }
                write.WriteLine();
                write.WriteLine("This property also gets an annual average wind speed rating of " + windRating + " out of 10.");
                if (windRating > 5)
                {
                    write.WriteLine("Because of this, windmill energy is a feasible option for this piece of property.");
                }
                else
                {
                    write.WriteLine("Because of this, windmill energy is not a feasible option for this piece of property.");
                }
            }
            response = "File has been created at the specified File Path.";
            return response;
            
        }
    }
}
