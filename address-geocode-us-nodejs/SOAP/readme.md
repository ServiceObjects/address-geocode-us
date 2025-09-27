![Service Objects Logo](https://www.serviceobjects.com/wp-content/uploads/2021/05/SO-Logo-with-TM.gif "Service Objects Logo")

# AGUS - Address Geocode – US

DOTS Address Geocode – US is a publicly available XML and JSON web service that provides latitude/longitude and metadata information about a physical US address. The service provides geocoding information, such as the latitude and longitude location of a US address, along with demographic information, such as the census tract, block and other metadata.

DOTS Address Geocode – US can help provide instant address locations to websites or enhancement to contact lists.

## [Service Objects Website](https://serviceobjects.com)

# AGUS - GetBestMatches_V4

Returns the latitude and longitude for a given US address. This operation will use cascading logic when an exact address match is not found and it will return the next closest level match available. 

The operation output is designed to allow the service to return new pieces of data as they become available without having to re-integrate. 

### [GetBestMatches_V4 Developer Guide/Documentation](https://www.serviceobjects.com/docs/dots-address-geocode-us/agus-operations/agus-getbestmatch_v4-recommended/)

## Library Usage

```
// 1. Build the input
//
//  Required fields:
//               address
//               city 
//               state
//               postalCode
//               licenseKey
//               isLive
// 
// Optional:
//        timeoutSeconds

import { GetBestMatchV4Soap } from "../address-geocode-us-nodejs/SOAP/get_best_match_v4_soap.js";

const address = "136 W Canon Perdido St Ste D";
const city = "Santa Barbara";
const state = "CA";
const postalCode = "93101";
const timeoutSeconds = 15;
const isLive = true;
const licenseKey = "YOUR LICENSE KEY";

// 2. Call the sync InvokeAsync() method.
const agus = new GetBestMatchV4Soap(address, city, state, postalCode, licenseKey, isLive, timeoutSeconds);
const response = await agus.invokeAsync();

// 3. Inspect results.
console.log("\n* Geocode Info *\n");
if (response)
{
    console.log(`Level            : ${response.Level}`);
    console.log(`Level Description: ${response.LevelDescription}`);
    console.log(`Latitude         : ${response.Latitude}`);
    console.log(`Longitude        : ${response.Longitude}`);
    console.log(`Zip              : ${response.Zip}`);

    console.log("\n* Information Components *\n");
    if (response?.InformationComponents.InformationComponent.length > 0)
    {
        const raw = response?.InformationComponents?.InformationComponent;
        const components = Array.from(raw ?? []);
        components.forEach((component, index) => {
            console.log(`${component.Name}: ${component.Value}`);
        });
    }
    else
    {
        console.log("No information components found.");
    }
}
else
{
    console.log("No geocode info found.");
}

if (response.Error)
{
    console.log("\n* Error *\n");
    console.log(`Error Description: ${response.Error.Desc}`);
    console.log(`Error Number     : ${response.Error.Number}`);
    console.log(`Error Location   : ${response.Error.Location}`);
}
```

# AGUS - GetDistanceToWater

Returns an estimated distance from a given latitude and longitude to the nearest saltwater.

### [GetDistanceToWater Developer Guide/Documentation](https://www.serviceobjects.com/docs/dots-address-geocode-us/agus-operations/agus-getdistancetowater/)

## Library Usage

```
// 1. Build the input
//
//  Required fields:
//               latitude
//               longitude
//               licenseKey
//               isLive
// 
// Optional:
//        timeoutSeconds

import { GetDistanceToWaterClient } from "../address-geocode-us-nodejs/REST/get_distance_to_water_rest.js";

const latitude = "34.419120";
const longitude = "-119.703421";
const timeoutSeconds = 15;
const isLive = true;
const licenseKey = "YOUR LICENSE KEY";

// 2. Call the sync InvokeAsync() method.
const agus = new GetDistanceToWaterSoap(latitude, longitude, licenseKey, isLive, timeoutSeconds);
const response =await agus.invokeAsync();

// 3. Inspect results.

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
```

# AGUS - GetReverseLocation

Returns an estimated address for a given latitude and longitude.

### [GetReverseLocation Developer Guide/Documentation](https://www.serviceobjects.com/docs/dots-address-geocode-us/agus-operations/agus-getreverselocation/)

## Library Usage

```
// 1. Build the input
//
//  Required fields:
//               latitude
//               longitude
//               licenseKey
//               isLive
// 
// Optional:
//        timeoutSeconds

import { GetReverseLocationSoap } from "../address-geocode-us-nodejs/SOAP/get_reverse_location_soap.js";

const latitude = "34.419120";
const longitude = "-119.703421";
const timeoutSeconds = 15;
const isLive = true;
const licenseKey = "YOUR LICENSE KEY";

// 2. Call the sync InvokeAsync() method.
const agus = new GetReverseLocationSoap(latitude, longitude, licenseKey, isLive, timeoutSeconds);
const response =await agus.invokeAsync();

// 3. Inspect results.
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
```
