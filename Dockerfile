FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env (указал ссылку)
WORKDIR /app

COPY *.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0 (указал ссылку)
WORKDIR /app
COPY --from=build-env /app/out .

ENTRYPOINT ["dotnet", "SimpleCryptoBot.dll"]
