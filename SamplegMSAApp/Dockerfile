FROM mcr.microsoft.com/dotnet/aspnet:8.0-nanoserver-ltsc2022 AS base
WORKDIR /app
EXPOSE 8080


FROM mcr.microsoft.com/dotnet/sdk:8.0-nanoserver-ltsc2022 AS build
ARG BUILD_CONFIGURATION=Development
WORKDIR /src
COPY ["SamplegMSAApp/SamplegMSAApp.csproj", "SamplegMSAApp/"]
RUN dotnet restore "./SamplegMSAApp/SamplegMSAApp.csproj"
COPY . .
WORKDIR "/src/SamplegMSAApp"
RUN dotnet add package System.Configuration.ConfigurationManager
RUN dotnet add package Microsoft.AspNet.WebPages.WebData --version 3.3.0
RUN dotnet build "./SamplegMSAApp.csproj" -c %BUILD_CONFIGURATION% -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Development

RUN dotnet publish "./SamplegMSAApp.csproj" -c %BUILD_CONFIGURATION% -o /app/publish /p:UseAppHost=false /p:Environment=Development
RUN dir

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Additional command line arguments should be handled in Program.cs
ENTRYPOINT ["dotnet", "SamplegMSAApp.dll"]