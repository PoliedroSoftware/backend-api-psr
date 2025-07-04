using Poliedro.Psr.Application.Dto;
using Poliedro.Psr.Domain.Dto;
using Poliedro.Psr.Domain.Enum;

namespace Poliedro.Psr.Application.Mock;

public static class MockDataFactory
{
    private static string GetMonthName(int monthNumber)
    {
        return Enum.GetName(typeof(Month), monthNumber) ?? "Unknown";
    }
    public static ResponseReaderDto GetByID(
        Guid guid,
        int pageNumber,
        int pageSize)
    {
        Random random = new();
        List<ReaderHistoryDto> readerHistories = []; ;
        var years = new[] { 2022, 2023, 2024 };
        int monthRamdom = random.Next(1, 13);
        for (int i = 0; i < 24; i++)
        {
            readerHistories.Add(new ReaderHistoryDto(
              Month: GetMonthName(monthRamdom),
              NumberMonth: monthRamdom,
              Reader: random.Next(1000000, 10000000).ToString(),
              Year: years[random.Next(years.Length)]
            ));
        }

        for (int i = 0; i < 36; i++)
        {
            monthRamdom = random.Next(1, 13);
            readerHistories.Add(new ReaderHistoryDto(
              Month: GetMonthName(monthRamdom),
              Reader: random.Next(1000000, 10000000).ToString(),
              NumberMonth: monthRamdom,
              Year: years[random.Next(years.Length)]
            ));
        }

        var sortedHistories = readerHistories
            .OrderByDescending(history => history.Year)
            .ThenBy(history => history.NumberMonth)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        var totalCount = readerHistories.Count;
        var paginatedResponse = new PaginatedReaderHistoryDto(sortedHistories, totalCount, pageNumber, pageSize);

      
        IEnumerable<AddressDto> addresses = [
            new AddressDto(
               Kdx: "123",
               Neighborhood: "San Agustin",
               Zone: "A1",
               City: "Ocaña",
               Country: "Colombia",
               Devices:
                [
                    new DeviceDto(
                       Guid: Guid.NewGuid(),
                       DeviceType: DeviceType.Light.ToString(),
                       Qr: new QrDetailsDto(
                            Code: "QR1234567890",
                            Uri: new Uri("https://softmatic.com/images/QR%20ECC%20Low%20Example.png")),
                       State: new StateDevice(
                           State: true,
                           Description: "Active",
                           Color: "#6ACE66"),
                       Reader: new ReaderDto(
                          Reader: random.Next(1000000, 10000000).ToString(),
                          DateTime: DateTime.UtcNow,
                          LastReader: random.Next(1000000, 10000000).ToString()),
                      ReaderHistories: paginatedResponse,
                      Provider:random.Next(10, 100).ToString()),
                      new DeviceDto(
                       Guid: Guid.NewGuid(),
                       DeviceType: DeviceType.Water.ToString(),
                       Qr: new QrDetailsDto(
                            Code: "QR1234567890",
                            Uri: new Uri("https://softmatic.com/images/QR%20ECC%20Low%20Example.png")),
                      State: new StateDevice(
                           State: false,
                           Description: "Inactive",
                           Color: "#E7E7E7"),
                      Reader: new ReaderDto(
                          random.Next(1000000, 10000000).ToString(),
                          DateTime.UtcNow.AddDays(-1),
                          random.Next(1, 8).ToString()),
                      ReaderHistories: paginatedResponse,
                      Provider:random.Next(10, 100).ToString())
                ],
               Gps: [
                   new MarkerDto(
                         Position: new PositionDto(
                             Lat: 8.239635,
                             Lng: -73.350713
                         ),
                         Code: random.Next(10, 100).ToString(),
                         Trt: random.Next(10, 100).ToString(),
                         Phone: "3150000000",
                         Photo: "https://decohunter.com/wp-content/uploads/2020/05/fachadas-de-casa.jpg"
                     ),
                     new MarkerDto(
                         Position: new PositionDto(
                             Lat: 8.239635,
                             Lng: -73.350815
                         ),
                         Code: random.Next(10, 100).ToString(),
                         Trt: random.Next(10, 100).ToString(),
                         Phone: "3150000000",
                         Photo: "https://decohunter.com/wp-content/uploads/2020/05/fachadas-de-casa.jpg"
                     ),

               ]
            ),
            new AddressDto(
               Kdx: "456",
               Neighborhood: "San Clara",
               Zone: "B2",
               City: "Cucuta",
               Country:  "Colombia",
                [
                    new DeviceDto(
                       Guid: Guid.NewGuid(),
                       DeviceType: DeviceType.Gas.ToString(),
                       Qr: new QrDetailsDto(
                            Code: "QR1234567890",
                            Uri: new Uri("https://softmatic.com/images/QR%20ECC%20Low%20Example.png")),
                       State: new StateDevice(
                           State: true,
                           Description: "Active",
                           Color: "#6ACE66"),
                       Reader: new ReaderDto(
                           random.Next(1000000, 10000000).ToString(),
                           DateTime.UtcNow.AddDays(-2),
                           random.Next(1000000, 10000000).ToString()),
                       ReaderHistories: paginatedResponse,
                       Provider:random.Next(10, 100).ToString()),
                    new DeviceDto(
                     Guid: Guid.NewGuid(),
                     DeviceType: DeviceType.Light.ToString(),
                     Qr: new QrDetailsDto(
                            Code: "QR1234567890",
                            Uri: new Uri("https://softmatic.com/images/QR%20ECC%20Low%20Example.png")),
                     State: new StateDevice(
                           State: true,
                           Description: "Active",
                           Color: "#6ACE66"),
                     Reader: new ReaderDto(
                        Reader: random.Next(1000000, 10000000).ToString(),
                        DateTime: DateTime.UtcNow.AddDays(-3),
                        LastReader: random.Next(1000000, 10000000).ToString()),
                     ReaderHistories: paginatedResponse,
                     Provider:random.Next(10, 100).ToString()),
                    new DeviceDto(
                    Guid: Guid.NewGuid(),
                    DeviceType: DeviceType.Water.ToString(),
                    Qr: new QrDetailsDto(
                            Code: "QR1234567890",
                            Uri: new Uri("https://softmatic.com/images/QR%20ECC%20Low%20Example.png")),
                    State: new StateDevice(
                            State: false,
                            Description: "Inactive",
                            Color: "#E7E7E7"),
                    Reader:
                    new ReaderDto(
                        random.Next(1, 8).ToString(),
                        DateTime.UtcNow.AddDays(-4),
                        random.Next(1000000, 10000000).ToString()),
                    ReaderHistories: paginatedResponse, 
                    Provider:random.Next(10, 100).ToString()),
                ],
               Gps: [
                   new MarkerDto(
                         Position: new PositionDto(
                             Lat: 8.239635,
                             Lng: -73.350713
                         ),
                         Code: random.Next(10, 100).ToString(),
                         Trt: random.Next(10, 100).ToString(),
                         Phone: "3150000000",
                         Photo: "https://decohunter.com/wp-content/uploads/2020/05/fachadas-de-casa.jpg"
                     ),
                     new MarkerDto(
                         Position: new PositionDto(
                             Lat: 8.239635,
                             Lng: -73.350815
                         ),
                         Code: random.Next(10, 100).ToString(),
                         Trt: random.Next(10, 100).ToString(),
                         Phone: "3150000000",
                         Photo: "https://decohunter.com/wp-content/uploads/2020/05/fachadas-de-casa.jpg"
                     ),

               ]
            ),
            new AddressDto(
               Kdx: "789",
               Neighborhood: "San Francisco",
               Zone: "C3",
               City: "Abrego",
               Country: "Colombia",
               Devices: [
                    new DeviceDto(
                    Guid: Guid.NewGuid(),
                    DeviceType: DeviceType.Gas.ToString(),
                    Qr: new QrDetailsDto(
                            Code: "QR1234567890",
                            Uri: new Uri("https://softmatic.com/images/QR%20ECC%20Low%20Example.png")),
                    State:  new StateDevice(
                           State: true,
                           Description: "Active",
                           Color: "#6ACE66"),
                    Reader: new
                    ReaderDto(
                        random.Next(1000000, 10000000).ToString(), DateTime.UtcNow.AddDays(-5),
                        random.Next(1000000, 10000000).ToString()),
                    ReaderHistories: paginatedResponse,
                    Provider:random.Next(10, 100).ToString()),
                ],
               Gps: [
                   new MarkerDto(
                         Position: new PositionDto(
                             Lat: 8.239635,
                             Lng: -73.350713
                         ),
                         Code: random.Next(10, 100).ToString(),
                         Trt: random.Next(10, 100).ToString(),
                         Phone: "3150000000",
                         Photo: "https://decohunter.com/wp-content/uploads/2020/05/fachadas-de-casa.jpg"
                     ),
                     new MarkerDto(
                         Position: new PositionDto(
                             Lat: 8.239635,
                             Lng: -73.350815
                         ),
                         Code: random.Next(10, 100).ToString(),
                         Trt: random.Next(10, 100).ToString(),
                         Phone: "3150000000",
                         Photo: "https://decohunter.com/wp-content/uploads/2020/05/fachadas-de-casa.jpg"
                     ),

               ]
            )];

        return new ResponseReaderDto(
          User: new UserDto(
          Id: Guid.NewGuid(),
             Person: new PersonDto(
                 Guid: Guid.NewGuid(),
                 Name: "Poliedro",
                 LastName: "Software",
                 Phone: "+573157690579",
                 Email: "poliedrocloud@outlook.com"
               ),
             Address: addresses,
             Roles: ["admin", "business"]
           )
       );
    }

    public static PaginationDto<ResponseReaderDto> GetALL(int totalUsers, int pageNumber, int pageSize)
    {
        Random random = new();
        List<ResponseReaderDto> users = [];
        if (pageSize > 50) pageSize = 50;

        int startIndex = (pageNumber - 1) * pageSize;
        int endIndex = Math.Min(startIndex + pageSize, totalUsers);
        List<ReaderHistoryDto> readerHistories = []; 
        var years = new[] { 2022, 2023, 2024 };
        int monthRandom = random.Next(1, 13);

       
        for (int i = 0; i < 100; i++)
        {
            readerHistories.Add(new ReaderHistoryDto(
                Month: GetMonthName(monthRandom),
                NumberMonth: monthRandom,
                Reader: random.Next(1000000, 10000000).ToString(),
                Year: years[random.Next(years.Length)]
            ));
        }

      
        var sortedHistories = readerHistories
            .OrderByDescending(history => history.Year)
            .ThenBy(history => history.NumberMonth)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        var totalCount = readerHistories.Count;
        var paginatedResponse = new PaginatedReaderHistoryDto(sortedHistories, totalCount, pageNumber, pageSize);

       
        for (int userIndex = startIndex + 1; userIndex < endIndex; userIndex++)
        {
            List<AddressDto> addresses = []; 
            for (int addressIndex = 0; addressIndex < 3; addressIndex++)
            {
                addresses.Add(new AddressDto(
                    Kdx: $"KDX{userIndex}_{addressIndex}_123",
                    Neighborhood: $"Neighborhood{userIndex}_{addressIndex}",
                    Zone: $"Zone{userIndex}_{addressIndex}",
                    City: $"Ocaña{userIndex}",
                    Country: "Colombia",
                    Devices:[
                    new DeviceDto(
                        Guid: Guid.NewGuid(),
                        DeviceType: DeviceType.Light.ToString(),
                        Qr: new QrDetailsDto(
                            Code: $"QR{userIndex}{addressIndex}1234567890",
                            Uri: new Uri("https://softmatic.com/images/QR%20ECC%20Low%20Example.png")),
                        State: new StateDevice(
                            State: true,
                            Description: "Active",
                            Color: "#6ACE66"),
                        Reader: new ReaderDto(
                            Reader: random.Next(1000000, 10000000).ToString(),
                            DateTime: DateTime.UtcNow,
                            LastReader: random.Next(1000000, 10000000).ToString()),
                        ReaderHistories: paginatedResponse,
                        Provider: "CENS"
                    ),
                    new DeviceDto(
                        Guid: Guid.NewGuid(),
                        DeviceType: DeviceType.Water.ToString(),
                        Qr: new QrDetailsDto(
                            Code: $"QR{userIndex}{addressIndex}1234567890",
                            Uri: new Uri("https://softmatic.com/images/QR%20ECC%20Low%20Example.png")),
                        State: new StateDevice(
                            State: false,
                            Description: "Inactive",
                            Color: "#E7E7E7"),
                        Reader: new ReaderDto(
                            Reader: random.Next(1000000, 10000000).ToString(),
                            DateTime: DateTime.UtcNow.AddDays(-1),
                            LastReader: random.Next(1000000, 10000000).ToString()),
                        ReaderHistories: paginatedResponse,
                        Provider: "ESPO"),
                     new DeviceDto(
                        Guid: Guid.NewGuid(),
                        DeviceType: DeviceType.Gas.ToString(),
                        Qr: new QrDetailsDto(
                            Code: $"QR{userIndex}{addressIndex}1234567890",
                            Uri: new Uri("https://softmatic.com/images/QR%20ECC%20Low%20Example.png")),
                        State: new StateDevice(
                            State: false,
                            Description: "Inactive",
                            Color: "#E7E7E7"),
                        Reader: new ReaderDto(
                            Reader: random.Next(1000000, 10000000).ToString(),
                            DateTime: DateTime.UtcNow.AddDays(-1),
                            LastReader: random.Next(1000000, 10000000).ToString()),
                        ReaderHistories: paginatedResponse,
                        Provider: "NORGAS")
                    ],
                      Gps: [
                       new MarkerDto(
                             Position: new PositionDto(
                                 Lat: 8.239635,
                                 Lng: -73.350713
                             ),
                             Code: random.Next(10, 100).ToString(),
                             Trt: random.Next(10, 100).ToString(),
                             Phone: "3150000000",
                             Photo: "https://decohunter.com/wp-content/uploads/2020/05/fachadas-de-casa.jpg"
                         ),
                         new MarkerDto(
                             Position: new PositionDto(
                                 Lat: 8.239635,
                                 Lng: -73.350815
                             ),
                             Code: random.Next(10, 100).ToString(),
                             Trt: random.Next(10, 100).ToString(),
                             Phone: "3150000000",
                             Photo: "https://decohunter.com/wp-content/uploads/2020/05/fachadas-de-casa.jpg"
                         ),

                   ]
                ));

            }
            var roles = userIndex % 2 == 0 ? ["admin", "business"] : new List<string> { "business" };

            users.Add(new ResponseReaderDto(
                User: new UserDto(
                    Id: Guid.NewGuid(),
                    Person: new PersonDto(
                        Guid: Guid.NewGuid(),
                        Name: $"Name{userIndex}",
                        LastName: $"Lastname{userIndex}",
                        Phone: $"+573157690{random.Next(1000, 9999)}",
                        Email: $"user{userIndex}@outlook.com"
                    ),
                    Address: addresses,
                    Roles: roles
                )
            ));
        }
        return new PaginationDto<ResponseReaderDto>(users, totalUsers, pageNumber, pageSize);
    }


    public static PaginationDto<PersonSearch> GenerateUsers(
        int totalUsers, 
        int pageNumber, 
        int pageSize,
        string? neighborhoodFilter = null,
        string? nameFilter = null)
    {
        if (pageSize > 50) pageSize = 50; 
        List<PersonSearch> users = [];
        int startIndex = (pageNumber - 1) * pageSize;
        int endIndex = Math.Min(startIndex + pageSize, totalUsers);
        for (int i = startIndex + 1; i <= endIndex; i++)
        {
            var user = new PersonSearch
            (
                Guid: Guid.NewGuid(),
                Name: $"Name{i}",
                LastName: $"LastName{i}",
                Neighborhood: $"Neighborhood{i}",
                Kdx: $"KDX{i}_123"
            );
            if ((string.IsNullOrEmpty(neighborhoodFilter) || user.Neighborhood.Contains(neighborhoodFilter, StringComparison.OrdinalIgnoreCase)) &&
            (string.IsNullOrEmpty(nameFilter) || user.Name.Contains(nameFilter, StringComparison.OrdinalIgnoreCase)))
            {
                users.Add(user);
            }
        }
        return new PaginationDto<PersonSearch>(users, totalUsers, pageNumber, pageSize);
    }


    public static string Login(AuthenticationRequest request)
    {
        string token = string.Empty;
        if(request.CodeCountry == "+57" && request.Phone == "2222222222" && request.Password == "1234")
        {
            token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiJ1c2VyX2lkXzEyMyIsIm5iZiI6MTYwMDAwMDAwMCwiZXhwIjoxNjAwMDA2MDAwLCJpYXQiOjE2MDAwMDAwMDAsImlzcyI6InlvdXJfaXNzdWVyIiwiYXVkIjoieW91cl9hdWRpZW5jZSJ9.qHF2bHU1RG0q8hOhX4FcGA1_v5hBplmW8kJRxwJz2kU";
        }
       return token; 
    }

    public static bool VerifyPhone(VerifyPhoneRequest verifyPhone) 
        => (verifyPhone.CodeCountry == "+57" && verifyPhone.Phone == "2222222222");

    public static string Registry(AuthenticationRequest request) => "User successfully created";

    public static List<ErrorReportDto> GenerateMockErrorData()
    {
        var mockData = new List<ErrorReportDto>();
        var random = new Random();

        for (int i = 0; i < 50; i++)
        {
            var dto = new ErrorReportDto(
                ErrorReport: $"ErrorReport_{i}",
                ErrorReportAdmitted: random.Next(0, 2) == 0 ? "admitted" : "not_admitted",
                Error: $"indexOutOfBoundsException: index: {random.Next(0, 10)}, Size: 4",
                BatteryPercentag: $"{random.Next(0, 101)}%",
                Status: random.Next(0, 2) == 0 ? "active" : "inactive",
                DeviceNumber: $"12433233-{i}",
                FirstSeen: DateTime.Now.AddDays(-random.Next(1, 1000))
            );

            mockData.Add(dto);
        }

        return mockData;
    }
}

