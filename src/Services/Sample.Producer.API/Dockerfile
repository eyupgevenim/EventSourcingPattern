#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Services/Sample.Producer.API/Sample.Producer.API.csproj", "src/Services/Sample.Producer.API/"]
COPY ["src/EventBus/EventBus/EventBus.csproj", "src/EventBus/EventBus/"]
COPY ["src/EventBus/EventBus.RabbitMQ/EventBus.RabbitMQ.csproj", "src/EventBus/EventBus.RabbitMQ/"]
RUN dotnet restore "src/Services/Sample.Producer.API/Sample.Producer.API.csproj"
COPY . .
WORKDIR "/src/src/Services/Sample.Producer.API"
RUN dotnet build "Sample.Producer.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Sample.Producer.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Sample.Producer.API.dll"]