#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Inventory/Inventory.API/Inventory.API.csproj", "Inventory/Inventory.API/"]
COPY ["Inventory/Inventory.BL/Inventory.BL.csproj", "Inventory/Inventory.BL/"]
COPY ["Inventory/Inventory.Domain/Inventory.Domain.csproj", "Inventory/Inventory.Domain/"]
COPY ["Inventory/Inventry.Infrastructure/Inventory.Infrastructure.csproj", "Inventory/Inventry.Infrastructure/"]
RUN dotnet restore "Inventory/Inventory.API/Inventory.API.csproj"
COPY . .
WORKDIR "/src/Inventory/Inventory.API"
RUN dotnet build "Inventory.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Inventory.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Inventory.API.dll"]
