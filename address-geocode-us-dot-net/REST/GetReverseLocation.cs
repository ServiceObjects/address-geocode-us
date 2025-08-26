using System.Web;

namespace address_geocode_us_dot_net.REST
{
    /// <summary>
    /// Provides functionality to call the ServiceObjects Address Geocode US (AGUS) REST API's GetReverseLocation endpoint,
    /// retrieving the estimated address for a given latitude and longitude with fallback to a backup endpoint for reliability in live mode.
    /// </summary>
    public class GetReverseLocationClient
    {
        // Base URL constants: production, backup, and trial
        private const string LiveBaseUrl = "https://sws.serviceobjects.com/GCR/api.svc/json/";
        private const string BackupBaseUrl = "https://swsbackup.serviceobjects.com/GCR/api.svc/json/";
        private const string TrialBaseUrl = "https://trial.serviceobjects.com/GCR/api.svc/json/";

        /// <summary>
        /// Synchronously calls the GetReverseLocation REST endpoint to retrieve address information,
        /// attempting the primary endpoint first and falling back to the backup if the response is invalid
        /// (Error.Number == "4") in live mode.
        /// </summary>
        /// <param name="input">The input parameters including latitude, longitude, and license key.</param>
        /// <returns>Deserialized <see cref="GetReverseLocationResponse"/>.</returns>
        public static GetReverseLocationResponse Invoke(GetReverseLocationInput input)
        {
            // Use query string parameters so missing/optional fields don't break
            // the URL as path parameters would.
            string url = BuildUrl(input, input.IsLive ? LiveBaseUrl : TrialBaseUrl);
            GetReverseLocationResponse response = Helper.HttpGet<GetReverseLocationResponse>(url, input.TimeoutSeconds);

            // Fallback on error payload in live mode
            if (input.IsLive && !IsValid(response))
            {
                string fallbackUrl = BuildUrl(input, BackupBaseUrl);
                GetReverseLocationResponse fallbackResponse = Helper.HttpGet<GetReverseLocationResponse>(fallbackUrl, input.TimeoutSeconds);
                return fallbackResponse;
            }

            return response;
        }

        /// <summary>
        /// Asynchronously calls the GetReverseLocation REST endpoint to retrieve address information,
        /// attempting the primary endpoint first and falling back to the backup if the response is invalid
        /// (Error.Number == "4") in live mode.
        /// </summary>
        /// <param name="input">The input parameters including latitude, longitude, and license key.</param>
        /// <returns>Deserialized <see cref="GetReverseLocationResponse"/>.</returns>
        public static async Task<GetReverseLocationResponse> InvokeAsync(GetReverseLocationInput input)
        {
            // Use query string parameters so missing/optional fields don't break
            // the URL as path parameters would.
            string url = BuildUrl(input, input.IsLive ? LiveBaseUrl : TrialBaseUrl);
            GetReverseLocationResponse response = await Helper.HttpGetAsync<GetReverseLocationResponse>(url, input.TimeoutSeconds).ConfigureAwait(false);

            // Fallback on error payload in live mode
            if (input.IsLive && !IsValid(response))
            {
                string fallbackUrl = BuildUrl(input, BackupBaseUrl);
                GetReverseLocationResponse fallbackResponse = await Helper.HttpGetAsync<GetReverseLocationResponse>(fallbackUrl, input.TimeoutSeconds).ConfigureAwait(false);
                return fallbackResponse;
            }

            return response;
        }

        /// <summary>
        /// Builds the full request URL, including URL-encoded query string parameters.
        /// </summary>
        /// <param name="input">The input parameters for the API call.</param>
        /// <param name="baseUrl">The base URL (live, backup, or trial).</param>
        /// <returns>The complete URL with query string.</returns>
        public static string BuildUrl(GetReverseLocationInput input, string baseUrl)
        {
            string qs = $"GetReverseLocation?" +
                     $"Latitude={HttpUtility.UrlEncode(input.Latitude)}" +
                     $"&Longitude={HttpUtility.UrlEncode(input.Longitude)}" +
                     $"&LicenseKey={HttpUtility.UrlEncode(input.LicenseKey)}";
            return baseUrl + qs;
        }

        private static bool IsValid(GetReverseLocationResponse response) => response?.Error == null || response.Error.Number != "4";

        /// <summary>
        /// Input parameters for the GetReverseLocation API. This operation returns the estimated address
        /// for a given latitude and longitude, including city, state, ZIP code, and county.
        /// </summary>
        /// <param name="Latitude">The latitude of the location.</param>
        /// <param name="Longitude">The longitude of the location.</param>
        /// <param name="LicenseKey">Your license key to use the service.</param>
        /// <param name="IsLive">Option to use live service or trial service</param>
        /// <param name="TimeoutSeconds">Timeout, in seconds, for the call to the service.</param>
        public record GetReverseLocationInput(
            string Latitude = "",
            string Longitude = "",
            string LicenseKey = "",
            bool IsLive = true,
            int TimeoutSeconds = 15
        );
    }
}