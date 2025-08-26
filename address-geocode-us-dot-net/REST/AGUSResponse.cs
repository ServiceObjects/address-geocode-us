using System.Runtime.Serialization;

namespace address_geocode_us_dot_net.REST
{

    /// <summary>
    /// Response from GetBestMatch_V4 API
    /// </summary>
    [DataContract]

    public class GetBestMatchV4Response
    {
        public string Level { get; set; }
        public string LevelDescription { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Zip { get; set; }
        public InformationComponent[] InformationComponents { get; set; }
        public Error Error { get; set; }
        public override string ToString()
        {
            string infoComponentsString = InformationComponents != null
                ? string.Join(", ", InformationComponents.Select(ic => ic.ToString()))
                : "null";

            return $"Level: {Level}\n" +
                   $"LevelDescription: {LevelDescription}\n" +
                   $"Latitude: {Latitude}\n" +
                   $"Longitude: {Longitude}\n" +
                   $"Zip: {Zip}\n" +
                   $"InformationComponents: [{infoComponentsString}]\n" +
                   $"Error: {(Error != null ? Error.ToString() : "null")}";
        }
    }

    /// <summary>
    /// Information Component for GetBestMatch_V4
    /// </summary>
    [DataContract]
    public class InformationComponent
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public override string ToString()
        {
            return $"Name: {Name}, Value: {Value}";
        }
    }


    /// <summary>
    /// Response from GetDistanceToWater API, containing the estimated distance to the nearest saltwater
    /// and the coordinates of the closest saltwater location.
    /// </summary>
    [DataContract]
    public class GetDistanceToWaterResponse
    {
        public string DistanceToWater { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string WaterLat { get; set; }
        public string WaterLon { get; set; }
        public Error Error { get; set; }

        public override string ToString()
        {
            return $"MilesToWater: {DistanceToWater}\n" +
                   $"Latitude: {Latitude}\n" +
                   $"Longitude: {Longitude}\n" +
                   $"ClosestWaterLatitude: {WaterLat}\n" +
                   $"ClosestWaterLongitude: {WaterLon}\n" +
                   $"Error: {(Error != null ? Error.ToString() : "null")}";
        }
    }


    /// <summary>
    /// Response from GetReverseLocation API, containing the estimated address, city, state, ZIP code,
    /// and county for the given coordinates.
    /// </summary>
    [DataContract]
    public class GetReverseLocationResponse
    {
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string County { get; set; }
        public Error Error { get; set; }
        public override string ToString()
        {
            return $"Address: {Address}\n" +
                   $"City: {City}\n" +
                   $"State: {State}\n" +
                   $"Zip: {Zip}\n" +
                   $"County: {County}\n" +
                   $"Error: {(Error != null ? Error.ToString() : "null")}";
        }
    }
    /// <summary>
    /// Error object for API responses
    /// </summary>
    [DataContract]
    public class Error
    {
        public string Desc { get; set; }
        public string Number { get; set; }
        public string Location { get; set; }
        public override string ToString()
        {
            return $"Desc: {Desc}, TypeCode: {Number}, Location: {Location}";
        }
    }
}