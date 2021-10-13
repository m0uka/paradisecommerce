FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["ParadiseCommerce.Services.Provisioning/ParadiseCommerce.Services.Provisioning.csproj", "ParadiseCommerce.Services.Provisioning/"]
RUN dotnet restore "ParadiseCommerce.Services.Provisioning/ParadiseCommerce.Services.Provisioning.csproj"
COPY . .
WORKDIR "/src/ParadiseCommerce.Services.Provisioning"
RUN dotnet build "ParadiseCommerce.Services.Provisioning.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ParadiseCommerce.Services.Provisioning.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ParadiseCommerce.Services.Provisioning.dll"]
