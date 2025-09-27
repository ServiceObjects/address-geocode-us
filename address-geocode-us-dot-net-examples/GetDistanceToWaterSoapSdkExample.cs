using address_geocode_us_dot_net.SOAP;
using AGUSService;
using System.ComponentModel;

namespace address_geocode_us_dot_net_examples
{
    public static class GetDistanceToWaterSoapSdkExample
    {
        public static void Go(string LicenseKey, bool IsLive)
        {
            Console.WriteLine("\r\n--------------------------------------------------");
            Console.WriteLine("Address Geocode US - GetDistanceToWater - SOAP SDK");
            Console.WriteLine("--------------------------------------------------");

            string Latitude = "34.419120";
            string Longitude = "-119.703421";

            Console.WriteLine("\r\n* Input *\r\n");
            Console.WriteLine($"Latitude    : {Latitude}");
            Console.WriteLine($"Longitude   : {Longitude}");
            Console.WriteLine($"License Key : {LicenseKey}");
            Console.WriteLine($"Is Live     : {IsLive}");

            GetDistanceToWaterValidation getDistanceToWaterValidation = new(IsLive);
            DistanceToWaterInfo response = getDistanceToWaterValidation.GetDistanceToWater(Latitude, Longitude, LicenseKey).Result;

            if (response.Error is null)
            {
                Console.WriteLine("\r\n* Distance To Water Info *\r\n");
                Console.WriteLine($"Distance To Water: {response.DistanceToWater}");
                Console.WriteLine($"Latitude         : {response.Latitude}");
                Console.WriteLine($"Longitude        : {response.Longitude}");
                Console.WriteLine($"Water Latitude   : {response.WaterLat}");
                Console.WriteLine($"Water Longitude  : {response.WaterLon}");
            }
            else
            {
                Console.WriteLine("\r\n* Error *\r\n");
                Console.WriteLine($"Error Description: {response.Error.Desc}");
                Console.WriteLine($"Error Number     : {response.Error.Number}");
                Console.WriteLine($"Error Location   : {response.Error.Location}");
            }
        }
    }
}