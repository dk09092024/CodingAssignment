﻿FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["BankingAccountApi/BankingAccountApi.csproj", "BankingAccountApi/"]
RUN dotnet restore "BankingAccountApi/BankingAccountApi.csproj"
COPY . .
WORKDIR "/src/BankingAccountApi"
RUN dotnet build "BankingAccountApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "BankingAccountApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BankingAccountApi.dll"]

EXPOSE 5040
EXPOSE 8080
EXPOSE 8081