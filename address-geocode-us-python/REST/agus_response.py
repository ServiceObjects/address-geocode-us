from dataclasses import dataclass
from typing import Optional, List


@dataclass
class GetDistanceToWaterInput:
    Latitude: Optional[float] = None
    Longitude: Optional[float] = None
    LicenseKey: Optional[str] = None
    IsLive: bool = True
    TimeoutSeconds: int = 15

    def __str__(self) -> str:
        return (f"GetDistanceToWaterInput: Latitude={self.Latitude}, Longitude={self.Longitude}, "
                f"LicenseKey={self.LicenseKey}, IsLive={self.IsLive}, TimeoutSeconds={self.TimeoutSeconds}")


@dataclass
class InformationComponent:
    Name: Optional[str] = None
    Value: Optional[str] = None

    def __str__(self) -> str:
        return f"InformationComponent: Name={self.Name}, Value={self.Value}"


@dataclass
class Error:
    Desc: Optional[str] = None
    Number: Optional[str] = None
    Location: Optional[str] = None

    def __str__(self) -> str:
        return (f"Error: Desc={self.Desc}, Number={self.Number}, Location={self.Location}")


@dataclass
class GetBestMatchV4Response:
    Level: Optional[str] = None
    LevelDescription: Optional[str] = None
    Latitude: Optional[float] = None
    Longitude: Optional[float] = None
    Zip: Optional[str] = None
    InformationComponents: Optional[List['InformationComponent']] = None
    Error: Optional['Error'] = None

    def __post_init__(self):
        if self.InformationComponents is None:
            self.InformationComponents = []

    def __str__(self) -> str:
        components_string = ', '.join(str(component) for component in self.InformationComponents) if self.InformationComponents else 'None'
        error = str(self.Error) if self.Error else 'None'
        return (f"GetBestMatchV4Response: Level={self.Level}, LevelDescription={self.LevelDescription}, "
                f"Latitude={self.Latitude}, Longitude={self.Longitude}, Zip={self.Zip}, "
                f"InformationComponents=[{components_string}], Error={error}")


@dataclass
class GetDistanceToWaterResponse:
    DistanceToWater: Optional[float] = None
    Latitude: Optional[float] = None
    Longitude: Optional[float] = None
    WaterLat: Optional[float] = None
    WaterLon: Optional[float] = None
    Error: Optional['Error'] = None

    def __str__(self) -> str:
        error = str(self.Error) if self.Error else 'None'
        return (f"GetDistanceToWaterResponse: DistanceToWater={self.DistanceToWater}, Latitude={self.Latitude}, "
                f"Longitude={self.Longitude}, WaterLat={self.WaterLat}, WaterLon={self.WaterLon}, Error={error}")


@dataclass
class GetReverseLocationResponse:
    Address: Optional[str] = None
    City: Optional[str] = None
    State: Optional[str] = None
    Zip: Optional[str] = None
    County: Optional[str] = None
    Error: Optional['Error'] = None

    def __str__(self) -> str:
        error = str(self.Error) if self.Error else 'None'
        return (f"GetReverseLocationResponse: Address={self.Address}, City={self.City}, State={self.State}, "
                f"Zip={self.Zip}, County={self.County}, Error={error}")