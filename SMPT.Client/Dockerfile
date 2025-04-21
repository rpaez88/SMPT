FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["SMPT.Client/SMPT.Client.csproj", "SMPT.Client/"]
COPY ["SMPT.Entities/SMPT.Entities.csproj", "SMPT.Entities/"]
RUN dotnet restore "SMPT.Client/SMPT.Client.csproj"
COPY . .
WORKDIR "/src/SMPT.Client"
RUN dotnet build "SMPT.Client.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "SMPT.Client.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SMPT.Client.dll"]
