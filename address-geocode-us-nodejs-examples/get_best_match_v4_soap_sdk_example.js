import { GetBestMatchV4Soap } from "../address-geocode-us-nodejs/SOAP/get_best_match_v4_soap.js";

async function GetBestMatchV4SoapGo(licenseKey, isLive) {
    console.log("\n----------------------------------------------");
    console.log("Address Geocode US - GetBestMatchV4 - SOAP SDK");
    console.log("----------------------------------------------");

    const address = "136 W Canon Perdido St Ste D";
    const city = "Santa Barbara";
    const state = "CA";
    const postalCode = "93101";
    const timeoutSeconds = 15;

    console.log("\n* Input *\n");
    console.log(`Address        : ${address}`);
    console.log(`City           : ${city}`);
    console.log(`State          : ${state}`);
    console.log(`PostalCode     : ${postalCode}`);
    console.log(`License Key    : ${licenseKey}`);
    console.log(`Is Live        : ${isLive}`);
    console.log(`Timeout Seconds: ${timeoutSeconds}`);

    try
    {
        const agus = new GetBestMatchV4Soap(address, city, state, postalCode, licenseKey, isLive, timeoutSeconds);
        const response = await agus.invokeAsync();
    
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
    }
    catch (e)
    {
        console.log("\n* Error *\n");
        console.log(`Error Message: ${e.message}`);
    }
}

export {GetBestMatchV4SoapGo };
