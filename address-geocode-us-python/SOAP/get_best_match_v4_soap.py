from sre_parse import State
from suds.client import Client
from suds import WebFault
from suds.sudsobject import Object

class GetBestMatchV4Soap:
    def __init__(self, address: str, city: str, state: str, postal_code: str, license_key: str, is_live: bool = True, timeout_ms: int = 15000):
        """
        Initialize the GetBestMatchV4Soap client with input parameters and set up primary and backup WSDL URLs.

        Parameters:
            address: Address line of the address to geocode (e.g., "123 Main Street").
            city: The city of the address to geocode (e.g., "New York"). Optional if postal code is provided.
            state: The state of the address to geocode (e.g., "NY"). Optional if postal code is provided.
            postal_code: The ZIP code of the address to geocode. Optional if city and state are provided.
            license_key: Your ServiceObjects license key.
            is_live: Determines whether to use the live or trial servers.
            timeout_ms: Timeout, in milliseconds, for the call to the service.

        Raises:
            ValueError: If LicenseKey is empty or null.
        """
        self.is_live = is_live
        self.timeout = timeout_ms / 1000.0
        self.license_key=license_key
        self.address=address
        self.city=city
        self.state=state
        self.postal_code=postal_code

        # WSDL URLs

        self._primary_wsdl = (
            'https://sws.serviceobjects.com/gcr/soap.svc?wsdl'
            if is_live else
            'https://trial.serviceobjects.com/gcr/soap.svc?wsdl'
        )
        self._backup_wsdl = (
            'https://swsbackup.serviceobjects.com/gcr/soap.svc?wsdl'
            if is_live else
            'https://trial.serviceobjects.com/gcr/soap.svc?wsdl'
        )

    def get_best_match_v4(self) -> Object:
        """
        Calls the GetBestMatch_V4 SOAP endpoint, attempting the primary endpoint first and falling back
        to the backup if the response is invalid (Error.TypeCode == '4') in live mode or if the primary call fails.

        Returns:  
            suds.sudsobject.Object: SOAP response containing geocoding details or error.

        Raises:
            RuntimeError: If both primary and backup calls fail, with detailed error messages.
        """

           # Common kwargs for both calls
        call_kwargs = dict(
            Address=self.address,
            City=self.city,
            State=self.state,
            PostalCode=self.postal_code,
            LicenseKey=self.license_key,
        )
        # Attempt primary
        try:
            client = Client(self._primary_wsdl)
            response = client.service.GetBestMatch_V4(**call_kwargs)

            # If response invalid or Error.Number == "4", trigger fallback
            if response is None or (hasattr(response, "Error") and response.Error and response.Error.Number == "4"):
               raise ValueError("Primary returned no result or Error.Number=4")

            return response

        except (WebFault, ValueError, Exception) as primary_ex:
                try:
                    client = Client(self._backup_wsdl)
                    response = client.service.GetBestMatch_V4(**call_kwargs)
                    if response is None:
                        raise ValueError("Backup returned no result")
                    return response
                except (WebFault, Exception) as backup_ex:
                    msg = (
                        "Both primary and backup endpoints failed.\n"
                        f"Primary error: {str(primary_ex)}\n"
                        f"Backup error: {str(backup_ex)}"
                    )
                    raise RuntimeError(msg)
