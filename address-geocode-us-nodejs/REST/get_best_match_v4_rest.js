import axios from 'axios';
import querystring from 'querystring';
import { GetBestMatchV4Response } from './agus_response.js';

/**
 * @constant
 * @type {string}
 * @description The base URL for the live ServiceObjects Address Geocode US (AGUS) API service.
 */
const liveBaseUrl = 'https://sws.serviceobjects.com/GCR/api.svc/json/';

/**
 * @constant
 * @type {string}
 * @description The base URL for the backup ServiceObjects Address Geocode US (AGUS) API service.
 */
const backupBaseUrl = 'https://swsbackup.serviceobjects.com/GCR/api.svc/json/';

/**
 * @constant
 * @type {string}
 * @description The base URL for the trial ServiceObjects Address Geocode US (AGUS) API service.
 */
const trialBaseUrl = 'https://trial.serviceobjects.com/GCR/api.svc/json/';

/**
 * <summary>
 * Checks if a response from the API is valid by verifying that it either has no Error object
 * or the Error.Number is not equal to '4'.
 * </summary>
 * <param name="response" type="Object">The API response object to validate.</param>
 * <returns type="boolean">True if the response is valid, false otherwise.</returns>
 */
const isValid = (response) => !response?.Error || response.Error.Number !== '4';

/**
 * <summary>
 * Constructs a full URL for the GetBestMatch_V4 API endpoint by combining the base URL
 * with query parameters derived from the input parameters.
 * </summary>
 * <param name="params" type="Object">An object containing all the input parameters.</param>
 * <param name="baseUrl" type="string">The base URL for the API service (live, backup, or trial).</param>
 * <returns type="string">The constructed URL with query parameters.</returns>
 */
const buildUrl = (params, baseUrl) =>
    `${baseUrl}GetBestMatch_V4?${querystring.stringify(params)}`;

/**
 * <summary>
 * Performs an HTTP GET request to the specified URL with a given timeout.
 * </summary>
 * <param name="url" type="string">The URL to send the GET request to.</param>
 * <param name="timeoutSeconds" type="number">The timeout duration in seconds for the request.</param>
 * <returns type="Promise<GetBestMatchV4Response>">A promise that resolves to a GetBestMatchV4Response object containing the API response data.</returns>
 * <exception cref="Error">Thrown if the HTTP request fails, with a message detailing the error.</exception>
 */
const httpGet = async (url, timeoutSeconds) => {
    try {
        const response = await axios.get(url, { timeout: timeoutSeconds * 1000 });
        return new GetBestMatchV4Response(response.data);
    } catch (error) {
        throw new Error(`HTTP request failed: ${error.message}`);
    }
};

/**
 * <summary>
 * Provides functionality to call the ServiceObjects Address Geocode US (AGUS) API's GetBestMatch_V4 endpoint,
 * retrieving geocoding information (e.g., latitude, longitude, ZIP code) for a given US address with fallback to a backup endpoint for reliability in live mode.
 * </summary>
 */
const GetBestMatchV4Client = {
    /**
     * <summary>
     * Asynchronously invokes the GetBestMatch_V4 API endpoint, attempting the primary endpoint
     * first and falling back to the backup if the response is invalid (Error.Number == '4') in live mode.
     * </summary>
     * @param {string} Address - Address line of the address to geocode (e.g., "123 Main Street").
     * @param {string} City - The city of the address to geocode. For example, “New York”. The city isn’t required, but if one is not provided, the Zip code is required..
     * @param {string} State - The state of the address to geocode. For example, “NY”. This does not need to be contracted, full state names will work as well. The state isn’t required, but if one is not provided, the Zip code is required..
     * @param {string} PostalCode - The state of the address to geocode. For example, “NY”. This does not need to be contracted, full state names will work as well. The state isn’t required, but if one is not provided, the Zip code is required..
     * @param {string} LicenseKey - Your license key to use the service.
     * @param {boolean} isLive - Value to determine whether to use the live or trial servers.
     * @param {number} timeoutSeconds - Timeout, in seconds, for the call to the service.
     * @returns {Promise<GetBestMatchV4Response>} - A promise that resolves to a GetBestMatchV4Response object.
     */
    async invokeAsync(Address, City, State, PostalCode, LicenseKey, isLive = true, timeoutSeconds = 15) {
        const params = {
            Address,
            City,
            State,
            PostalCode,
            LicenseKey
        };

        const url = buildUrl(params, isLive ? liveBaseUrl : trialBaseUrl);
        let response = await httpGet(url, timeoutSeconds);

        if (isLive && !isValid(response)) {
            const fallbackUrl = buildUrl(params, backupBaseUrl);
            const fallbackResponse = await httpGet(fallbackUrl, timeoutSeconds);
            return fallbackResponse;
        }
        return response;
    },

    /**
     * <summary>
     * Synchronously invokes the GetBestMatch_V4 API endpoint by wrapping the async call
     * and awaiting its result immediately.
     * </summary>
     * @param {string} Address - Address line of the address to geocode (e.g., "123 Main Street").
     * @param {string} City - The city of the address to geocode. For example, “New York”. The city isn’t required, but if one is not provided, the Zip code is required..
     * @param {string} State - The state of the address to geocode. For example, “NY”. This does not need to be contracted, full state names will work as well. The state isn’t required, but if one is not provided, the Zip code is required..
     * @param {string} PostalCode - The state of the address to geocode. For example, “NY”. This does not need to be contracted, full state names will work as well. The state isn’t required, but if one is not provided, the Zip code is required..
     * @param {string} LicenseKey - Your license key to use the service.
     * @param {boolean} isLive - Value to determine whether to use the live or trial servers.
     * @param {number} timeoutSeconds - Timeout, in seconds, for the call to the service.
     * @returns {GetBestMatchV4Response} - A GetBestMatchV4Response object with geocoding details or an error.
     */
    invoke(Address, City, State, PostalCode, LicenseKey, isLive = true, timeoutSeconds = 15) {
        return (async () => await this.invokeAsync(
            Address, City, State, PostalCode, LicenseKey, isLive, timeoutSeconds
        ))();
    }
};

export { GetBestMatchV4Client, GetBestMatchV4Response };