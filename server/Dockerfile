# syntax=docker/dockerfile:1
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app
    
# Copy csproj and restore as distinct layers
COPY ./Project3/*.csproj ./Project3/
# COPY ./Project3.sln ./
RUN dotnet restore ./Project3/
    
# Copy everything else and build
COPY ./ ./
RUN dotnet publish -c Release -o out
    
# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env /app/out .

EXPOSE 7208

# ENTRYPOINT ["dotnet", "Project3.dll"]
CMD dotnet Project3.dll --urls=http://0.0.0.0:7208

