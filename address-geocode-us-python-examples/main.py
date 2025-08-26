
from get_distance_to_water_rest_sdk_example import get_distance_to_water_rest_sdk_go
from get_best_match_v4_rest_sdk_example import get_best_match_v4_rest_sdk_go
from get_reverse_location_rest_sdk_example import get_reverse_location_rest_sdk_go
from get_best_match_v4_soap_sdk_example import get_best_match_v4_soap_sdk_go
from get_distance_to_water_soap_sdk_example import get_distance_to_water_soap_sdk_go
from get_reverse_location_soap_sdk_example import get_reverse_location_soap_sdk_go

if __name__ =="__main__":

   # Your license key from Service Objects.  
    # Trial license keys will only work on the trial environments and production  
    # license keys will only work on production environments.
    #   
    license_key = "LICENSE KEY"  
    is_live = True

    # Address Geocode US - GetBestMatchV4 - REST SDK
    get_best_match_v4_rest_sdk_go(is_live,license_key)

    # Address Geocode US - GetBestMatchV4 - SOAP SDK
    get_best_match_v4_soap_sdk_go(is_live,license_key)

    # Address Geocode US - GetDistanceToWater - REST SDK
    get_distance_to_water_rest_sdk_go(is_live,license_key)

    # Address Geocode US - GetDistanceToWater - SOAP SDK
    get_distance_to_water_soap_sdk_go(is_live,license_key)
    
    # Address Geocode US - GetReverseLocation - REST SDK
    get_reverse_location_rest_sdk_go(is_live,license_key)

    # Address Geocode US - GetReverseLocation - SOAP SDK
    get_reverse_location_soap_sdk_go(is_live,license_key)