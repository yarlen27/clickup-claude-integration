# Multi-stage build for .NET API
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/ClickUp.Web.API/ClickUp.Web.API.csproj", "src/ClickUp.Web.API/"]
COPY ["src/ClickUp.Core/ClickUp.Core.csproj", "src/ClickUp.Core/"]
COPY ["src/ClickUp.Infrastructure/ClickUp.Infrastructure.csproj", "src/ClickUp.Infrastructure/"]
RUN dotnet restore "src/ClickUp.Web.API/ClickUp.Web.API.csproj"
COPY . .
WORKDIR "/src/src/ClickUp.Web.API"
RUN dotnet build "ClickUp.Web.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ClickUp.Web.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ClickUp.Web.API.dll"]