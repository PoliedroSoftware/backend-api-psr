# Etapa base
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8081

# Etapa de construcción
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copiar archivos de proyecto y restaurar dependencias
COPY ["Poliedro.Psr.Api/Poliedro.Psr.Api.csproj", "Poliedro.Psr.Api/"]
COPY ["Poliedro.Psr.Application/Poliedro.Psr.Application.csproj", "Poliedro.Psr.Application/"]
COPY ["Poliedro.Psr.Domain/Poliedro.Psr.Domain.csproj", "Poliedro.Psr.Domain/"]
COPY ["Poliedro.Psr.Infraestructure.External.Azure/Poliedro.Psr.Infraestructure.External.Azure.csproj", "Poliedro.Psr.Infraestructure.External.Azure/"]
COPY ["Poliedro.Psr.Infraestructure.Worker/Poliedro.Psr.Infraestructure.Worker.csproj", "Poliedro.Psr.Infraestructure.Worker/"]
COPY ["Poliedro.Psr.Infraestructure.External.RabbitMQ/Poliedro.Psr.Infraestructure.External.RabbitMQ.csproj", "Poliedro.Psr.Infraestructure.External.RabbitMQ/"]

RUN dotnet restore "./Poliedro.Psr.Api/Poliedro.Psr.Api.csproj"

# Copiar el resto de los archivos y construir el proyecto
COPY . .
WORKDIR "/src/Poliedro.Psr.Api"
RUN dotnet build "./Poliedro.Psr.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Etapa de publicación
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Poliedro.Psr.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Etapa final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Poliedro.Psr.Api.dll"]