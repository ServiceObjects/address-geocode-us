using address_geocode_us_dot_net.REST;
using System.ComponentModel;

namespace address_geocode_us_dot_net_examples
{
    public class GetDistanceToWaterRestSdkExample
    {
        public static void Go(string LicenseKey, bool IsLive)
        {
            Console.WriteLine("\r\n--------------------------------------------------");
            Console.WriteLine("Address Geocode US - GetDistanceToWater - REST SDK");
            Console.WriteLine("--------------------------------------------------");

            GetDistanceToWaterClient.GetDistanceToWaterInput getDistanceToWaterInput = new(
                Latitude: "34.419120",
                Longitude: "-119.703421",
                LicenseKey: LicenseKey,
                IsLive: IsLive,
                TimeoutSeconds: 15
            );

            Console.WriteLine("\r\n* Input *\r\n");
            Console.WriteLine($"Latitude    : {getDistanceToWaterInput.Latitude}");
            Console.WriteLine($"Longitude   : {getDistanceToWaterInput.Longitude}");
            Console.WriteLine($"License Key : {getDistanceToWaterInput.LicenseKey}");
            Console.WriteLine($"Is Live     : {getDistanceToWaterInput.IsLive}");

            GetDistanceToWaterResponse response = GetDistanceToWaterClient.Invoke(getDistanceToWaterInput);
            if (response.Error is null)
            {
                Console.WriteLine("\r\n* Distance Info *\r\n");
                Console.WriteLine($"MilesToWater          : {response.DistanceToWater}");
                Console.WriteLine($"Latitude              : {response.Latitude}");
                Console.WriteLine($"Longitude             : {response.Longitude}");
                Console.WriteLine($"ClosestWaterLatitude  : {response.WaterLat}");
                Console.WriteLine($"ClosestWaterLongitude : {response.WaterLon}");
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
