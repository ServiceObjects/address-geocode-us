using address_geocode_us_dot_net.REST;

namespace address_geocode_us_dot_net_examples
{
    public  class GetBestMatchV4RestSdkExample
    {
        public static void Go(string LicenseKey, bool IsLive)
        {
            Console.WriteLine("\r\n----------------------------------------------");
            Console.WriteLine("Address Geocode US - GetBestMatchV4 - REST SDK");
            Console.WriteLine("----------------------------------------------");

            GetBestMatchV4Client.GetBestMatchV4Input getBestMatchV4Input = new(
                Address: "136 W Canon Perdido St Ste D",
                City: "Santa Barbara",
                State: "CA",
                PostalCode: "93101",
                LicenseKey: LicenseKey,
                IsLive: IsLive,
                TimeoutSeconds: 15
            );

            Console.WriteLine("\r\n* Input *\r\n");
            Console.WriteLine($"Address      : {getBestMatchV4Input.Address}");
            Console.WriteLine($"City         : {getBestMatchV4Input.City}");
            Console.WriteLine($"State        : {getBestMatchV4Input.State}");
            Console.WriteLine($"Postal Code  : {getBestMatchV4Input.PostalCode}");
            Console.WriteLine($"License Key  : {getBestMatchV4Input.LicenseKey}");
            Console.WriteLine($"Is Live      : {getBestMatchV4Input.IsLive}");
        
            GetBestMatchV4Response response = GetBestMatchV4Client.Invoke(getBestMatchV4Input);
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
                Console.WriteLine($"Error Number  : {response.Error.Number}");
                Console.WriteLine($"Error Desc    : {response.Error.Desc}");
                Console.WriteLine($"Error Location: {response.Error.Location}");
            }
        }
    }
}
