import requests  # HTTP client for RESTful API calls
from agus_response import GetReverseLocationResponse, Error

# Endpoint URLs for ServiceObjects Address Geocode US (AGUS) API
primary_url = "https://sws.serviceobjects.com/GCR/api.svc/json/GetReverseLocation?"
backup_url = "https://swsbackup.serviceobjects.com/GCR/api.svc/json/GetReverseLocation?"
trial_url = "https://trial.serviceobjects.com/GCR/api.svc/json/GetReverseLocation?"

def get_reverse_location(
    latitude: str,
    longitude: str,
    license_key: str,
    is_live: bool = True) -> GetReverseLocationResponse:
    """
    Call ServiceObjects Address Geocode US (AGUS) API's GetReverseLocation endpoint
    to retrieve the estimated address for a given latitude and longitude.

    Parameters:
        latitude: Latitude coordinate (string representation of a number).
        longitude: Longitude coordinate (string representation of a number).
        license_key: Your ServiceObjects license key.
        is_live: Value to determine whether to use the live or trial servers.

    Returns:
        GetReverseLocationResponse: Parsed JSON response with address results or error details.
    """
    # Preparing input parameters
    params = {
        "Latitude": latitude,
        "Longitude": longitude,
        "LicenseKey": license_key,
    }

    # Selecting the base URL: production vs trial
    url = primary_url if is_live else trial_url

    # Attempting primary (or trial) endpoint first
    try:
        response = requests.get(url, params=params, timeout=10)
        response.raise_for_status()
        data = response.json()
      
        # If API returned an error in JSON payload, trigger fallback
        error = getattr(response, 'Error', None)
        if not (error is None or getattr(error, 'Number', None) != "4"):
           if is_live:
            # Trying backup URL
            response = requests.get(backup_url, params=params, timeout=10)
            response.raise_for_status()
            data = response.json()
           else:
             # Trial mode error is terminal
            raise RuntimeError(f"AGUS trial error: {data['Error']}")

           error = Error(**data.get("Error", {})) if data.get("Error") else None
              
        return GetReverseLocationResponse(Address=data.get("Address"),
                                          City=data.get("City"),
                                          State=data.get("State"),
                                          Zip=data.get("Zip"),
                                          County=data.get("County"),
                                          Error=error
                                          )  

    except requests.RequestException as req_exc:
        # Network or HTTP-level error occurred
        if is_live:
            try:
                # Fallback to backup URL
                response = requests.get(backup_url, params=params, timeout=10)
                response.raise_for_status()
                data = response.json()
                if "Error" in data:
                    raise RuntimeError(f"AGUS backup error: {data['Error']}") from req_exc

                error = Error(**data.get("Error", {})) if data.get("Error") else None


                return GetReverseLocationResponse(Address=data.get("Address"),
                                          City=data.get("City"),
                                          State=data.get("State"),
                                          Zip=data.get("Zip"),
                                          County=data.get("County"),
                                          Error=error
                                          )  
            except Exception as backup_exc:
                raise RuntimeError("AGUS service unreachable on both endpoints") from backup_exc
        else:
            raise RuntimeError(f"AGUS trial error: {str(req_exc)}") from req_exc