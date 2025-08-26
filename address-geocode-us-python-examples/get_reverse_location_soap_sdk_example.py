import sys
import os

sys.path.insert(0, os.path.abspath("../address-geocode-us-python/SOAP"))

from get_reverse_location_soap import GetReverseLocationSoap

def get_reverse_location_soap_sdk_go(is_live: bool, license_key: str) -> None:
    print("\r\n--------------------------------------------------")
    print("Address Geocode US - GetReverseLocation - SOAP SDK")
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
        service = GetReverseLocationSoap(latitude,longitude,license_key,is_live,timeout_seconds)
        response = service.get_reverse_location()

        if response and not hasattr(response, "Error"):
            print("\r\n* Reverse Location Info *\r\n")
            print(f"Address    : {response.Address}")
            print(f"City       : {response.City}")
            print(f"County     : {response.County}")
            print(f"State      : {response.State}")
            print(f"Zip        : {response.Zip}")
            print(f"PostalCode : {getattr(response, 'PostalCode', '')}")
        elif hasattr(response, "Error") and response.Error:
            print("\n* Error *\n")
            print(f"Error Description: {response.Error.Desc}")
            print(f"Error Number     : {response.Error.Number}")
            print(f"Error Location   : {response.Error.Location}")
        else:
            print("\nNo location info found.")
    except Exception as e:
        print("\n* Exception *\n")
        print(f"Exception Message: {str(e)}")

