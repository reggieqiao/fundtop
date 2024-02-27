#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Fundtop.Crawler/Fundtop.Crawler.csproj", "Fundtop.Crawler/"]
COPY ["Fundtop.Repositories/Fundtop.Repositories.csproj", "Fundtop.Repositories/"]
COPY ["Fundtop.Core/Fundtop.Core.csproj", "Fundtop.Core/"]
COPY ["Fundtop.Models/Fundtop.Models.csproj", "Fundtop.Models/"]
RUN dotnet restore "Fundtop.Crawler/Fundtop.Crawler.csproj"
COPY . .
WORKDIR "/src/Fundtop.Crawler"
RUN dotnet build "Fundtop.Crawler.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Fundtop.Crawler.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Fundtop.Crawler.dll"]