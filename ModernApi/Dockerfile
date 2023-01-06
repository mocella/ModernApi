ARG CONTAINERVERSION=6.0-alpine

# Stage 1 - Base Stage
FROM mcr.microsoft.com/dotnet/sdk:$CONTAINERVERSION AS base
WORKDIR /app

# Stage 2 - Build Stage
FROM mcr.microsoft.com/dotnet/sdk:$CONTAINERVERSION AS build
WORKDIR /src

COPY ["ModernApi/ModernApi.csproj", "ModernApi/"]
COPY ["Api.Core/Api.Core.csproj", "Api.Core/"]

RUN dotnet restore "ModernApi/ModernApi.csproj"

COPY . .

WORKDIR "/src/ModernApi"
RUN dotnet build "ModernApi.csproj" -c Release -o /app/build

# Stage 3 - Publish Stage
FROM build AS publish
RUN dotnet publish "ModernApi.csproj" -c Release -o /app/publish

# Stage 4 - Final Stage
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Control dotnet core behavior via environment variables
ENV DOTNET_RUNNING_IN_CONTAINER=true
ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["dotnet", "ModernApi.dll"]
