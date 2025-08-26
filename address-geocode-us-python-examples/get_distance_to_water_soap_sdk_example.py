
import sys
import os

sys.path.insert(0, os.path.abspath("../address-geocode-us-python/SOAP"))

from get_distance_to_water_soap import GetDistanceToWaterSoap

def get_distance_to_water_soap_sdk_go(is_live: bool, license_key: str) -> None:
    print("\r\n--------------------------------------------------")
    print("Address Geocode US - GetDistanceToWater - SOAP SDK")
    print("--------------------------------------------------")

    latitude = "34.419120"
    longitude = "-119.703421"
    timeout_seconds = 15

    print("\r\n* Input *\r\n")
    print(f"Latitude        : {latitude}")
    print(f"Longitude       : {longitude}")
    print(f"License Key     : {license_key}")
    print(f"Is Live         : {is_live}")
    print(f"Timeout Seconds : {timeout_seconds}")

    try:
        service = GetDistanceToWaterSoap(latitude,longitude,license_key,is_live,timeout_seconds)
        response = service.get_distance_to_water()

        if response and not hasattr(response, "Error"):
            print("\r\n* Distance Info *\r\n")
            print(f"Miles To Water          : {response.DistanceToWater}")
            print(f"Latitude                : {response.Latitude}")
            print(f"Longitude               : {response.Longitude}")
            print(f"Closest Water Latitude  : {response.WaterLat}")
            print(f"Closest Water Longitude : {response.WaterLon}")
        elif hasattr(response, "Error") and response.Error:
            print("\n* Error *\n")
            print(f"Error Description: {response.Error.Desc}")
            print(f"Error Number     : {response.Error.Number}")
            print(f"Error Location   : {response.Error.Location}")
        else:
            print("\nNo distance info found.")
    except Exception as e:
        print("\n* Exception *\n")
        print(f"Exception Message: {str(e)}")
