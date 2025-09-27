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
// 1 Instantiate the service wrapper
string IsLive = true;
GetBestMatchV4Validation getBestMatchV4Validation = new(IsLive);

// 2 Provide your input data
//  Required fields:
//               Address
//               City 
//               State
//               PostalCode
//               LicenseKey
//               IsLive
// 
// Optional:
//        TimeoutSeconds

string Adress = "136 W Canon Perdido St Ste D";
string City = "Santa Barbara";
string State = "CA";
string PostalCode = "93101";
string LicenseKey = "YOUR LICENSE KEY"

// 3 Call the service
Location_V4 response = getBestMatchV4Validation.GetBestMatchV4(Adress, City, State, PostalCode, LicenseKey).Result;

// 4. Inspect results.
if (response.Error is null)
{

    Console.WriteLine("\r\n* Geocode Info *\r\n");
    Console.WriteLine($"Level            : {response.Level}");
    Console.WriteLine($"Level Description: {response.LevelDescription}");
    Console.WriteLine($"Latitude         : {response.Latitude}");
    Console.WriteLine($"Longitude        : {response.Longitude}");
    Console.WriteLine($"Zip              : {response.Zip}");

    Console.WriteLine("\r\n* Information Components *\r\n");
    if (response.InformationComponents?.Length > 0)
    {
        foreach (var component in response.InformationComponents)
        {
            Console.WriteLine($"{component.Name}: {component.Value}");
        }
    }
    else
    {
        Console.WriteLine("No information components found.");
    }
}
else
{
    Console.WriteLine("\r\n* Error *\r\n");
    Console.WriteLine($"Error Description: {response.Error.Desc}");
    Console.WriteLine($"Error Number     : {response.Error.Number}");
    Console.WriteLine($"Error Location   : {response.Error.Location}");
}
```

# AGUS - GetDistanceToWater

Returns an estimated distance from a given latitude and longitude to the nearest saltwater.

### [GetDistanceToWater Developer Guide/Documentation](https://www.serviceobjects.com/docs/dots-address-geocode-us/agus-operations/agus-getdistancetowater/)

## Library Usage

```
// 1 Instantiate the service wrapper
string IsLive = true;
GetDistanceToWaterValidation getDistanceToWaterValidation = new(IsLive);

// 2 Provide your input data
//  Required fields:
//               Latitude
//               Longitude
//               LicenseKey
//               IsLive
// 
// Optional:
//        TimeoutSeconds

string Latitude = "34.419120";
string Longitude = "-119.703421";
string LicenseKey = "YOUR LICENSE KEY"

// 3 Call the service
DistanceToWaterInfo response = getDistanceToWaterValidation.GetDistanceToWater(Latitude, Longitude, licenseKey).Result;

// 4. Inspect results.
if (response.Error is null)
{
    Console.WriteLine("\r\n* Distance To Water Info *\r\n");
    Console.WriteLine($"Distance To Water: {response.DistanceToWater}");
    Console.WriteLine($"Latitude         : {response.Latitude}");
    Console.WriteLine($"Longitude        : {response.Longitude}");
    Console.WriteLine($"Water Latitude   : {response.WaterLat}");
    Console.WriteLine($"Water Longitude  : {response.WaterLon}");
}
else
{
    Console.WriteLine("\r\n* Error *\r\n");
    Console.WriteLine($"Error Description: {response.Error.Desc}");
    Console.WriteLine($"Error Number     : {response.Error.Number}");
    Console.WriteLine($"Error Location   : {response.Error.Location}");
}
```

# AGUS - GetReverseLocation

Returns an estimated address for a given latitude and longitude.

### [GetReverseLocation Developer Guide/Documentation](https://www.serviceobjects.com/docs/dots-address-geocode-us/agus-operations/agus-getreverselocation/)

## Library Usage

```
// 1 Instantiate the service wrapper
string IsLive = true;
GetReverseLocationValidation getReverseLocationValidation = new(IsLive);

// 2 Provide your input data
//  Required fields:
//               Latitude
//               Longitude
//               LicenseKey
//               IsLive
// 
// Optional:
//        TimeoutSeconds

string Latitude = "34.419061";
string Longitude = "-119.702139";
string LicenseKey = "YOUR LICENSE KEY"

// 3 Call the service
ReverseAddress response = getReverseLocationValidation.GetReverseLocation(Latitude, Longitude, LicenseKey).Result;

// 4. Inspect results.
if (response.Error is null)
{
    Console.WriteLine("\r\n* Reverse Location Info *\r\n");
    Console.WriteLine($"Address : {response.Address}");
    Console.WriteLine($"City    : {response.City}");
    Console.WriteLine($"State   : {response.State}");
    Console.WriteLine($"Zip     : {response.Zip}");
    Console.WriteLine($"County  : {response.County}");
}
else
{
    Console.WriteLine("\r\n* Error *\r\n");
    Console.WriteLine($"Error Description: {response.Error.Desc}");
    Console.WriteLine($"Error Number     : {response.Error.Number}");
    Console.WriteLine($"Error Location   : {response.Error.Location}");
}
```
