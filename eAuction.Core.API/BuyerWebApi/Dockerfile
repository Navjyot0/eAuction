#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["BuyerWebApi/BuyerWebApi.csproj", "BuyerWebApi/"]
COPY ["JWTAuthenticationManager/JWTAuthenticationManager.csproj", "JWTAuthenticationManager/"]
RUN dotnet restore "BuyerWebApi/BuyerWebApi.csproj"
COPY . .
WORKDIR "/src/BuyerWebApi"
RUN dotnet build "BuyerWebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BuyerWebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BuyerWebApi.dll"]