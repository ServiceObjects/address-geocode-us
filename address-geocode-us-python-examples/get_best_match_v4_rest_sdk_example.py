import sys
import os

sys.path.insert(0, os.path.abspath("../address-geocode-us-python/REST"))

from get_best_match_v4_rest import get_best_match_v4

def get_best_match_v4_rest_sdk_go(is_live: bool, license_key: str) -> None:
    print("\r\n----------------------------------------------")
    print("Address Geocode US - GetBestMatchV4 - REST SDK")
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

    try:
        response = get_best_match_v4(address, city, state, postal_code, license_key, is_live)

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

    except Exception as e:
        print("\r\n* Error *\r\n")
        print(f"Error Message: {str(e)}")
