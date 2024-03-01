#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Fundtop.Web/Fundtop.Web.csproj", "Fundtop.Web/"]
COPY ["Fundtop.Models/Fundtop.Models.csproj", "Fundtop.Models/"]
COPY ["Fundtop.Repositories/Fundtop.Repositories.csproj", "Fundtop.Repositories/"]
COPY ["Fundtop.Core/Fundtop.Core.csproj", "Fundtop.Core/"]
RUN dotnet restore "Fundtop.Web/Fundtop.Web.csproj"
COPY . .
WORKDIR "/src/Fundtop.Web"
RUN dotnet build "Fundtop.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Fundtop.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Fundtop.Web.dll"]