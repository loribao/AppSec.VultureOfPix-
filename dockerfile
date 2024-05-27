FROM mcr.microsoft.com/dotnet/sdk:8.0-jammy AS sonar
RUN apt-get update && apt-get install -y openjdk-17-jdk  inetutils-ping && apt-get clean;
ENV JAVA_HOME /usr/lib/jvm/java-17-openjdk-amd64/
RUN export JAVA_HOME
RUN dotnet tool install --global dotnet-sonarscanner
ENV PATH="${PATH}:/root/.dotnet/tools"
WORKDIR /src
COPY . .
ARG BUILD_CONFIGURATION=Release
ARG SONARQUBE_URL=http://host.docker.internal:9000
ARG SONARQUBE_TOKEN
RUN dotnet restore "./src/appsec-server/AppSec/AppSec.csproj"

RUN dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover

RUN dotnet sonarscanner begin /k:"appsec-server" /d:sonar.host.url=${SONARQUBE_URL} /d:sonar.token=${SONARQUBE_TOKEN} /d:sonar.cs.opencover.reportsPaths="test/**/coverage.opencover.xml"
RUN dotnet build "./src/appsec-server/AppSec/AppSec.csproj" -c $BUILD_CONFIGURATION
RUN dotnet sonarscanner end /d:sonar.token=${SONARQUBE_TOKEN}
