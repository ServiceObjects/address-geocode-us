import { soap } from 'strong-soap';
import { GetReverseLocationResponse } from './agus_response.js';

/**
 * <summary>
 * A class that provides functionality to call the ServiceObjects Address Geocode US (AGUS) SOAP service's GetReverseLocation endpoint,
 * retrieving the estimated address for given coordinates with fallback to a backup endpoint for reliability in live mode.
 * </summary>
 */
class GetReverseLocationSoap {
    /**
     * <summary>
     * Initializes a new instance of the GetReverseLocation class with the provided input parameters,
     * setting up primary and backup WSDL URLs based on the live/trial mode.
     * </summary>
     * @param {string} Latitude - The latitude of the location.
     * @param {string} Longitude - The longitude of the location.
     * @param {string} LicenseKey - Your license key to use the service.
     * @param {boolean} isLive - Value to determine whether to use the live or trial servers.
     * @param {number} timeoutSeconds - Timeout, in seconds, for the call to the service.
     * @throws {Error} Thrown if LicenseKey is empty or null.
     */
    constructor(Latitude, Longitude, LicenseKey, isLive = true, timeoutSeconds = 15) {

        this.args = {
            Latitude,
            Longitude,
            LicenseKey
        };

        this.isLive = isLive;
        this.timeoutSeconds = timeoutSeconds;

        this.LiveBaseUrl = "https://sws.serviceobjects.com/gcr/soap.svc?wsdl";
        this.BackupBaseUrl = "https://swsbackup.serviceobjects.com/gcr/soap.svc?wsdl";
        this.TrialBaseUrl = "https://trial.serviceobjects.com/gcr/soap.svc?wsdl";

        this._primaryWsdl = this.isLive ? this.LiveBaseUrl : this.TrialBaseUrl;
        this._backupWsdl = this.isLive ? this.BackupBaseUrl : this.TrialBaseUrl;
    }

    /**
     * <summary>
     * Asynchronously calls the GetReverseLocation SOAP endpoint, attempting the primary endpoint
     * first and falling back to the backup if the response is invalid (Error.Number == '4') in live mode
     * or if the primary call fails.
     * </summary>
     * <returns type="Promise<ReverseAddressResponse>">A promise that resolves to a ReverseAddressResponse object containing address details or an error.</returns>
     * <exception cref="Error">Thrown if both primary and backup calls fail, with detailed error messages.</exception>
     */
    async invokeAsync() {
        try {
            const primaryResult = await this._callSoap(this._primaryWsdl, this.args);

            if (this.isLive && !this._isValid(primaryResult)) {
                console.warn("Primary returned Error.Number == '4', falling back to backup...");
                const backupResult = await this._callSoap(this._backupWsdl, this.args);
                return backupResult;
            }

            return primaryResult;
        } catch (primaryErr) {
           
                try {
                    const backupResult = await this._callSoap(this._backupWsdl, this.args);
                    return backupResult;
                } catch (backupErr) {
                    throw new Error(`Both primary and backup calls failed:\nPrimary: ${primaryErr.message}\nBackup: ${backupErr.message}`);
                }
            } 
    }

    /**
     * <summary>
     * Performs a SOAP service call to the specified WSDL URL with the given arguments,
     * creating a client and processing the response into a ReverseAddressResponse object.
     * </summary>
     * <param name="wsdlUrl" type="string">The WSDL URL of the SOAP service endpoint (primary or backup).</param>
     * <param name="args" type="Object">The arguments to pass to the GetReverseLocation method.</param>
     * <returns type="Promise<ReverseAddressResponse>">A promise that resolves to a ReverseAddressResponse object containing the SOAP response data.</returns>
     * <exception cref="Error">Thrown if the SOAP client creation fails, the service call fails, or the response cannot be parsed.</exception>
     */
    _callSoap(wsdlUrl, args) {
        return new Promise((resolve, reject) => {
            soap.createClient(wsdlUrl, { timeout: this.timeoutSeconds * 1000 }, (err, client) => {
                if (err) return reject(err);

                client.GetReverseLocation(args, (err, result) => {
                    const rawData = result?.GetReverseLocationResult;
                    try {
                        if (!rawData) {
                            return reject(new Error("SOAP response is empty or undefined."));
                        }
                        const parsed = new GetReverseLocationResponse(rawData);
                        resolve(parsed);
                    } catch (parseErr) {
                        reject(new Error(`Failed to parse SOAP response: ${parseErr.message}`));
                    }
                });
            });
        });
    }

    /**
     * <summary>
     * Checks if a SOAP response is valid by verifying that it exists and either has no Error object
     * or the Error.Number is not equal to '4'.
     * </summary>
     * <param name="response" type="ReverseAddressResponse">The ReverseAddressResponse object to validate.</param>
     * <returns type="boolean">True if the response is valid, false otherwise.</returns>
     */
    _isValid(response) {
        return response && (!response.Error || response.Error.Number !== '4');
    }
}

export { GetReverseLocationSoap };