
using System.Web;

namespace address_geocode_us_dot_net.REST
{
    /// <summary>
    /// Provides functionality to call the ServiceObjects Address Geocode US (AGUS) REST API's GetBestMatch_V4 endpoint,
    /// retrieving geocoding info rmation (e.g., latitude, longitude, ZIP code) for a given US address with fallback to a backup endpoint
    /// for reliability in live mode.
    /// </summary>
    public static class GetBestMatchV4Client
    {
        // Base URL constants: production, backup, and trial
        private const string LiveBaseUrl = "https://sws.serviceobjects.com/GCR/api.svc/json/";
        private const string BackupBaseUrl = "https://swsbackup.serviceobjects.com/GCR/api.svc/json/";
        private const string TrialBaseUrl = "https://trial.serviceobjects.com/GCR/api.svc/json/";

        /// <summary>
        /// Synchronously calls the GetBestMatch_V4 REST endpoint to retrieve geocoding information,
        /// attempting the primary endpoint first and falling back to the backup if the response is invalid
        /// (Error.Number == "4") in live mode.
        /// </summary>
        /// <param name="input">The input parameters including address, city, state, postal code, and license key.</param>
        /// <returns>Deserialized <see cref="GetBestMatchV4Response"/> containing geocoding data or an error.</returns>
        public static GetBestMatchV4Response Invoke(GetBestMatchV4Input input)
        {
            //Use query string parameters so missing/options fields don't break
            //the URL as path parameters would.
            string url = BuildUrl(input, input.IsLive ? LiveBaseUrl : TrialBaseUrl);
            GetBestMatchV4Response response = Helper.HttpGet<GetBestMatchV4Response>(url, input.TimeoutSeconds);

            // Fallback on error in live mode
            if (input.IsLive && !IsValid(response))
            {
                string fallbackUrl = BuildUrl(input, BackupBaseUrl);
                GetBestMatchV4Response fallbackResponse = Helper.HttpGet<GetBestMatchV4Response>(fallbackUrl, input.TimeoutSeconds);
                return fallbackResponse;
            }

            return response;
        }

        /// <summary>
        /// Asynchronously calls the GetBestMatch_V4 REST endpoint to retrieve geocoding information,
        /// attempting the primary endpoint first and falling back to the backup if the response is invalid
        /// (Error.Number == "4") in live mode.
        /// </summary>
        /// <param name="input">The input parameters including address, city, state, postal code, and license key.</param>
        /// <returns>Deserialized <see cref="GetBestMatchV4Response"/> containing geocoding data or an error.</returns>
        public static async Task<GetBestMatchV4Response> InvokeAsync(GetBestMatchV4Input input)
        {
            //Use query string parameters so missing/options fields don't break
            //the URL as path parameters would.
            string url = BuildUrl(input, input.IsLive ? LiveBaseUrl : TrialBaseUrl);
            GetBestMatchV4Response response = await Helper.HttpGetAsync<GetBestMatchV4Response>(url, input.TimeoutSeconds).ConfigureAwait(false);

            // Fallback on error in live mode
            if (input.IsLive && !IsValid(response))
            {
                string fallbackUrl = BuildUrl(input, BackupBaseUrl);
                GetBestMatchV4Response fallbackResponse = await Helper.HttpGetAsync<GetBestMatchV4Response>(fallbackUrl, input.TimeoutSeconds).ConfigureAwait(false);
                return fallbackResponse;
            }

            return response;
        }

        /// <summary>
        /// Builds the full request URL for the GetBestMatch_V4 endpoint, including URL-encoded query string parameters.
        /// </summary>
        /// <param name="input">The input parameters for the API call.</param>
        /// <param name="baseUrl">The base URL (live, backup, or trial).</param>
        /// <returns>The complete URL with query string.</returns>
        public static string BuildUrl(GetBestMatchV4Input input, string baseUrl)
        {
            // Construct query string with URL-encoded parameters
            string qs = $"GetBestMatch_V4?" +
                     $"Address={HttpUtility.UrlEncode(input.Address)}" +
                     $"&City={HttpUtility.UrlEncode(input.City)}" +
                     $"&State={HttpUtility.UrlEncode(input.State)}" +
                     $"&PostalCode={HttpUtility.UrlEncode(input.PostalCode)}" +
                     $"&LicenseKey={HttpUtility.UrlEncode(input.LicenseKey)}";
            return baseUrl + qs;
        }
    
        private static bool IsValid(GetBestMatchV4Response response) => response?.Error == null || response.Error.Number != "4";

        /// <summary>
        /// Input parameters for the GetBestMatch_V4 API call. Represents a US address to geocode
        /// and returns latitude and longitude with cascading logic for partial matches.
        /// </summary>
        /// <param name="Address">Address line of the address to geocode (e.g., “123 Main Street”).</param>
        /// <param name="City">The city of the address to geocode (e.g., “New York”). Optional if postal code is provided.</param>
        /// <param name="State">The state of the address to geocode (e.g., “NY”). Optional if postal code is provided.</param>
        /// <param name="PostalCode">The ZIP code of the address to geocode. Optional if city and state are provided.</param>
        /// <param name="LicenseKey">The license key to authenticate the API request.</param>
        /// <param name="IsLive">Indicates whether to use the live service (true) or trial service (false).</param>
        /// <param name="TimeoutSeconds">Timeout duration for the API call, in seconds.</param>
        public record GetBestMatchV4Input(
            string Address = "",
            string City = "",
            string State = "",
            string PostalCode = "",
            string LicenseKey = "",
            bool IsLive = true,
            int TimeoutSeconds = 15
        );
    }
}