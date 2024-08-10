FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["LeaveTimes.API/LeaveTimes.API.csproj", "LeaveTimes.API/"]
COPY ["LeaveTimes.Domain/LeaveTimes.Domain.csproj", "LeaveTimes.Domain/"]
COPY ["LeaveTimes.Application/LeaveTimes.Application.csproj", "LeaveTimes.Application/"]
COPY ["LeaveTimes.Infrastructure/LeaveTimes.Infrastructure.csproj", "LeaveTimes.Infrastructure/"]
RUN dotnet restore "./LeaveTimes.API/LeaveTimes.API.csproj"
COPY . .
WORKDIR "/src/LeaveTimes.API"
RUN dotnet build "LeaveTimes.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "LeaveTimes.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LeaveTimes.API.dll"]