FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["Kwetter.Services/Kwetter.Services.ProfileService/Kwetter.Services.ProfileService.Rest/Kwetter.Services.ProfileService.Rest.csproj", "Kwetter.Services.ProfileService.Rest/"]
COPY ["Kwetter.Services/Kwetter.Services.ProfileService/Kwetter.Services.ProfileService.Domain/Kwetter.Services.ProfileService.Domain.csproj", "Kwetter.Services.ProfileService.Domain/"]
COPY ["Kwetter.Services/Kwetter.Services.ProfileService/Kwetter.Services.ProfileService.Persistence/Kwetter.Services.ProfileService.Persistence.csproj", "Kwetter.Services.ProfileService.Persistence/"]
COPY ["Kwetter.Services/Kwetter.Services.ProfileService/Kwetter.Services.ProfileService.Application/Kwetter.Services.ProfileService.Application.csproj", "Kwetter.Services.ProfileService.Application/"]

RUN dotnet restore "Kwetter.Services.ProfileService.Rest/Kwetter.Services.ProfileService.Rest.csproj"
COPY . .
WORKDIR "/src/Kwetter.Services/Kwetter.Services.ProfileService/Kwetter.Services.ProfileService.Rest"
RUN dotnet build "Kwetter.Services.ProfileService.Rest.csproj" -c Release -o /app/build
FROM build AS publish
RUN dotnet publish "Kwetter.Services.ProfileService.Rest.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Kwetter.Services.ProfileService.Rest.dll"]