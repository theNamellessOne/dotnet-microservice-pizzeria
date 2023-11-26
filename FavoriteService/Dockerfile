FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /FavoriteService
EXPOSE 80

COPY *.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /FavoriteService
COPY --from=build-env /FavoriteService/out .
ENTRYPOINT ["dotnet", "FavoriteService.dll"]