# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["FMSEvaluering.Api/FMSEvaluering.Api.csproj", "FMSEvaluering.Api/"]
COPY ["FMSEvaluering.Application/FMSEvaluering.Application.csproj", "FMSEvaluering.Application/"]
COPY ["FMSEvaluering.Domain/FMSEvaluering.Domain.csproj", "FMSEvaluering.Domain/"]
COPY ["FMSEvaluering.Infrastructure/FMSEvaluering.Infrastructure.csproj", "FMSEvaluering.Infrastructure/"]

COPY ["FMSEvaluering.DatabaseMigration/FMSEvaluering.DatabaseMigration.csproj", "FMSEvaluering.DatabaseMigration/"]
RUN dotnet restore "./FMSEvaluering.Api/FMSEvaluering.Api.csproj"
COPY . .
WORKDIR "/src/FMSEvaluering.Api"
RUN dotnet build "./FMSEvaluering.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./FMSEvaluering.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FMSEvaluering.Api.dll"]