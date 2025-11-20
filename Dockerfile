# 1. Etapa de construcción del proyecto
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copia todos los archivos
COPY . .

# Publicar en Release
RUN dotnet publish -c Release -o /app/publish

# 2. Etapa final (runtime)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app

# Copiar lo publicado
COPY --from=build /app/publish .

# Render usa la variable $PORT, así que configuramos ASP.NET para usarla
ENV ASPNETCORE_URLS=http://0.0.0.0:${PORT}

# Iniciar la aplicación (NOMBRE REAL DEL DLL)
ENTRYPOINT ["dotnet", "ReportesAPI.dll"]
