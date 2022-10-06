﻿FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["shpTest.csproj", "./"]
RUN dotnet restore "shpTest.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "shpTest.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "shpTest.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "shpTest.dll"]
