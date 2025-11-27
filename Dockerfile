# 1. Build aşaması
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Proje dosyalarını kopyala ve restore et
COPY *.sln .
COPY WebApi/*.csproj ./WebApi/
RUN dotnet restore

# Tüm projeyi kopyala ve publish et
COPY . .
RUN dotnet publish -c Release -o /app/publish

# 2. Runtime aşaması
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "WebApi.dll"]

# Portu expose et
EXPOSE 80
