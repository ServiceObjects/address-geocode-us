import { GetBestMatchV4ClientGo } from "./get_best_match_v4_rest_sdk_example.js";
import { GetDistanceToWaterClientGo } from "./get_distance_to_water_rest_sdk_example.js";
import { GetReverseLocationClientGo } from "./get_reverse_location_rest_sdk_example.js";
import { GetBestMatchV4SoapGo } from "./get_best_match_v4_soap_sdk_example.js";
import { GetDistanceToWaterSoapGo } from "./get_distance_to_water_soap_sdk_example.js";
import { GetReverseLocationSoapGo } from "./get_reverse_location_soap_sdk_example.js";


export async function main() {

    //Your license key from Service Objects.
    //Trial license keys will only work on the
    //trail environments and production license
    //keys will only work on production environments.
    const licenseKey = "LICENSE KEY";
    const isLive = true;

    // Address Geocode US - GetBestMatchV4 - REST SDK
    await GetBestMatchV4ClientGo(licenseKey, isLive);

    // Address Geocode US - GetBestMatchV4 - SOAP SDK
    await GetBestMatchV4SoapGo(licenseKey, isLive);

    //Address Geocode US - GetDistanceToWater - REST SDK
    await GetDistanceToWaterClientGo(licenseKey, isLive);

    //Address Geocode US - GetDistanceToWater - SOAP SDK
    await GetDistanceToWaterSoapGo(licenseKey, isLive);

    //Address Geocode US - GetReverseLocation - REST SDK
    await GetReverseLocationClientGo(licenseKey, isLive);

    // Address Geocode US - GetDistanceToWater - SOAP SDK
    await GetReverseLocationSoapGo(licenseKey, isLive);
}
main().catch((error) => {
  console.error("An error occurred:", error);
  process.exit(1);
});
