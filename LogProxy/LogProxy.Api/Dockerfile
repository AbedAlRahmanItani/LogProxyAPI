#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["LogProxy.Api/LogProxy.Api.csproj", "LogProxy.Api/"]
COPY ["LogProxy.Infrastructure/LogProxy.Infrastructure.csproj", "LogProxy.Infrastructure/"]
COPY ["LogProxy.Application/LogProxy.Application.csproj", "LogProxy.Application/"]
COPY ["LogProxy.Domain/LogProxy.Domain.csproj", "LogProxy.Domain/"]
COPY ["LogProxy.Persistence/LogProxy.Persistence.csproj", "LogProxy.Persistence/"]
RUN dotnet restore "LogProxy.Api/LogProxy.Api.csproj"
COPY . .
WORKDIR "/src/LogProxy.Api"
RUN dotnet build "LogProxy.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LogProxy.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_ENVIRONMENT=Docker
RUN mv appsettings.Docker.json appsettings.json
ENTRYPOINT ["dotnet", "LogProxy.Api.dll", "--environment=Docker"]