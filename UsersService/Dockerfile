FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /UsersService
EXPOSE 80

COPY *.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /UsersService
COPY --from=build-env /UsersService/out .
ENTRYPOINT ["dotnet", "UsersService.dll"]