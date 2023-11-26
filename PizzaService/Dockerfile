FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /PizzaService
EXPOSE 80

COPY *.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /PizzaService
COPY --from=build-env /PizzaService/out .
ENTRYPOINT ["dotnet", "PizzaService.dll"]