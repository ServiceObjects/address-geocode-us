using System.Web;

namespace address_geocode_us_dot_net.REST
{
    /// <summary>
    /// Provides functionality to call the ServiceObjects Address Geocode US (AGUS) REST API's GetDistanceToWater endpoint,
    /// retrieving the estimated distance from a given latitude and longitude to the nearest saltwater with fallback to a backup endpoint
    /// for reliability in live mode.
    /// </summary>
    public class GetDistanceToWaterClient
    {
        // Base URL constants: production, backup, and trial
        private const string LiveBaseUrl = "https://sws.serviceobjects.com/GCR/api.svc/json/";
        private const string BackupBaseUrl = "https://swsbackup.serviceobjects.com/GCR/api.svc/json/";
        private const string TrialBaseUrl = "https://trial.serviceobjects.com/GCR/api.svc/json/";

        /// <summary>
        /// Synchronously calls the GetDistanceToWater REST endpoint to retrieve distance information,
        /// attempting the primary endpoint first and falling back to the backup if the response is invalid
        /// (Error.Number == "4") in live mode.
        /// </summary>
        /// <param name="input">The input parameters including latitude, longitude, and license key.</param>
        /// <returns>Deserialized <see cref="GetDistanceToWaterResponse"/> containing distance data or an error.</returns>
        public static GetDistanceToWaterResponse Invoke(GetDistanceToWaterInput input)
        {
            //Use query string parameters so missing/options fields don't break
            //the URL as path parameters would.
            string url = BuildUrl(input, input.IsLive ? LiveBaseUrl : TrialBaseUrl);
            GetDistanceToWaterResponse response = Helper.HttpGet<GetDistanceToWaterResponse>(url, input.TimeoutSeconds);
            if (input.IsLive && !IsValid(response))
            {
                string fallbackUrl = BuildUrl(input, BackupBaseUrl);
                GetDistanceToWaterResponse fallbackResponse = Helper.HttpGet<GetDistanceToWaterResponse>(fallbackUrl, input.TimeoutSeconds);
                return fallbackResponse;
            }

            return response;
        }

        /// <summary>
        /// Asynchronously calls the GetDistanceToWater REST endpoint to retrieve distance information,
        /// attempting the primary endpoint first and falling back to the backup if the response is invalid
        /// (Error.Number == "4") in live mode.
        /// </summary>
        /// <param name="input">The input parameters including latitude, longitude, and license key.</param>
        /// <returns>Deserialized <see cref="GetDistanceToWaterResponse"/> containing distance data or an error.</returns>
        public static async Task<GetDistanceToWaterResponse> InvokeAsync(GetDistanceToWaterInput input)
        {
            //Use query string parameters so missing/options fields don't break
            //the URL as path parameters would.
            string url = BuildUrl(input, input.IsLive ? LiveBaseUrl : TrialBaseUrl);
            GetDistanceToWaterResponse response = await Helper.HttpGetAsync<GetDistanceToWaterResponse>(url, input.TimeoutSeconds).ConfigureAwait(false);
            if (input.IsLive && !IsValid(response))
            {
                var fallbackUrl = BuildUrl(input, BackupBaseUrl);
                GetDistanceToWaterResponse fallbackResponse = await Helper.HttpGetAsync<GetDistanceToWaterResponse>(fallbackUrl, input.TimeoutSeconds).ConfigureAwait(false);
                return fallbackResponse;
            }

            return response;
        }

        /// <summary>
        /// Builds the full request URL for the GetDistanceToWater endpoint, including URL-encoded query string parameters.
        /// </summary>
        /// <param name="input">The input parameters for the API call.</param>
        /// <param name="baseUrl">The base URL (live, backup, or trial).</param>
        /// <returns>The complete URL with query string.</returns>
        public static string BuildUrl(GetDistanceToWaterInput input, string baseUrl)
        {
            // Construct query string with URL-encoded parameters
            string qs = $"GetDistanceToWater?" +
                     $"Latitude={HttpUtility.UrlEncode(input.Latitude)}" +
                     $"&Longitude={HttpUtility.UrlEncode(input.Longitude)}" +
                     $"&LicenseKey={HttpUtility.UrlEncode(input.LicenseKey)}";
            return baseUrl + qs;
        }

        /// <summary>
        /// Checks if the API response is valid by ensuring it does not contain a fatal Error Code 4.
        /// </summary>
        /// <param name="response">The API response to validate.</param>
        /// <returns>True if the response is valid (no error or not Error Code 4), otherwise false.</returns>
        private static bool IsValid(GetDistanceToWaterResponse response) => response?.Error == null || response.Error.Number != "4";

        /// <summary>
        /// Input parameters for the GetDistanceToWater API call. Represents coordinates to measure
        /// the distance to the nearest saltwater, along with authentication details.
        /// </summary>
        /// <param name="Latitude">The latitude of the location (e.g., "34.0522").</param>
        /// <param name="Longitude">The longitude of the location (e.g., "-118.2437").</param>
        /// <param name="LicenseKey">The license key to authenticate the API request.</param>
        /// <param name="IsLive">Indicates whether to use the live service (true) or trial service (false).</param>
        /// <param name="TimeoutSeconds">Timeout duration for the API call, in seconds.</param>
        public record GetDistanceToWaterInput(
            string Latitude = "",
            string Longitude = "",
            string LicenseKey = "",
            bool IsLive = true,
            int TimeoutSeconds = 15
        );
    }
}