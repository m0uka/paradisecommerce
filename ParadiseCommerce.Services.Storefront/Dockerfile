FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["ParadiseCommerce.Services.Storefront/ParadiseCommerce.Services.Storefront.csproj", "ParadiseCommerce.Services.Storefront/"]
RUN dotnet restore "ParadiseCommerce.Services.Storefront/ParadiseCommerce.Services.Storefront.csproj"
COPY . .
WORKDIR "/src/ParadiseCommerce.Services.Storefront"
RUN dotnet build "ParadiseCommerce.Services.Storefront.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ParadiseCommerce.Services.Storefront.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ParadiseCommerce.Services.Storefront.dll"]
