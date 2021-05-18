FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["Services/SearchService/Rest/Kwetter.Services.SearchService.Rest.csproj", "Rest/"]
COPY ["Services/SearchService/Domain/Kwetter.Services.SearchService.Domain.csproj", "Domain/"]
COPY ["Services/SearchService/Persistence/Kwetter.Services.SearchService.Persistence.csproj", "Persistence/"]
COPY ["Services/SearchService/Application/Kwetter.Services.SearchService.Application.csproj", "Application/"]
COPY ["Services/SearchService/Infrastructure/Kwetter.Services.SearchService.Infrastructure.csproj", "Infrastructure/"]

RUN dotnet restore "Rest/Kwetter.Services.ProfileService.Rest.csproj"
COPY . .
WORKDIR "/src/Services/SearchService/Rest"
RUN dotnet build "Kwetter.Services.SearchService.Rest.csproj" -c Release -o /app/build
FROM build AS publish
RUN dotnet publish "Kwetter.Services.SearchService.Rest.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Kwetter.Services.SearchService.Rest.dll"]