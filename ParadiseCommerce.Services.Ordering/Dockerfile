﻿FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["ParadiseCommerce.Services.Ordering.csproj", "ParadiseCommerce.Services.Ordering.csproj"]

RUN dotnet restore "ParadiseCommerce.Services.Ordering.csproj"
COPY . .
RUN dotnet build "ParadiseCommerce.Services.Ordering.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ParadiseCommerce.Services.Ordering.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ParadiseCommerce.Services.Ordering.dll"]
