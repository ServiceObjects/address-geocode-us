using AGUSService;
using address_geocode_us_dot_net.SOAP;

namespace address_geocode_us_dot_net_examples
{
    public static class GetBestMatchV4SoapSdkExample
    {
        public static void Go(string LicenseKey, bool IsLive)
        {
            Console.WriteLine("\r\n----------------------------------------------");
            Console.WriteLine("Address Geocode US - GetBestMatchV4 - SOAP SDK");
            Console.WriteLine("----------------------------------------------");

            string Adress = "136 W Canon Perdido St Ste D";
            string City = "Santa Barbara";
            string State = "CA";
            string PostalCode = "93101";

            Console.WriteLine("\r\n* Input *\r\n");
            Console.WriteLine($"Address      : {Adress}");
            Console.WriteLine($"City         : {City}");
            Console.WriteLine($"State        : {State}");
            Console.WriteLine($"Postal Code  : {PostalCode}");
            Console.WriteLine($"License Key  : {LicenseKey}");
            Console.WriteLine($"Is Live      : {IsLive}");

            GetBestMatchV4Validation getBestMatchV4Validation = new(IsLive);

            Location_V4 response = getBestMatchV4Validation.GetBestMatchV4(Adress, City, State, PostalCode, LicenseKey).Result;

            if (response.Error is null)
            {

                Console.WriteLine("\r\n* Geocode Info *\r\n");
                Console.WriteLine($"Level            : {response.Level}");
                Console.WriteLine($"Level Description: {response.LevelDescription}");
                Console.WriteLine($"Latitude         : {response.Latitude}");
                Console.WriteLine($"Longitude        : {response.Longitude}");
                Console.WriteLine($"Zip              : {response.Zip}");

                Console.WriteLine("\r\n* Information Components *\r\n");
                if (response.InformationComponents?.Length > 0)
                {
                    foreach (var component in response.InformationComponents)
                    {
                        Console.WriteLine($"{component.Name}: {component.Value}");
                    }
                }
                else
                {
                    Console.WriteLine("No information components found.");
                }
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