
import sys
import os

sys.path.insert(0, os.path.abspath("../address-geocode-us-python/REST"))

from get_distance_to_water_rest import get_distance_to_water

def get_distance_to_water_rest_sdk_go(is_live: bool, license_key: str) -> None:
    print("\r\n--------------------------------------------------")
    print("Address Geocode US - GetDistanceToWater - REST SDK")
    print("--------------------------------------------------")

    latitude = "34.419120"
    longitude = "-119.703421"

    print("\r\n* Input *\r\n")
    print(f"Latitude       : {latitude}")
    print(f"Longitude      : {longitude}")
    print(f"License Key    : {license_key}")
    print(f"Is Live        : {is_live}")

    try:
        response = get_distance_to_water(latitude, longitude, license_key, is_live)

        print("\r\n* Distance Info *\r\n")
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

    except Exception as e:
        print("\r\n* Error *\r\n")
        print(f"Error Message: {str(e)}")