using address_geocode_us_dot_net_examples;

//Your license key from Service Objects.
//Trial license keys will only work on the
//trail environments and production license
//keys will only work on production environments.
string LicenseKey = "LICENSE KEY";

bool IsProductionKey = false;


// Address Geocode US - GetBestMatchV4 - REST SDK
GetBestMatchV4RestSdkExample.Go(LicenseKey, IsProductionKey);

// Address Geocode US - GetBestMatchV4 - SOAP SDK
GetBestMatchV4SoapSdkExample.Go(LicenseKey, IsProductionKey);

// Address Geocode US - GetDistanceToWater - REST SDK
GetDistanceToWaterRestSdkExample.Go(LicenseKey, IsProductionKey);

// Address Geocode US - GetReverseLocation - SOAP SDK
GetDistanceToWaterSoapSdkExample.Go(LicenseKey, IsProductionKey);

// Address Geocode US - GetReverseLocation - REST SDK
GetReverseLocationRestSdkExample.Go(LicenseKey, IsProductionKey);

// Address Geocode US - GetDistanceToWater - SOAP SDK
GetReverseLocationSoapSdkExample.Go(LicenseKey, IsProductionKey);

