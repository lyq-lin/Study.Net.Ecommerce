#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Cart.WebAPI/Cart.WebAPI.csproj", "Cart.WebAPI/"]
COPY ["Cart.Domain/Cart.Domain.csproj", "Cart.Domain/"]
COPY ["Cart.Infrastructure/Cart.Infrastructure.csproj", "Cart.Infrastructure/"]
COPY ["Common.Jwt/Common.Jwt.csproj", "Common.Jwt/"]
COPY ["Common.RabbitMQ/Common.RabbitMQ.csproj", "Common.RabbitMQ/"]
RUN dotnet restore "Cart.WebAPI/Cart.WebAPI.csproj"
COPY . .
WORKDIR "/src/Cart.WebAPI"
RUN dotnet build "Cart.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Cart.WebAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Cart.WebAPI.dll"]