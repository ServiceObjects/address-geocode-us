using AGUSService;
using address_geocode_us_dot_net.SOAP;

namespace address_geocode_us_dot_net_examples
{
    public static class GetReverseLocationSoapSdkExample
    {
        public static void Go(string licenseKey, bool isLive)
        {
            Console.WriteLine("\r\n--------------------------------------------------");
            Console.WriteLine("Address Geocode US - GetReverseLocation - SOAP SDK");
            Console.WriteLine("--------------------------------------------------");

            string latitude = "34.419061";
            string longitude = "-119.702139";

            Console.WriteLine("\r\n* Input *\r\n");
            Console.WriteLine($"Latitude    : {latitude}");
            Console.WriteLine($"Longitude   : {longitude}");
            Console.WriteLine($"License Key : {licenseKey}");
            Console.WriteLine($"Is Live     : {isLive}");

            GetReverseLocationValidation getReverseLocationValidation = new(isLive);
            ReverseAddress response = getReverseLocationValidation.GetReverseLocation(latitude, longitude, licenseKey).Result;
            
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