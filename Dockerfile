# Use the official .NET SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy csproj and restore dependencies
COPY *.csproj ./
RUN dotnet restore

# Copy the rest of the source and build
COPY . ./
RUN dotnet publish -c Release -o out

# Use the smaller runtime image for the final output
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .

# Start the app
ENTRYPOINT ["dotnet", "gfcf14-art-backend.dll"]