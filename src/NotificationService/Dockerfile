FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
EXPOSE 80

# Copy csproj and restore as distinct layers
COPY Carsties.slnx Carsties.slnx
COPY src/NotificationService/NotificationService.csproj src/NotificationService/NotificationService.csproj
COPY src/Contracts/Contracts.csproj src/Contracts/Contracts.csproj

# Restore package deps
RUN dotnet restore src/NotificationService/NotificationService.csproj

# Copy everything else and build
COPY . .
WORKDIR /src/src/NotificationService
RUN dotnet publish -c Release -o /app/publish

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "NotificationService.dll"]