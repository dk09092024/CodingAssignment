﻿FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["TransactionApi/TransactionApi.csproj", "TransactionApi/"]
RUN dotnet restore "TransactionApi/TransactionApi.csproj"
COPY . .
WORKDIR "/src/TransactionApi"
RUN dotnet build "TransactionApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "TransactionApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TransactionApi.dll"]

EXPOSE 80
EXPOSE 8080
EXPOSE 8081