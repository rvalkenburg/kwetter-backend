FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["Services/AuthorizationService/Rest/Kwetter.Services.AuthorizationService.Rest.csproj", "Rest/"]
COPY ["Services/AuthorizationService/Domain/Kwetter.Services.AuthorizationService.Domain.csproj", "Domain/"]
COPY ["Services/AuthorizationService/Persistence/Kwetter.Services.AuthorizationService.Persistence.csproj", "Persistence/"]
COPY ["Services/AuthorizationService/Application/Kwetter.Services.AuthorizationService.Application.csproj", "Application/"]
COPY ["Services/AuthorizationService/Infrastructure/Kwetter.Services.AuthorizationService.Infrastructure.csproj", "Infrastructure/"]

RUN dotnet restore "Rest/Kwetter.Services.AuthorizationService.Rest.csproj"
COPY . .
WORKDIR "/src/Services/AuthorizationService/Rest"
RUN dotnet build "Kwetter.Services.AuthorizationService.Rest.csproj" -c Release -o /app/build
FROM build AS publish
RUN dotnet publish "Kwetter.Services.AuthorizationService.Rest.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Kwetter.Services.AuthorizationService.Rest.dll"]
