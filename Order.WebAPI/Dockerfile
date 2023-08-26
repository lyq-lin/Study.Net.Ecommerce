#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Order.WebAPI/Order.WebAPI.csproj", "Order.WebAPI/"]
COPY ["Common.Alipay/Common.Alipay.csproj", "Common.Alipay/"]
COPY ["Common.Jwt/Common.Jwt.csproj", "Common.Jwt/"]
COPY ["Common.RabbitMQ/Common.RabbitMQ.csproj", "Common.RabbitMQ/"]
COPY ["Order.Domain/Order.Domain.csproj", "Order.Domain/"]
COPY ["Order.Infrastructure/Order.Infrastructure.csproj", "Order.Infrastructure/"]
RUN dotnet restore "Order.WebAPI/Order.WebAPI.csproj"
COPY . .
WORKDIR "/src/Order.WebAPI"
RUN dotnet build "Order.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Order.WebAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Order.WebAPI.dll"]