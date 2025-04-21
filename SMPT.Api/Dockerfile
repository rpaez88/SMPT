FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["SMPT.Api/SMPT.Api.csproj", "SMPT.Api/"]
COPY ["SMPT.DataServices/SMPT.DataServices.csproj", "SMPT.DataServices/"]
COPY ["SMPT.Entities/SMPT.Entities.csproj", "SMPT.Entities/"]
RUN dotnet restore "SMPT.Api/SMPT.Api.csproj"
COPY . .
WORKDIR "/src/SMPT.Api"
RUN dotnet build "SMPT.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "SMPT.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SMPT.Api.dll"]
