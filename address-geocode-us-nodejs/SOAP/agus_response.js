export class GetDistanceToWaterInput {
    constructor(data = {}) {
        this.Latitude = data.Latitude || null;
        this.Longitude = data.Longitude || null;
        this.LicenseKey = data.LicenseKey || null;
        this.IsLive = data.IsLive !== undefined ? data.IsLive : true;
        this.TimeoutSeconds = data.TimeoutSeconds !== undefined ? data.TimeoutSeconds : 15;
    }

    toString() {
        return `GetDistanceToWaterInput: Latitude = ${this.Latitude}, Longitude = ${this.Longitude}, LicenseKey = ${this.LicenseKey}, IsLive = ${this.IsLive}, TimeoutSeconds = ${this.TimeoutSeconds}`;
    }
}

/**
 * Information Component for API responses.
 */
export class InformationComponent {
    constructor(data = {}) {
        this.Name = data.Name || null;
        this.Value = data.Value || null;
    }

    toString() {
        return `Name = ${this.Name}, Value = ${this.Value}`;
    }
}

/**
 * Error object for API responses.
 */
export class Error {
    constructor(data = {}) {
        this.Desc = data.Desc || null;
        this.Number = data.Number || null;
        this.Location = data.Location || null;
    }

    toString() {
        return `Error: Desc = ${this.Desc}, Number = ${this.Number}, Location = ${this.Location}`;
    }
}

/**
 * Response from GetBestMatch_V4 API.
 */
export class GetBestMatchV4Response {
    constructor(data = {}) {
        this.Level = data.Level || null;
        this.LevelDescription = data.LevelDescription || null;
        this.Latitude = data.Latitude || null;
        this.Longitude = data.Longitude || null;
        this.Zip = data.Zip || null;
        this.InformationComponents =data.InformationComponents.InformationComponent;
        this.Error = data.Error ? new Error(data.Error) : null;
    }

    toString() {
        const componentsString = this.InformationComponents.length
            ? this.InformationComponents.map(component => component.toString()).join(', ')
            : 'null';
        return `GetBestMatchV4Response: Level = ${this.Level}, LevelDescription = ${this.LevelDescription}, Latitude = ${this.Latitude}, Longitude = ${this.Longitude}, Zip = ${this.Zip}, InformationComponents = [${componentsString}], Error = ${this.Error ? this.Error.toString() : 'null'}`;
    }
}

/**
 * Response from GetDistanceToWater API, containing the estimated distance to the nearest saltwater
 * and the coordinates of the closest saltwater location.
 */
export class GetDistanceToWaterResponse {
    constructor(data = {}) {
        this.DistanceToWater = data.DistanceToWater || null;
        this.Latitude = data.Latitude || null;
        this.Longitude = data.Longitude || null;
        this.WaterLat = data.WaterLat || null;
        this.WaterLon = data.WaterLon || null;
        this.Error = data.Error ? new Error(data.Error) : null;
    }

    toString() {
        return `GetDistanceToWaterResponse: MilesToWater = ${this.DistanceToWater}, Latitude = ${this.Latitude}, Longitude = ${this.Longitude}, ClosestWaterLatitude = ${this.WaterLat}, ClosestWaterLongitude = ${this.WaterLon}, Error = ${this.Error ? this.Error.toString() : 'null'}`;
    }
}

/**
 * Response from GetReverseLocation API, containing the estimated address, city, state, ZIP code,
 * and county for the given coordinates.
 */
export class GetReverseLocationResponse {
    constructor(data = {}) {
        this.Address = data.Address || null;
        this.City = data.City || null;
        this.State = data.State || null;
        this.Zip = data.Zip || null;
        this.County = data.County || null;
        this.Error = data.Error ? new Error(data.Error) : null;
    }

    toString() {
        return `GetReverseLocationResponse: Address = ${this.Address}, City = ${this.City}, State = ${this.State}, Zip = ${this.Zip}, County = ${this.County}, Error = ${this.Error ? this.Error.toString() : 'null'}`;
    }
}

export default GetDistanceToWaterResponse;