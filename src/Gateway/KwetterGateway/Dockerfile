FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["Gateway/KwetterGateway/Kwetter.Gateway.csproj", "KwetterGateway/"]
RUN dotnet restore "Gateway/KwetterGateway/Kwetter.Gateway.csproj"
COPY . .
WORKDIR "/src/Gateway/KwetterGateway"
RUN dotnet build "Kwetter.Gateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Kwetter.Gateway.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Kwetter.Gateway.dll"]
