import requests  # HTTP client for RESTful API calls
from agus_response import GetDistanceToWaterResponse, Error

# Endpoint URLs for ServiceObjects Address Geocode US (AGUS) API
primary_url = "https://sws.serviceobjects.com/GCR/api.svc/json/GetDistanceToWater?"
backup_url = "https://swsbackup.serviceobjects.com/GCR/api.svc/json/GetDistanceToWater?"
trial_url = "https://trial.serviceobjects.com/GCR/api.svc/json/GetDistanceToWater?"

def get_distance_to_water(
    latitude: str,
    longitude: str,
    license_key: str,
    is_live: bool = True) -> GetDistanceToWaterResponse:
    """
    Call ServiceObjects Address Geocode US (AGUS) API's GetDistanceToWater endpoint
    to retrieve the estimated distance from a given latitude and longitude to the nearest saltwater.

    Parameters:
        latitude: Latitude coordinate (string representation of a number).
        longitude: Longitude coordinate (string representation of a number).
        license_key: Your ServiceObjects license key.
        is_live: Value to determine whether to use the live or trial servers.

    Returns:
        GetDistanceToWaterResponse: Parsed JSON response with distance to water or error details.
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
               # Try backup URL
              response = requests.get(backup_url, params=params, timeout=10)
              response.raise_for_status()
              data = response.json()
             else:
                # Trial mode error is terminal
                raise RuntimeError(f"AGUS trial error: {data['Error']}")

        error = Error(**data.get("Error", {})) if data.get("Error") else None
              
        return GetDistanceToWaterResponse(
            DistanceToWater=data.get("DistanceToWater"),
            Latitude=data.get("Latitude"),
            Longitude=data.get("Longitude"),
            WaterLat=data.get("WaterLat"),
            WaterLon=data.get("WaterLon"),
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

                return GetDistanceToWaterResponse(
                    DistanceToWater=data.get("DistanceToWater"),
                    Latitude=data.get("Latitude"),
                    Longitude=data.get("Longitude"),
                    WaterLat=data.get("WaterLat"),
                    WaterLon=data.get("WaterLon"),
                    Error=error
        )
            except Exception as backup_exc:
                raise RuntimeError("AGUS service unreachable on both endpoints") from backup_exc
        else:
            raise RuntimeError(f"AGUS trial error: {str(req_exc)}") from req_exc