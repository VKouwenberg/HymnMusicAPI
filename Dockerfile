# Use the official .NET SDK image as a build stage
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

# Set the working directory
WORKDIR /app

# Copy the solution file and restore dependencies
COPY HymnCoreAPI.sln .
COPY HymnBusinessAPI/HymnCoreAPI.csproj HymnBusinessAPI/
COPY ContractBusinessAPI/ContractBusinessAPI.csproj ContractBusinessAPI/
COPY ContractDataBusiness/ContractDataBusiness.csproj ContractDataBusiness/
COPY HymnBusiness/HymnBusiness.csproj HymnBusiness/
COPY HymnData/HymnData.csproj HymnData/
COPY HymnModels/HymnModels.csproj HymnModels/

RUN dotnet restore

# Copy the rest of the application
COPY . .

# Build the application
RUN dotnet publish HymnBusinessAPI/HymnCoreAPI.csproj -c Release -o out

# Use the official .NET runtime image as a runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .

# Set environment variables for Docker
ENV DOTNET_RUNNING_IN_CONTAINER=true
ENV ASPNETCORE_URLS=http://+:80

# Expose the ports
EXPOSE 7077
EXPOSE 5019
EXPOSE 443

# Set the entry point for the container
ENTRYPOINT ["dotnet", "HymnCoreAPI.dll"]
