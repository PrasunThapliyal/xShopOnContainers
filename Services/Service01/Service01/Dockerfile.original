FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS runtime
WORKDIR /app
COPY out/. .
ENTRYPOINT ["dotnet", "Service01.dll"]
