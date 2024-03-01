FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src
COPY "MySanpeteWeb/MySanpeteWeb.csproj" .
RUN dotnet restore
COPY . .
RUN dotnet publish /src/MySanpeteWeb/MySanpeteWeb.csproj -c release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:8.0 as final
WORKDIR /app
EXPOSE 80
EXPOSE 443
COPY --from=build /app .
ENTRYPOINT ["dotnet", "MySanpeteWeb.dll"]