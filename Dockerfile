# Use the official .NET 7 SDK image as a build environment
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
WORKDIR /app

# Copy only the csproj file and restore dependencies
COPY *.csproj ./
RUN dotnet restore --disable-parallel

# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build-env /app/out .

# Expose ports 80 and 443
EXPOSE 80
EXPOSE 443

# Set ASP.NET Core environment (default)
ENV ASPNETCORE_ENVIRONMENT=Development

# Set the entry point for the container
ENTRYPOINT ["dotnet", "DataCompressionAPI.dll"]