#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
# COPY ["Service01/Service01.csproj", "Service01/"]
# RUN dotnet restore "Service01/Service01.csproj"
COPY . .
RUN dotnet restore
#WORKDIR "/src/Service01"
RUN dotnet build "Service01.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Service01.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Service01.dll"]
