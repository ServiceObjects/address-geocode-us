import sys
import os

sys.path.insert(0, os.path.abspath("../address-geocode-us-python/SOAP"))

from get_best_match_v4_soap import GetBestMatchV4Soap

def get_best_match_v4_soap_sdk_go(is_live: bool, license_key: str) -> None:
    print("\r\n----------------------------------------------")
    print("Address Geocode US - GetBestMatchV4 - SOAP SDK")
    print("----------------------------------------------")

    address = "136 W Canon Perdido St Ste D"
    city = "Santa Barbara"
    state = "CA"
    postal_code = "93101"
    timeout_seconds = 15

    print("\r\n* Input *\r\n")
    print(f"Address        : {address}")
    print(f"City           : {city}")
    print(f"State          : {state}")
    print(f"PostalCode     : {postal_code}")
    print(f"License Key    : {license_key}")
    print(f"Is Live        : {is_live}")
    print(f"Timeout Seconds: {timeout_seconds}")

    try:
        service = GetBestMatchV4Soap(address, city, state, postal_code, license_key, is_live, timeout_seconds)
        response = service.get_best_match_v4()

        if not hasattr(response, 'Error') or not response.Error:
            print("\r\n* Geocode Info *\r\n")
            print(f"Level            : {response.Level}")
            print(f"Level Description: {response.LevelDescription}")
            print(f"Latitude         : {response.Latitude}")
            print(f"Longitude        : {response.Longitude}")
            print(f"Zip              : {response.Zip}")

            print("\r\n* Information Components *\r\n")
            if hasattr(response, 'InformationComponents') and response.InformationComponents:
                for component in response.InformationComponents.InformationComponent:
                    print(f"{component.Name}: {component.Value}")
            else:
                print("No information components found.")
        else:
            print("No geocode info found.")

        if hasattr(response, 'Error') and response.Error:
            print("\r\n* Error *\r\n")
            print(f"Error Desc    : {response.Error.Desc}")
            print(f"Error Number  : {response.Error.Number}")
            print(f"Error Location: {response.Error.Desc}")

    except Exception as e:
        print("\r\n* Error *\r\n")
        print(f"Exception occurred: {str(e)}")
