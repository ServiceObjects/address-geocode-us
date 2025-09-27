using AGUSService;
using address_geocode_us_dot_net.SOAP;

namespace address_geocode_us_dot_net_examples
{
    public static class GetReverseLocationSoapSdkExample
    {
        public static void Go(string LicenseKey, bool IsLive)
        {
            Console.WriteLine("\r\n--------------------------------------------------");
            Console.WriteLine("Address Geocode US - GetReverseLocation - SOAP SDK");
            Console.WriteLine("--------------------------------------------------");

            string Latitude = "34.419061";
            string Longitude = "-119.702139";

            Console.WriteLine("\r\n* Input *\r\n");
            Console.WriteLine($"Latitude    : {Latitude}");
            Console.WriteLine($"Longitude   : {Longitude}");
            Console.WriteLine($"License Key : {LicenseKey}");
            Console.WriteLine($"Is Live     : {IsLive}");

            GetReverseLocationValidation getReverseLocationValidation = new(IsLive);
            ReverseAddress response = getReverseLocationValidation.GetReverseLocation(Latitude, Longitude, LicenseKey).Result;
            
            if (response.Error is null)
            {
                Console.WriteLine("\r\n* Reverse Location Info *\r\n");
                Console.WriteLine($"Address : {response.Address}");
                Console.WriteLine($"City    : {response.City}");
                Console.WriteLine($"County  : {response.County}");
                Console.WriteLine($"State   : {response.State}");
                Console.WriteLine($"Zip     : {response.Zip}");
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