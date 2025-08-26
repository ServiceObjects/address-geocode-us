using AGUSService;
using System.Text.RegularExpressions;

namespace address_geocode_us_dot_net.SOAP
{
    /// <summary>
    /// Provides functionality to call the ServiceObjects Address Geocode US (AGUS) SOAP service's GetBestMatch_V4 operation,
    /// retrieving geocoding information (e.g., latitude, longitude, ZIP code) for a given US address with fallback to a backup endpoint
    /// for reliability in live mode.
    /// </summary>
    public class GetBestMatchV4Validation
    {
        private const string LiveBaseUrl = "https://sws.serviceobjects.com/GCR/soap.svc/SOAP";
        private const string BackupBaseUrl = "https://swsbackup.serviceobjects.com/GCR/soap.svc/SOAP";
        private const string TrialBaseUrl = "https://trial.serviceobjects.com/GCR/soap.svc/SOAP";

        private readonly string _primaryUrl;
        private readonly string _backupUrl;
        private readonly int _timeoutMs;
        private readonly bool _isLive;

        /// <summary>
        /// Initializes URLs/timeout/IsLive.
        /// </summary>
        public GetBestMatchV4Validation(bool isLive)
        {
            _timeoutMs = 10000;
            _isLive = isLive;

            _primaryUrl = isLive ? LiveBaseUrl : TrialBaseUrl;
            _backupUrl = isLive ? BackupBaseUrl : TrialBaseUrl;

            if (string.IsNullOrWhiteSpace(_primaryUrl))
                throw new InvalidOperationException("Primary URL not set.");
            if (string.IsNullOrWhiteSpace(_backupUrl))
                throw new InvalidOperationException("Backup URL not set.");
        }

        /// <summary>
        /// Async, returns the latitude and longitude for a given US address. This operation will use cascading logic when an exact address match is not found and it will return the next closest level match available. The operation output is designed to allow the service to return new pieces of data as they become available without having to re-integrate.
        /// </summary>
        /// <param name="Address">Address line of the address to geocode. For example, “123 Main Street”.</param>
        /// <param name="City">The city of the address to geocode. For example, “New York”. The city isn’t required, but if one is not provided, the Zip code is required.</param>
        /// <param name="State">The state of the address to geocode. For example, “NY”. This does not need to be contracted, full state names will work as well. The state isn’t required, but if one is not provided, the Zip code is required.</param>
        /// <param name="PostalCode">The zip code of the address to geocode. A zip code isn’t required, but if one is not provided, the City and State are required.</param>
        /// <param name="LicenseKey">Your license key to use the service.</param>
        public async Task<Location_V4> GetBestMatchV4(string Address, string City, string State, string PostalCode, string LicenseKey)
        {
            GCRSoapServiceClient clientPrimary = null;
            GCRSoapServiceClient clientBackup = null;

            try
            {
                // Attempt Primary
                clientPrimary = new GCRSoapServiceClient();
                clientPrimary.Endpoint.Address = new System.ServiceModel.EndpointAddress(_primaryUrl);
                clientPrimary.InnerChannel.OperationTimeout = TimeSpan.FromMilliseconds(_timeoutMs);

                Location_V4 response = await clientPrimary.GetBestMatch_V4Async(
                    Address, City, State, PostalCode, LicenseKey).ConfigureAwait(false);

                if (_isLive && !IsValid(response))
                {
                    throw new InvalidOperationException("Primary endpoint returned null or a fatal Number=4 error for GetBestMatch_V4");
                }
                return response;
            }
            catch (Exception primaryEx)
            {

                try
                {
                    clientBackup = new GCRSoapServiceClient();
                    clientBackup.Endpoint.Address = new System.ServiceModel.EndpointAddress(_backupUrl);
                    clientBackup.InnerChannel.OperationTimeout = TimeSpan.FromMilliseconds(_timeoutMs);

                    return await clientBackup.GetBestMatch_V4Async(
                        Address, City, State, PostalCode, LicenseKey).ConfigureAwait(false);
                }
                catch (Exception backupEx)
                {
                    throw new InvalidOperationException(
                        $"Both primary and backup endpoints failed.\n" +
                        $"Primary error: {primaryEx.Message}\n" +
                        $"Backup error: {backupEx.Message}");
                }
                finally
                {
                    clientBackup?.Close();
                }
            }
            finally
            {
                clientPrimary?.Close();
            }
        }

        private static bool IsValid(Location_V4 response) => response?.Error == null || response.Error.Number != "4";
    }
}