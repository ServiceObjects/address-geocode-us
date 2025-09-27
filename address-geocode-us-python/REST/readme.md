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
# 1. Build the input
#
# Required fields:
#               address
#               city 
#               state
#               postal_code
#               license_key
#               is_live

from get_best_match_v4_soap import GetBestMatchV4Soap

address = "136 W Canon Perdido St Ste D"
city = "Santa Barbara"
state = "CA"
postal_code = "93101"
is_live = True
license_key = "YOUR LICENSE KEY"

# 2. Call the method.
response = get_best_match_v4(address, city, state, postal_code, license_key, is_live)

# 3. Inspect results.
print("\r\n* Geocode Info *\r\n")
if response and not response.Error:
    print(f"Level            : {response.Level}")
    print(f"Level Description: {response.LevelDescription}")
    print(f"Latitude         : {response.Latitude}")
    print(f"Longitude        : {response.Longitude}")
    print(f"Zip              : {response.Zip}")

    print("\r\n* Information Components *\r\n")
    if response.InformationComponents and len(response.InformationComponents) > 0:
        for component in response.InformationComponents:
            print(f"{component.Name}: {component.Value}")
    else:
        print("No information components found.")
else:
    print("No geocode info found.")

if response.Error:
    print("\r\n* Error *\r\n")
    print(f"Error Desc    : {response.Error.Desc}")
    print(f"Error Number  : {response.Error.Number}")
    print(f"Error Location: {response.Error.Desc}")
```

# AGUS - GetDistanceToWater

Returns an estimated distance from a given latitude and longitude to the nearest saltwater.

### [GetDistanceToWater Developer Guide/Documentation](https://www.serviceobjects.com/docs/dots-address-geocode-us/agus-operations/agus-getdistancetowater/)

## Library Usage

```
# 1. Build the input
#
#  Required fields:
#               latitude
#               longitude
#               license_key
#               is_live

from get_distance_to_water_soap import GetDistanceToWaterSoap

latitude = "34.419120"
longitude = "-119.703421"
is_live = True
license_key = "YOUR LICENSE KEY"

# 2. Call the sync InvokeAsync() method.
response = get_distance_to_water(latitude, longitude, license_key, is_live)

# 3. Inspect results.
if response and not response.Error:
    print(f"MilesToWater         : {response.DistanceToWater}")
    print(f"Latitude             : {response.Latitude}")
    print(f"Longitude            : {response.Longitude}")
    print(f"ClosestWaterLatitude : {response.WaterLat}")
    print(f"ClosestWaterLongitude: {response.WaterLon}")
else:
    print("No distance info found.")

if response.Error:
    print("\r\n* Error *\r\n")
    print(f"Error Desc    : {response.Error.Desc}")
    print(f"Error Number  : {response.Error.Number}")
    print(f"Error Location: {response.Error.Desc}")
```

# AGUS - GetReverseLocation

Returns an estimated address for a given latitude and longitude.

### [GetReverseLocation Developer Guide/Documentation](https://www.serviceobjects.com/docs/dots-address-geocode-us/agus-operations/agus-getreverselocation/)

## Library Usage

```
# 1. Build the input
#
#  Required fields:
#               latitude
#               longitude
#               license_key
#               is_live

from get_reverse_location_soap import GetReverseLocationSoap

latitude = "34.419120"
longitude = "-119.703421"
is_live = True
license_key = "YOUR LICENSE KEY"

# 2. Call the sync InvokeAsync() method.
response = get_reverse_location(latitude, longitude, license_key, is_live)

# 3. Inspect results.
if response and not response.Error:
    print(f"Address   : {response.Address}")
    print(f"City      : {response.City}")
    print(f"State     : {response.State}")
    print(f"Zip       : {response.Zip}")
    print(f"County    : {response.County}")
else:
    print("No location info found.")

if response.Error:
    print("\r\n* Error *\r\n")
    print(f"Error Desc    : {response.Error.Desc}")
    print(f"Error Number  : {response.Error.Number}")
    print(f"Error Location: {response.Error.Desc}")
```
