#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Services/Sample.Consumer.API/Sample.Consumer.API.csproj", "src/Services/Sample.Consumer.API/"]
COPY ["src/EventBus/EventBus/EventBus.csproj", "src/EventBus/EventBus/"]
COPY ["src/EventBus/EventBus.RabbitMQ/EventBus.RabbitMQ.csproj", "src/EventBus/EventBus.RabbitMQ/"]
RUN dotnet restore "src/Services/Sample.Consumer.API/Sample.Consumer.API.csproj"
COPY . .
WORKDIR "/src/src/Services/Sample.Consumer.API"
RUN dotnet build "Sample.Consumer.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Sample.Consumer.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Sample.Consumer.API.dll"]