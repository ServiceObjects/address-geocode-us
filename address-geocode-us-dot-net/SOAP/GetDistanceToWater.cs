using AGUSService;

namespace address_geocode_us_dot_net.SOAP
{
    /// <summary>
    /// Provides functionality to call the ServiceObjects Address Geocode US (AGUS) SOAP service's GetDistanceToWater operation,
    /// retrieving the distance to the nearest body of water for given coordinates with fallback to a backup endpoint for reliability in live mode.
    /// </summary>
    public class GetDistanceToWaterValidation
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
        public GetDistanceToWaterValidation(bool isLive)
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
        /// This operation returns the distance to the nearest body of water (in miles) for a given latitude and longitude,
        /// along with the coordinates of the input location and the nearest water body. Sign up for a free trial key at
        /// </summary>
        /// <param name="Latitude">The latitude of the location.</param>
        /// <param name="Longitude">The longitude of the location.</param>
        /// <param name="LicenseKey">Your license key to use the service.</param>
        /// <returns>A <see cref="Task{DistanceToWaterInfo}"/> containing a <see cref="DistanceToWaterInfo"/> object with distance details or an error.</returns>
        /// <exception cref="Exception">Thrown if both primary and backup endpoints fail.</exception>
        public async Task<DistanceToWaterInfo> GetDistanceToWater(string Latitude, string Longitude, string LicenseKey)
        {
            GCRSoapServiceClient clientPrimary = null;
            GCRSoapServiceClient clientBackup = null;

            try
            {
                // Attempt Primary
                clientPrimary = new GCRSoapServiceClient();
                clientPrimary.Endpoint.Address = new System.ServiceModel.EndpointAddress(_primaryUrl);
                clientPrimary.InnerChannel.OperationTimeout = TimeSpan.FromMilliseconds(_timeoutMs);

                DistanceToWaterInfo response = await clientPrimary.GetDistanceToWaterAsync(
                    Latitude, Longitude, LicenseKey).ConfigureAwait(false);

                if (_isLive && !IsValid(response))
                {
                    throw new InvalidOperationException("Primary endpoint returned null or a fatal Number=4 error for GetDistanceToWater");
                }
                return response;
            }
            catch (Exception primaryEx)
            {
                // If primary fails, try backup in live mode

                try
                {
                    clientBackup = new GCRSoapServiceClient();
                    clientBackup.Endpoint.Address = new System.ServiceModel.EndpointAddress(_backupUrl);
                    clientBackup.InnerChannel.OperationTimeout = TimeSpan.FromMilliseconds(_timeoutMs);

                    return await clientBackup.GetDistanceToWaterAsync(
                        Latitude, Longitude, LicenseKey).ConfigureAwait(false);
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

        private static bool IsValid(DistanceToWaterInfo response) => response?.Error == null || response.Error.Number != "4";
    }
}