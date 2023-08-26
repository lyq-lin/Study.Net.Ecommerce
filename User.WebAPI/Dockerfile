#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["User.WebAPI/User.WebAPI.csproj", "User.WebAPI/"]
COPY ["Common.Jwt/Common.Jwt.csproj", "Common.Jwt/"]
COPY ["Common.RabbitMQ/Common.RabbitMQ.csproj", "Common.RabbitMQ/"]
COPY ["User.Domain/User.Domain.csproj", "User.Domain/"]
COPY ["User.Infrastructure/User.Infrastructure.csproj", "User.Infrastructure/"]
RUN dotnet restore "User.WebAPI/User.WebAPI.csproj"
COPY . .
WORKDIR "/src/User.WebAPI"
RUN dotnet build "User.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "User.WebAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "User.WebAPI.dll"]