using address_geocode_us_dot_net.REST;

namespace address_geocode_us_dot_net_examples
{
    public static class GetReverseLocationRestSdkExample
    {
        public static void Go(string licenseKey, bool isLive)
        {
            Console.WriteLine("\r\n--------------------------------------------------");
            Console.WriteLine("Address Geocode US - GetReverseLocation - REST SDK");
            Console.WriteLine("--------------------------------------------------");

            GetReverseLocationClient.GetReverseLocationInput getReverseLocationInput = new(
                Latitude: "34.419061",
                Longitude: "-119.702139",
                LicenseKey: licenseKey,
                IsLive: isLive,
                TimeoutSeconds: 15
            );

            Console.WriteLine("\r\n* Input *\r\n");
            Console.WriteLine($"Latitude    : {getReverseLocationInput.Latitude}");
            Console.WriteLine($"Longitude   : {getReverseLocationInput.Longitude}");
            Console.WriteLine($"License Key : {getReverseLocationInput.LicenseKey}");
            Console.WriteLine($"Is Live     : {getReverseLocationInput.IsLive}");

            GetReverseLocationResponse response = GetReverseLocationClient.Invoke(getReverseLocationInput);
            if (response.Error == null)
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
                Console.WriteLine("\r\n* Error *\r\n");
                Console.WriteLine($"Error Number  : {response.Error.Number}");
                Console.WriteLine($"Error Desc    : {response.Error.Desc}");
                Console.WriteLine($"Error Location: {response.Error.Location}");
            }
        }
    }
}