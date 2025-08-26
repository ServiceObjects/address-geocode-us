import { GetReverseLocationClient } from "../address-geocode-us-nodejs/REST/get_reverse_location_rest.js";

async function GetReverseLocationClientGo(licenseKey, isLive) {
    console.log("\n--------------------------------------------------");
    console.log("Address Geocode US - GetReverseLocation - REST SDK");
    console.log("--------------------------------------------------");

    const latitude = "34.419120";
    const longitude = "-119.703421";
    const timeoutSeconds = 15;

    console.log("\n* Input *\n");
    console.log(`Latitude       : ${latitude}`);
    console.log(`Longitude      : ${longitude}`);
    console.log(`License Key    : ${licenseKey}`);
    console.log(`Is Live        : ${isLive}`);
    console.log(`Timeout Seconds: ${timeoutSeconds}`);

    try
    {
        const response = await GetReverseLocationClient.invoke(latitude, longitude, licenseKey, isLive, timeoutSeconds);
        console.log("\n* Reverse Location Info *\n");
        if (response)
        {
            console.log(`Address   : ${response.Address}`);
            console.log(`City      : ${response.City}`);
            console.log(`County    : ${response.County}`);
            console.log(`State     : ${response.State}`);
            console.log(`Zip       : ${response.Zip}`);
        }
        else
        {
            console.log("No location info found.");
        }

        if (response.Error) {
            console.log("\n* Error *\n");
            console.log(`Error Description: ${response.Error.Desc}`);
            console.log(`Error Number     : ${response.Error.Number}`);
            console.log(`Error Location   : ${response.Error.Location}`);
        }
    }
    catch (e)
    {
        console.log("\n* Error *\n");
        console.log(`Error Message: ${e.message}`);
    }
}

export { GetReverseLocationClientGo };