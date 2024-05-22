FROM mcr.microsoft.com/dotnet/aspnet:8.0-jammy AS base
WORKDIR /app
EXPOSE 5080
EXPOSE 5081
RUN apt-get update && apt-get install -y curl git ssh && apt-get clean
USER $APP_UID

FROM mcr.microsoft.com/dotnet/sdk:8.0-jammy AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/appsec-server/AppSec/AppSec.csproj", "src/appsec-server/AppSec/"]
RUN dotnet restore "src/appsec-server/AppSec/AppSec.csproj"
COPY . .
WORKDIR "/src/src/appsec-server/AppSec"
RUN dotnet build "AppSec.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "AppSec.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AppSec.dll"]

