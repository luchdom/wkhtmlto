
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
# Download the necessary packages to execute wkhtml 
RUN apt-get update && \
    apt-get install -y --no-install-recommends libxrender1 libxext6 libfontconfig libjpeg62-turbo libpng16-16 && \
    rm -rf /var/lib/apt/lists/*
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Samples/WkHtmlSampleApi/WkHtmlSampleApi.csproj", "src/Samples/WkHtmlSampleApi/"]
RUN dotnet restore "./src/Samples/WkHtmlSampleApi/./WkHtmlSampleApi.csproj"
COPY . .
WORKDIR "/src/src/Samples/WkHtmlSampleApi"
RUN dotnet build "./WkHtmlSampleApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./WkHtmlSampleApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WkHtmlSampleApi.dll"]