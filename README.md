![Service Objects Logo](https://www.serviceobjects.com/wp-content/uploads/2021/05/SO-Logo-with-TM.gif "Service Objects Logo")

# AGUS - Address Geocode US

DOTS Address Geocode – US is a publicly available XML and JSON web service that provides latitude/longitude and metadata information about a physical US address. The service provides geocoding information, such as the latitude and longitude location of a US address, along with demographic information, such as the census tract, block and other metadata.

AGUS can help provide instant address locations to websites or enhancement to contact lists.

## [Service Objects Website](https://serviceobjects.com)
## [Developer Guide/Documentation](https://www.serviceobjects.com/docs/)

# AGUS - GetBestMatches_V4

GetBestMatches_V4: Returns the latitude and longitude for a given US address. This operation will use cascading logic when an exact address match is not found and it will return the next closest level match available. The operation output is designed to allow the service to return new pieces of data as they become available without having to re-integrate.

## [GetBestMatches_V4 Developer Guide/Documentation](https://www.serviceobjects.com/docs/dots-address-geocode-us/agus-operations/agus-getbestmatch_v4-recommended/)

## GetBestMatches_V4 Request URLs (Query String Parameters)

>[!CAUTION]
>### *Important - Use query string parameters for all inputs.  Do not use path parameters since it will lead to errors due to optional parameters and special character issues.*


### JSON
#### Trial

https://trial.serviceobjects.com/GCR/api.svc/json/GetBestMatch_V4?Address=136+W+Canon+Perdido+St+Ste+D&City=Santa+Barbara&State=CA&PostalCode=93101&LicenseKey={LicenseKey}

#### Production

https://sws.serviceobjects.com/GCR/api.svc/json/GetBestMatch_V4?Address=136+W+Canon+Perdido+St+Ste+D&City=Santa+Barbara&State=CA&PostalCode=93101&LicenseKey={LicenseKey}

#### Production Backup

https://swsbackup.serviceobjects.com/GCR/api.svc/json/GetBestMatch_V4?Address=136+W+Canon+Perdido+St+Ste+D&City=Santa+Barbara&State=CA&PostalCode=93101&LicenseKey={LicenseKey}

### XML
#### Trial

https://trial.serviceobjects.com/GCR/api.svc/xml/GetBestMatch_V4?Address=136+W+Canon+Perdido+St+Ste+D&City=Santa+Barbara&State=CA&PostalCode=93101&LicenseKey={LicenseKey}

#### Production

https://sws.serviceobjects.com/GCR/api.svc/xml/GetBestMatch_V4?Address=136+W+Canon+Perdido+St+Ste+D&City=Santa+Barbara&State=CA&PostalCode=93101&LicenseKey={LicenseKey}

#### Production Backup

https://swsbackup.serviceobjects.com/GCR/api.svc/xml/GetBestMatch_V4?Address=136+W+Canon+Perdido+St+Ste+D&City=Santa+Barbara&State=CA&PostalCode=93101&LicenseKey={LicenseKey}

# AGUS - GetDistanceToWater

This operation returns an estimate on the number of miles, kilometers and feet between two sets of coordinates.

## [GetDistanceToWater Developer Guide/Documentation](https://www.serviceobjects.com/docs/dots-address-geocode-us/agus-operations/agus-getdistance/)

## GetDistanceToWater Request URLs (Query String Parameters)

>[!CAUTION]
>#### *Important - Use query string parameters for all inputs.  Do not use path parameters since it will lead to errors due to optional parameters and special character issues.*

### JSON
#### Trial

https://trial.serviceobjects.com/GCR/api.svc/json/GetDistanceToWater?Latitude=34.419061&Longitude=-119.702139&LicenseKey={LicenseKey}

#### Production

https://sws.serviceobjects.com/GCR/api.svc/json/GetDistanceToWater?Latitude=34.419061&Longitude=-119.702139&LicenseKey={LicenseKey}

#### Production Backup

https://swsbackup.serviceobjects.com/GCR/api.svc/json/GetDistanceToWater?Latitude=34.419061&Longitude=-119.702139&LicenseKey={LicenseKey}

### XML
#### Trial

https://trial.serviceobjects.com/GCR/api.svc/xml/GetDistanceToWater?Latitude=34.419061&Longitude=-119.702139&LicenseKey={LicenseKey}

#### Production

https://sws.serviceobjects.com/GCR/api.svc/xml/GetDistanceToWater?Latitude=34.419061&Longitude=-119.702139&LicenseKey={LicenseKey}

#### Production Backup

https://swsbackup.serviceobjects.com/GCR/api.svc/xml/GetDistanceToWater?Latitude=34.419061&Longitude=-119.702139&LicenseKey={LicenseKey}

# AGUS - GetReverseLocation

Returns an estimated address for a given latitude and longitude.

## [GetReverseLocation Developer Guide/Documentation](https://www.serviceobjects.com/docs/dots-address-geocode-us/agus-operations/agus-getreverselocation/)

## GetReverseLocation Request URLs (Query String Parameters)

>[!CAUTION]
>#### *Important - Use query string parameters for all inputs.  Do not use path parameters since it will lead to errors due to optional parameters and special character issues.*

### JSON
#### Trial

https://trial.serviceobjects.com/GCR/api.svc/json/GetReverseLocation?Latitude=33.383769&Longitude=-112.039781&LicenseKey={LicenseKey}

#### Production

https://sws.serviceobjects.com/GCR/api.svc/json/GetReverseLocation?Latitude=33.383769&Longitude=-112.039781&LicenseKey={LicenseKey}

#### Production Backup

https://swsbackup.serviceobjects.com/GCR/api.svc/json/GetReverseLocation?Latitude=33.383769&Longitude=-112.039781&LicenseKey={LicenseKey}

### XML
#### Trial

https://trial.serviceobjects.com/GCR/api.svc/xml/GetReverseLocation?Latitude=33.383769&Longitude=-112.039781&LicenseKey={LicenseKey}

#### Production

https://sws.serviceobjects.com/GCR/api.svc/xml/GetReverseLocation?Latitude=33.383769&Longitude=-112.039781&LicenseKey={LicenseKey}

#### Production Backup

https://swsbackup.serviceobjects.com/GCR/api.svc/xml/GetReverseLocation?Latitude=33.383769&Longitude=-112.039781&LicenseKey={LicenseKey}
