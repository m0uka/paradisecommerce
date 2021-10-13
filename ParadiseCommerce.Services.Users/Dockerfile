FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["ParadiseCommerce.Services.Users/ParadiseCommerce.Services.Users.csproj", "ParadiseCommerce.Services.Users/"]
RUN dotnet restore "ParadiseCommerce.Services.Users/ParadiseCommerce.Services.Users.csproj"
COPY . .
WORKDIR "/src/ParadiseCommerce.Services.Users"
RUN dotnet build "ParadiseCommerce.Services.Users.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ParadiseCommerce.Services.Users.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ParadiseCommerce.Services.Users.dll"]
