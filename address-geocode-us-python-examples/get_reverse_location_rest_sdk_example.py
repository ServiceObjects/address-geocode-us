
import sys
import os

sys.path.insert(0, os.path.abspath("../address-geocode-us-python/REST"))

from get_reverse_location_rest import get_reverse_location

def get_reverse_location_rest_sdk_go(is_live: bool, license_key: str) -> None:
    print("\r\n--------------------------------------------------")
    print("Address Geocode US - GetReverseLocation - REST SDK")
    print("--------------------------------------------------")

    latitude = "34.419120"
    longitude = "-119.703421"

    print("\r\n* Input *\r\n")
    print(f"Latitude       : {latitude}")
    print(f"Longitude      : {longitude}")
    print(f"License Key    : {license_key}")
    print(f"Is Live        : {is_live}")

    try:
        response = get_reverse_location(latitude, longitude, license_key, is_live)

        print("\r\n* Reverse Location Info *\r\n")
        if response and not response.Error:
            print(f"Address   : {response.Address}")
            print(f"City      : {response.City}")
            print(f"County    : {response.County}")
            print(f"State     : {response.State}")
            print(f"Zip       : {response.Zip}")
        else:
            print("No location info found.")

        if response.Error:
            print("\r\n* Error *\r\n")
            print(f"Error Desc    : {response.Error.Desc}")
            print(f"Error Number  : {response.Error.Number}")
            print(f"Error Location: {response.Error.Desc}")

    except Exception as e:
        print("\r\n* Error *\r\n")
        print(f"Error Message: {str(e)}")