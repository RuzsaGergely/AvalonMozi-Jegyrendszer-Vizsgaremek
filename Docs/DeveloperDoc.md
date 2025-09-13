# Avalon Mozi Jegyrendszer - Fejlesztői dokumentáció

## Adatbázis

A szoftver jelenlegi implementációjában MSSQL adatbázist használunk adattárolásra melyet Entity Framework keretrendszerrel kezelünk szoftveres oldalon.

```
Fontos! Az EF rugalmas jellege miatt bármilyen EF kompatibilis adatbázist be lehet rakni a rendszer alá (pl. PostgreSQL, SQLite, MySQL, stb...).
```

### Táblák

**Users** tábla:

Ebben a táblában tároljuk a felhasználók adatait.

| Mező neve               | Mező típusa   | NULL engedélyezve? |
|-------------------------|---------------|--------------------|
| Id                      | int           | Unchecked          |
| TechnicalId             | nvarchar(MAX) | Unchecked          |
| FirstName               | nvarchar(MAX) | Unchecked          |
| LastName                | nvarchar(MAX) | Unchecked          |
| Email                   | nvarchar(MAX) | Unchecked          |
| Phone                   | nvarchar(MAX) | Checked            |
| PasswordHash            | nvarchar(MAX) | Unchecked          |
| LastSuccessfulLoginTime | datetime2(7)  | Unchecked          |
| Deleted                 | bit           | Unchecked          |

**Roles** tábla:

Ebben a táblában tároljuk a rendszer által kezelt jogosultságokat, szerepköröket.

| Mező neve     | Mező típusa   | NULL engedélyezve? |
|---------------|---------------|--------------------|
| Id            | int           | Unchecked          |
| TechnicalName | nvarchar(MAX) | Unchecked          |
| DisplayName   | nvarchar(MAX) | Unchecked          |
| Deleted       | bit           | Unchecked          |

**RoleUser** tábla:

Ebben a táblában tároljuk a felhasználók (Users tábla) és a szerepkörök (Roles) közötti összekötéseket, tehát azt, hogy melyik felhasználónak milyen jogai vannak.

| Mező neve | Mező típusa | NULL engedélyezve? |
|-----------|-------------|--------------------|
| RolesId   | int         | Unchecked          |
| UsersId   | int         | Unchecked          |

**BillingInformations** tábla:

Ebben a táblában tároljuk a felhasználók számlázási adatait. Egy felhasználóhoz több számlázási adat is tartozhat.

| Mező neve   | Mező típusa   | NULL engedélyezve? |
|-------------|---------------|--------------------|
| Id          | int           | Unchecked          |
| Name        | nvarchar(MAX) | Unchecked          |
| CompanyName | nvarchar(MAX) | Checked            |
| VATNumber   | nvarchar(MAX) | Checked            |
| Address1    | nvarchar(MAX) | Unchecked          |
| Address2    | nvarchar(MAX) | Checked            |
| ZipCode     | nvarchar(MAX) | Unchecked          |
| City        | nvarchar(MAX) | Unchecked          |
| County      | nvarchar(MAX) | Unchecked          |
| Deleted     | bit           | Unchecked          |
| UserId      | int           | Checked            |
| TechnicalId | nvarchar(MAX) | Unchecked          |

**Feedbacks** tábla:

Ebben a táblában tároljuk a weboldalon leadott visszajelzéseket.

| Mező neve | Mező típusa   | NULL engedélyezve? |
|-----------|---------------|--------------------|
| Id        | int           | Unchecked          |
| Name      | nvarchar(MAX) | Unchecked          |
| Email     | nvarchar(MAX) | Checked            |
| Phone     | nvarchar(MAX) | Checked            |
| Message   | nvarchar(MAX) | Unchecked          |
| Deleted   | bit           | Unchecked          |

**Movies** tábla:

Ebben a táblában tároljuk a filmek alapadatait.

| Mező neve        | Mező típusa   | NULL engedélyezve? |
|------------------|---------------|--------------------|
| Id               | int           | Unchecked          |
| TechnicalId      | nvarchar(MAX) | Unchecked          |
| Title            | nvarchar(MAX) | Unchecked          |
| SeoFriendlyTitle | nvarchar(MAX) | Unchecked          |
| Description      | nvarchar(MAX) | Unchecked          |
| AgeRestriction   | nvarchar(MAX) | Unchecked          |
| TicketPrice      | int           | Unchecked          |
| Deleted          | bit           | Unchecked          |
| CoverImageBase64 | nvarchar(MAX) | Checked            |

**MovieDates** tábla:

Ebben a táblában tároljuk az egyes filmekhez tartozó vetítési időpontokat.

| Mező neve   | Mező típusa   | NULL engedélyezve? |
|-------------|---------------|--------------------|
| Id          | int           | Unchecked          |
| TechnicalId | nvarchar(MAX) | Unchecked          |
| DateFrom    | datetime2(7)  | Unchecked          |
| DateTo      | datetime2(7)  | Unchecked          |
| MovieId     | int           | Checked            |
| Deleted     | bit           | Unchecked          |

**Orders** tábla:

Ebben a táblában tároljuk a rendelések alapadatait.

| Mező neve     | Mező típusa   | NULL engedélyezve? |
|---------------|---------------|--------------------|
| Id            | int           | Unchecked          |
| TechnicalId   | nvarchar(MAX) | Unchecked          |
| UserId        | int           | Unchecked          |
| BillingInfoId | int           | Unchecked          |
| PriceSumGross | int           | Unchecked          |
| PriceSumNet   | int           | Unchecked          |
| Deleted       | bit           | Unchecked          |

**OrderItems** tábla:

Ebben a táblában tároljuk a rendelésekhez tartozó rendelési elemeket.

| Mező neve          | Mező típusa   | NULL engedélyezve? |
|--------------------|---------------|--------------------|
| Id                 | int           | Unchecked          |
| TechnicalId        | nvarchar(MAX) | Unchecked          |
| MovieId            | int           | Unchecked          |
| SelectedDateTimeId | int           | Unchecked          |
| OrderId            | int           | Checked            |
| Deleted            | bit           | Unchecked          |

**Tickets** tábla:

Ebben a táblában tároljuk a rendelési elemekhez legenerált jegyek adatait.

| Mező neve    | Mező típusa   | NULL engedélyezve? |
|--------------|---------------|--------------------|
| Id           | int           | Unchecked          |
| TicketData   | nvarchar(MAX) | Unchecked          |
| AssignedToId | int           | Unchecked          |
| Deleted      | bit           | Unchecked          |

### Adatbázis diagram

![](./DeveloperDoc/DeveloperDoc_1.png)

## Backend

### Architektúra

A rendszer DDD (Domain Driven Design) elvekre épít. Ennek fényében minden izoláltan található meg a kódbázisban (perzisztencia réteg, application réteg, factory réteg, stb.).

A rendszerben az entitások és a DTO-k (Data Transfer Object) közötti konverziók factory-kon keresztül valósul meg. A service-ek által használt DTO-k (tehát amiknek nincsen entitás oldalon megfelelője) az application rétegben találhatóak, a többi a factory rétegben érhető el.

A projektben teljes terjedelemben DI-vel (Dependency Injection) operál, ezen keresztül tölti be a service-ket és factory-kat, szemben az állandó objektum létrehozással.

#### Szolgáltatások (service)

```
AvalonMozi.Application/
│
│   ApplicationExtensions.cs
│
├───Feedbacks
│   │   FeedbackService.cs
│   │   IFeedbackService.cs
│   │
│   └───Dto
│           FeedbackDto.cs
│
├───Movies
│   └───Services
│           IMovieService.cs
│           MovieService.cs
│
├───Orders
│   ├───Dto
│   │       OrderItemRequestDto.cs
│   │       OrderRequestDto.cs
│   │
│   └───Services
│           IOrderService.cs
│           OrderService.cs
│
├───Tickets
│   ├───Dto
│   │       TicketCheckResponseDto.cs
│   │       UserTicketDto.cs
│   │
│   └───Services
│           ITicketService.cs
│           TicketService.cs
│
└───Users
    ├───Dto
    │       UserRegisterDto.cs
    │
    └───Services
            IJwtAuthManager.cs
            IUserService.cs
            JwtAuthManager.cs
            UserService.cs
```

| Service neve    | Service feladata                                                                               |
|-----------------|------------------------------------------------------------------------------------------------|
| FeedbackService | Ez a szolgáltatás kezeli a weboldalon leadott visszajelzéseket. (C)                            |
| MovieService    | Ez a szolgáltatás kezeli a filmeket a rendszerben. (CRUD)                                      |
| OrderService    | Ez a szolgáltatás kezeli a weboldalon leadott rendeléseket. (CRUD)                             |
| TicketService   | Ez a szolgáltatás kezeli a leadott rendelések folytán legenerált belépőjegyeket. (CR)          |
| UserService     | Ez a szolgáltatás kezeli a felhasználókat és a felhasználók hitelesítését a rendszerben. (CR)  |
| JwtAuthManager  | Ez a szolgáltatás kezeli az authentikációhoz szükséges JWT tokenek előállítását.               |

#### Factory-k

```
AvalonMozi.Factories/
│
│   AvalonMozi.Factories.csproj
│   FactoryExtensions.cs
│
├───MovieFactories
│   │   MovieFactory.cs
│   │
│   └───Dto
│           MovieDateDto.cs
│           MovieDto.cs
│
├───OrderFactories
│   │   OrderFactory.cs
│   │
│   └───Dto
│           BillingInformationDto.cs
│           OrderDto.cs
│           OrderItemDto.cs
│
└───UserFactories
    │   RoleFactory.cs
    │   UserFactory.cs
    │
    └───Dto
            RoleDto.cs
            UserDto.cs
```

| Factory neve | Factory feladata                                                      |
|--------------|-----------------------------------------------------------------------|
| MovieFactory | Ez a factory alakítja át a filmek entitásait DTO-vá és fordítva       |
| OrderFactory | Ez a factory alakítja át a rendelések entitásait DTO-vá és fordítva   |
| UserFactory  | Ez a factory alakítja át a felhasználók entitásait DTO-vá és fordítva |

#### Controller-ek

```
AvalonMozi.Backend/
│
└───Controllers
        FeedbackController.cs
        MovieController.cs
        OrderController.cs
        TicketController.cs
        UserController.cs
```

| Controller neve    | Controller hatásköre                                    |
|--------------------|---------------------------------------------------------|
| FeedbackController | Ez a controller kezeli a visszajelzések API végpontjait |
| MovieController    | Ez a controller kezeli a filmek API végpontjait         |
| OrderController    | Ez a controller kezeli a rendelések API végpontjait     |
| TicketController   | Ez a controller kezeli a jegyek API végpontjait         |
| UserController     | Ez a controller kezeli a felhasználók API végpontjait   |

### Authentikáció

A rendszerben az autentikáció JWT token alapon működik. Minden token névreszólóan tartalmazza a felhasználó technikai azonosítóját valamint a felhasználó által birtokolt szerepköröket.

**Példa token:**

```
eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJlbWFpbCI6InZpenNnYXJlbWVrLmFkbWluQHRlc3RkZXYuaHUiLCJ1bmlxdWVfbmFtZSI6IkFkbWluIFZpenNnYXJlbWVrIiwiVXNlclRlY2huaWNhbElkIjoiNmNjYmY3ZmQtMTIwZC00NzJjLWI4OWQtMTU2MWE4YTE4M2VhIiwicm9sZSI6WyJBRE1JTiIsIkNVU1RPTUVSIl0sIm5iZiI6MTc1Nzc3OTkxMywiZXhwIjoxNzU3Nzg3MTEzLCJpYXQiOjE3NTc3Nzk5MTMsImlzcyI6Imh0dHBzOi8vYXZhbG9ubW96aS50ZXN0ZGV2Lmh1IiwiYXVkIjoiaHR0cHM6Ly9hdmFsb25tb3ppLnRlc3RkZXYuaHUifQ.8XzodCoEtvaTiXgcziePZsURKaXwK2__88RB4SquXw8
```

**JWT.io dekódolt adat:**

```json
{
  "email": "vizsgaremek.admin@testdev.hu",
  "unique_name": "Admin Vizsgaremek",
  "UserTechnicalId": "6ccbf7fd-120d-472c-b89d-1561a8a183ea",
  "role": [
    "ADMIN",
    "CUSTOMER"
  ],
  "nbf": 1757779913,
  "exp": 1757787113,
  "iat": 1757779913,
  "iss": "https://avalonmozi.testdev.hu",
  "aud": "https://avalonmozi.testdev.hu"
}
```

#### Authentikáció folyamata

1. A felületen / Swagger UI-on beküldjük a requestet az `/api/User/Login` enpointon. A request paraméterek URL-ben mennek.

```
curl -X 'POST' \
  'https://localhost:7285/api/User/Login?email=vizsgaremek.admin%40testdev.hu&password=12345' \
  -H 'accept: application/json' \
  -d ''
```

2. A `userService` megkapja az `AuthenticateUser()` metódusban az adatot.
3. A service-ben megkeressük az adatbázisból a felhasználót és összevetjük a hash értékeket.
4. Sikeres azonosítás esetén visszaadjuk a felhasználó entitásából gyártott DTO-t.

```csharp
public async Task<UserDto> AuthenticateUser(string email, string password)
{
    var user = await _database.Users.Include(x=>x.Roles).Where(x => x.Email == email && HashPassword(password) == x.PasswordHash).FirstOrDefaultAsync();
    if(user is null)
    {
        return null;
    }
    return _userFactory.ConvertEntityToDto(user);
}
```

5. A legyártott `UserDto` objektumot átadjuk a `JWTAuthManager` service `GenerateToken()` metódusának.
6. A service-ben legyártjuk a tokenjét a felhasználónak. A generálás során `claim` paraméterként adjuk meg a felhasználó szerepkörét és technikai azonosítóját a felhasználónak.

```csharp
public string GenerateToken(UserDto user)
{
    var jwtSettings = _configuration.GetSection("Jwt");

    var key = Encoding.ASCII.GetBytes(jwtSettings["Key"]);

    var claimList = new List<Claim>()
    {
        new Claim(ClaimTypes.Email, user.Email),
        new Claim(ClaimTypes.Name, user.FirstName + " " + user.LastName),
        new Claim("UserTechnicalId", user.TechnicalId)
    };

    foreach (var role in user.Roles)
    {
        claimList.Add(new Claim(ClaimTypes.Role, role.TechnicalName));
    }

    var tokenDescriptor = new SecurityTokenDescriptor
    {
        
        Subject = new ClaimsIdentity(claimList),
        Expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(jwtSettings["DurationInMinutes"])),
        Issuer = jwtSettings["Issuer"],
        Audience = jwtSettings["Audience"],
        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
    };

    var tokenHandler = new JwtSecurityTokenHandler();
    var token = tokenHandler.CreateToken(tokenDescriptor);
    return tokenHandler.WriteToken(token);
}
```

7. Visszaadjuk a tokent válaszként a requestre.

### NSwag generálás

A backend build folyamat szerves része az NSwag API generálása. Ennek segítségével a buildelés után automatikusan rendelkezésünkre áll az API végpontokat leképző Typescript nyelvű service.

Konfigurálása C#-ban:

```xml
<Target Name="NSwag" AfterTargets="PostBuildEvent" Condition=" '$(Configuration)' == 'Debug' ">
    <Message Importance="High" Text="$(NSwagExe_Net80) run nswag.json /variables:Configuration=$(Configuration)" />
    <Exec WorkingDirectory="$(ProjectDir)" EnvironmentVariables="ASPNETCORE_ENVIRONMENT=Development" Command="$(NSwagExe_Net80) run nswag.json /variables:Configuration=$(Configuration)" />
    <Delete Files="$(ProjectDir)\obj\$(MSBuildProjectFile).NSwag.targets" />
</Target>
```

nswag.json:

```json
{
  "runtime": "Net80",
  "defaultVariables": null,
  "documentGenerator": {
    "aspNetCoreToOpenApi": {
      "project": "AvalonMozi.Backend.csproj",
      "msBuildProjectExtensionsPath": null,
      "configuration": null,
      "runtime": null,
      "targetFramework": null,
      "noBuild": true,
      "verbose": false,
      "workingDirectory": null,
      "requireParametersWithoutDefault": true,
      "apiGroupNames": null,
      "defaultPropertyNameHandling": "Default",
      "defaultReferenceTypeNullHandling": "Null",
      "defaultDictionaryValueReferenceTypeNullHandling": "NotNull",
      "defaultResponseReferenceTypeNullHandling": "NotNull",
      "defaultEnumHandling": "Integer",
      "flattenInheritanceHierarchy": false,
      "generateKnownTypes": true,
      "generateEnumMappingDescription": false,
      "generateXmlObjects": false,
      "generateAbstractProperties": false,
      "generateAbstractSchemas": true,
      "ignoreObsoleteProperties": false,
      "allowReferencesWithProperties": false,
      "excludedTypeNames": [],
      "serviceHost": null,
      "serviceBasePath": null,
      "serviceSchemes": [],
      "infoTitle": "RESC API",
      "infoDescription": null,
      "infoVersion": "1.0.0",
      "documentTemplate": null,
      "documentProcessorTypes": [],
      "operationProcessorTypes": [],
      "typeNameGeneratorType": null,
      "schemaNameGeneratorType": null,
      "contractResolverType": null,
      "serializerSettingsType": null,
      "useDocumentProvider": true,
      "documentName": "v1",
      "aspNetCoreEnvironment": null,
      "createWebHostBuilderMethod": null,
      "startupType": null,
      "allowNullableBodyParameters": true,
      "output": "wwwroot/api/specification.json",
      "outputType": "OpenApi3",
      "assemblyPaths": [],
      "assemblyConfig": null,
      "referencePaths": [],
      "useNuGetCache": false
    }
  },
  "codeGenerators": {
    "openApiToTypeScriptClient": {
      "className": "{controller}Client",
      "moduleName": "",
      "namespace": "",
      "typeScriptVersion": 4.7,
      "template": "Angular",
      "promiseType": "Promise",
      "httpClass": "HttpClient",
      "withCredentials": false,
      "useSingletonProvider": true,
      "injectionTokenType": "InjectionToken",
      "rxJsVersion": 7.5,
      "dateTimeType": "Date",
      "nullValue": "Undefined",
      "generateClientClasses": true,
      "generateClientInterfaces": true,
      "generateOptionalParameters": false,
      "exportTypes": true,
      "wrapDtoExceptions": false,
      "exceptionClass": "SwaggerException",
      "clientBaseClass": null,
      "wrapResponses": false,
      "wrapResponseMethods": [],
      "generateResponseClasses": true,
      "responseClass": "SwaggerResponse",
      "protectedMethods": [],
      "configurationClass": null,
      "useTransformOptionsMethod": false,
      "useTransformResultMethod": false,
      "generateDtoTypes": true,
      "operationGenerationMode": "MultipleClientsFromOperationId",
      "markOptionalProperties": false,
      "generateCloneMethod": false,
      "typeStyle": "Class",
      "classTypes": [],
      "extendedClasses": [],
      "extensionCode": null,
      "generateDefaultValues": true,
      "excludedTypeNames": [],
      "excludedParameterNames": [],
      "handleReferences": false,
      "generateConstructorInterface": true,
      "convertConstructorInterfaceData": false,
      "importRequiredTypes": true,
      "useGetBaseUrlMethod": false,
      "baseUrlTokenName": "API_BASE_URL",
      "queryNullValue": "",
      "inlineNamedDictionaries": false,
      "inlineNamedAny": false,
      "templateDirectory": null,
      "typeNameGeneratorType": null,
      "propertyNameGeneratorType": null,
      "enumNameGeneratorType": null,
      "serviceHost": null,
      "serviceSchemes": null,
      "output": "../../AvalonMozi-Frontend/src/services/moziHttpClient.ts"
    }
  }
}
```

## Frontend

### Komponensek

```
src/app/
│   app.config.ts
│   app.css
│   app.html
│   app.routes.ts
│   app.spec.ts
│   app.ts
│
├───admin-dashboard
│       admin-dashboard.css
│       admin-dashboard.html
│       admin-dashboard.spec.ts
│       admin-dashboard.ts
│
├───admin-landing
│       admin-landing.css
│       admin-landing.html
│       admin-landing.spec.ts
│       admin-landing.ts
│
├───admin-ticketcheck
│       admin-ticketcheck.css
│       admin-ticketcheck.html
│       admin-ticketcheck.spec.ts
│       admin-ticketcheck.ts
│
├───cart
│       cart.css
│       cart.html
│       cart.spec.ts
│       cart.ts
│
├───checkout
│       checkout.css
│       checkout.html
│       checkout.spec.ts
│       checkout.ts
│
├───common
│       interfaces.ts
│
├───contact
│       contact.css
│       contact.html
│       contact.spec.ts
│       contact.ts
│
├───feedback
│       feedback.css
│       feedback.html
│       feedback.spec.ts
│       feedback.ts
│
├───footer
│       footer.css
│       footer.html
│       footer.spec.ts
│       footer.ts
│
├───header
│       header.css
│       header.html
│       header.spec.ts
│       header.ts
│
├───home
│       home.css
│       home.html
│       home.spec.ts
│       home.ts
│
├───localshared
│       local-shared-module.ts
│
├───login
│       login.css
│       login.html
│       login.spec.ts
│       login.ts
│
├───movie-by-seotitle
│       movie-by-seotitle.css
│       movie-by-seotitle.html
│       movie-by-seotitle.spec.ts
│       movie-by-seotitle.ts
│
├───movies
│       movies.css
│       movies.html
│       movies.spec.ts
│       movies.ts
│
├───register
│       register.css
│       register.html
│       register.spec.ts
│       register.ts
│
└───tickets
        tickets.css
        tickets.html
        tickets.spec.ts
        tickets.ts
```

| Komponens neve    | Komponens feladata                                                                                                          |
|-------------------|-----------------------------------------------------------------------------------------------------------------------------|
| admin-dashboard   | Ez tartalmazza az adminisztrációs felület dashboardját                                                                      |
| admin-landing     | Ez tartalmazza az adminisztrációs felület főmenüjét. Telefonos használatra tervezve nagy gombokkal van ellátva              |
| admin-ticketcheck | Ez tartalmazza a QR kódos jegyellenőrzés felületét.                                                                         |
| cart              | Ez tartalmazza a kosár felületét.                                                                                           |
| checkout          | Ez tartalmazza a pénztár felületét.                                                                                         |
| common            | A Common komponensben a felületen használt interfészeket találjuk amelyeknek nincs kötődésük a backendhez. (pl. kosár)      |
| contact           | Ez tartalmazza a kapcsolati oldal felületét.                                                                                |
| feedback          | Ez tartalmazza a visszajelzési oldal felületét.                                                                             |
| footer            | Ez a komponens tartalmazza az oldal láblécét. Ezt a komponenst az admin felületek kivételével minden oldalon behivatkozzuk. |
| header            | Ez a komponens tartalmazza az oldal fejlécét. Ezt a komponenst az admin felületek kivételével minden oldalon behivatkozzuk. |
| home              | Ez a komponens tartalmazza a főoldal tartalmát.                                                                             |
| localshared       | Ez egy gyűjtőkomponens amiben midnen használt modult beimportálunk, ezzel konszolidálva a modulok hivatkozását.             |
| login             | Ez a komponens tartalmazza a bejelentkezési oldalt.                                                                         |
| movie-by-seotitle | Ez a komponens tartalmazza a megnyitott film aloldalát.                                                                     |
| movies            | Ez a komponens tartalmazza a filmeket megjelenítő oldalt.                                                                   |
| register          | Ez a komponens tartalmazza a regisztrációs oldalt.                                                                          |
| tickets           | Ez a komponens tartalmazza a felhasználó jegyeit megjelenítő oldalt.                                                        |

### Service-ek

```
src/services
    auth-service.spec.ts
    auth-service.ts
    carthandler.service.ts
    carthandler.spec.ts
    jwt-interceptor.service.spec.ts
    jwt-interceptor.service.ts
    moziHttpClient.ts
```

| Service neve            | Service feladata                                                                                                                                                                         |
|-------------------------|------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| Auth-Service            | Ez a service felelős a weboldal autentikáció kezeléséért, valamint a felhasználói profil betöltéséért.                                                                                   |
| CartHandler             | Ez a service felelős a weboldal kosár kezeléséért.                                                                                                                                       |
| JWT-Interceptor-Service | Ez a service felelős azért, hogy minden kimenő API hívásra automatikusan rárakja a felhasználó JWT tokenjét.                                                                             |
| moziHttpClient          | Ez a service felelős minden API kapcsolat megvalósításáért minden DTO-val és interfésszel együtt. Ezt a service fájlt a backend automatikusan generálja build közben NSwag segítségével. |
