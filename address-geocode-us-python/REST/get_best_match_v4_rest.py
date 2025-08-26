import requests
from agus_response import GetBestMatchV4Response, Error, InformationComponent

# Endpoint URLs for ServiceObjects Address Geocode US (AGUS) API
primary_url = "https://sws.serviceobjects.com/GCR/api.svc/json/GetBestMatch_V4?"
backup_url = "https://swsbackup.serviceobjects.com/GCR/api.svc/json/GetBestMatch_V4?"
trial_url = "https://trial.serviceobjects.com/GCR/api.svc/json/GetBestMatch_V4?"

def get_best_match_v4(
    address: str,
    city: str,
    state: str,
    postal_code: str,
    license_key: str,
    is_live: bool = True) -> GetBestMatchV4Response:
    """
    Call ServiceObjects Address Geocode US (AGUS) API's GetBestMatch_V4 endpoint
    to retrieve geocoding information (latitude, longitude, ZIP code, etc.).

    Parameters:
        address: Address line of the address to geocode (e.g., "123 Main Street").
        city: The city of the address to geocode. For example, "New York".
        state: The state of the address to geocode. For example, "NY".
        postal_code: The postal code of the address to geocode. For example, "10001".
        license_key: Your ServiceObjects license key.
        is_live: Use live or trial servers.

    Returns:
        GetBestMatchV4Response: Parsed JSON response with geocoding results or error details.

    Raises:
        RuntimeError: If the API call fails on both primary and backup endpoints or if JSON parsing fails.
    """
    params = {
        "Address": address,
        "City": city,
        "State": state,
        "PostalCode": postal_code,
        "LicenseKey": license_key,
    }
    # Select the base URL: production vs trial
    url = primary_url if is_live else trial_url

    try:
        # Attempt primary (or trial) endpoint
        response = requests.get(url, params=params, timeout=10)
        response.raise_for_status()
        data = response.json()

         # If API returned an error in JSON payload, trigger fallback
        error = getattr(response, 'Error', None)
        if not (error is None or getattr(error, 'Number', None) != "4"):
            if is_live:
                # Try backup URL
                response = requests.get(backup_url, params=params, timeout=10)
                data = response.json()

                # If still error, propagate exception
                if 'Error' in data:
                    raise RuntimeError(f"AGUS service error: {data['Error']}")
            else:
                # Trial mode error is terminal
                raise RuntimeError(f"AGUS trial error: {data['Error']}")

        # Convert JSON response to GetBestMatchV4Response for structured access
        error = Error(**data.get("Error", {})) if data.get("Error") else None

        return GetBestMatchV4Response(
          Level=data.get("Level"),
          LevelDescription=data.get("LevelDescription"),
          Latitude=data.get("Latitude"),
          Longitude=data.get("Longitude"),
          Zip=data.get("Zip"),
          InformationComponents=[
              InformationComponent(Name=comp.get("Name"), Value=comp.get("Value"))
              for comp in data.get("InformationComponents", [])
          ] if "InformationComponents" in data else [],
            Error=error,
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

                return GetBestMatchV4Response(
                    Level=data.get("Level"),
                    LevelDescription=data.get("LevelDescription"),
                    Latitude=data.get("Latitude"),
                    Longitude=data.get("Longitude"),
                    Zip=data.get("Zip"),
                    InformationComponents=[
                        InformationComponent(Name=comp.get("Name"), Value=comp.get("Value"))
                        for comp in data.get("InformationComponents", [])
                    ] if "InformationComponents" in data else [],
                    Error=error,
                )
            except Exception as backup_exc:
                raise RuntimeError("AGUS service unreachable on both endpoints") from backup_exc
        else:
            raise RuntimeError(f"AGUS trial error: {str(req_exc)}") from req_exc