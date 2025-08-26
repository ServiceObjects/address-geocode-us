import { GetDistanceToWaterClient } from "../address-geocode-us-nodejs/REST/get_distance_to_water_rest.js";

async function GetDistanceToWaterClientGo(licenseKey, isLive) {
    console.log("\n--------------------------------------------------");
    console.log("Address Geocode US - GetDistanceToWater - REST SDK");
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
        const response = await GetDistanceToWaterClient.invoke(latitude, longitude, licenseKey, isLive, timeoutSeconds);
        console.log("\n* Distance Info *\n");
        if (response)
        {
            console.log(`MilesToWater         : ${response.DistanceToWater}`);
            console.log(`Latitude             : ${response.Latitude}`);
            console.log(`Longitude            : ${response.Longitude}`);
            console.log(`ClosestWaterLatitude : ${response.WaterLat}`);
            console.log(`ClosestWaterLongitude: ${response.WaterLon}`);
        }
        else
        {
            console.log("No distance info found.");
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

export { GetDistanceToWaterClientGo };