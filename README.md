
# Weather Observation

The solution has been developed using the latest stable version of .NET 8 to ensure compatibility with current technologies and to leverage the latest features and improvements. The solution consists of two main components: a console application, an API and internal modules.

# API
The API provides comprehensive weather observation data retrieval capabilities for any weather observation station within South Australia. The API defaults to providing data for the Adelaide Airport weather observation station but can handle requests for any other station within the region. 

# Accessing the API
The API can be accessed using the following URLs:

- Swagger: https://localhost:7138/swagger/index.html
- Retrieve all data: https://localhost:7138/SouthAustraliaWeatherObservation/GetWeatherObservationData
- Retrieve specific data: https://localhost:7138/SouthAustraliaWeatherObservation/GetWeatherObservationData?observationStationId=95676&field=name,air_temp
- Retrieve average temperature: https://localhost:7138/SouthAustraliaWeatherObservation/GetWeatherObservationAverageTempature?observationStationId=95676

- GraphQL Support 
  - In addition to the RESTful API, GraphQL has been implemented to provide enhanced querying capabilities and to support future enhancements. This allows clients to request specific data more efficiently and supports complex filtering and querying needs.

  - GraphQL Endpoint:
    - URL: http://localhost:1223/graphql/
    - Provides access to both weather station data and average temperature queries.

# Console Application
The console application is designed to allow users to input a weather observation station ID and retrieve the calculated average temperature for the previous 72 hours. If no ID is provided, it defaults to the Adelaide Airport weather observation station. The application interacts with the API to fetch and display this data.

# Deployment
The API is designed as a RESTful HTTP service that can be deployed to an IIS web server. Responses are provided in JSON format to ensure compatibility with a wide range of clients.

# Config
- API Config
  ```
  Configs for API in appsettings.json in the folder WeatherAPI\WeatherAPI: 
  "WeatherAPI": {
  "Uri": "http://www.<site>.gov.au",
  "RelativePath": "path/.<WMO>.json",
  "DefaultObservationStationId": "1234",
  "FilterPreviousHours": -72
  }

- Console Config
  ```
   Configs for API in appsettings.json in the folder WeatherAPI\WeatherAPI.Console:
   "ApiSettings": {
    "WeatherApiUrl": "https://localhost:7138",
    "AverageTemparatureUrlPath": "/SouthAustraliaWeatherObservation/GetWeatherObservationAverageTempature",
    "DefaultObservationId": 94672
  }
    ```
# Conclusion
The provided solution meets the requirements by delivering a fully functional and deployable source code. It includes comprehensive capabilities for retrieving and calculating weather data, both via RESTful API and GraphQL. The solution is designed for ease of deployment and can be hosted on an IIS web server. This approach ensures scalability, flexibility, and future-proofing for additional features and enhancements.




