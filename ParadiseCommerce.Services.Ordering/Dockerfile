FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["ParadiseCommerce.Services.Products/ParadiseCommerce.Services.Products.csproj", "ParadiseCommerce.Services.Products/"]
RUN dotnet restore "ParadiseCommerce.Services.Products/ParadiseCommerce.Services.Products.csproj"
COPY . .
WORKDIR "/src/ParadiseCommerce.Services.Products"
RUN dotnet build "ParadiseCommerce.Services.Products.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ParadiseCommerce.Services.Products.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ParadiseCommerce.Services.Products.dll"]
