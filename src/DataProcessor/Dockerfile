FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/DataProcessor/DataProcessor.csproj", "src/DataProcessor/"]
COPY ["src/DataProcessor.Infrastructure/DataProcessor.Infrastructure.csproj", "src/DataProcessor.Infrastructure/"]
COPY ["src/DataProcessor.Application/DataProcessor.Application.csproj", "src/DataProcessor.Application/"]
COPY ["src/DataProcessor.Domain/DataProcessor.Domain.csproj", "src/DataProcessor.Domain/"]
RUN dotnet restore "./src/DataProcessor/./DataProcessor.csproj"
COPY . .
WORKDIR "/src/src/DataProcessor"
RUN dotnet build "./DataProcessor.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./DataProcessor.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DataProcessor.dll"]