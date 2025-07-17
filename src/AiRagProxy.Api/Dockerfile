FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["AiRagProxy.Api/AiRagProxy.Api.csproj", "AiRagProxy.Api/"]
COPY ["AiRagProxy.Domain/AiRagProxy.Domain.csproj", "AiRagProxy.Domain/"]
COPY ["AiRagProxy.Storage/AiRagProxy.Storage.csproj", "AiRagProxy.Storage/"]
RUN dotnet restore "AiRagProxy.Api/AiRagProxy.Api.csproj"
COPY . .
WORKDIR "/src/AiRagProxy.Api"
RUN dotnet build "./AiRagProxy.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./AiRagProxy.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AiRagProxy.Api.dll"]
