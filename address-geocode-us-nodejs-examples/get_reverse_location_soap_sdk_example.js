import { GetReverseLocationSoap } from "../address-geocode-us-nodejs/SOAP/get_reverse_location_soap.js";

async function GetReverseLocationSoapGo(licenseKey, isLive) {
    console.log("\n--------------------------------------------------");
    console.log("Address Geocode US - GetReverseLocation - SOAP SDK");
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
        const agus = new GetReverseLocationSoap(latitude, longitude, licenseKey, isLive, timeoutSeconds);
        const response =await agus.invokeAsync();
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

        if (response.Error)
        {
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

export { GetReverseLocationSoapGo };