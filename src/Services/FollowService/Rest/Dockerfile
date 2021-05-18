FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["Services/FollowService/Rest/Kwetter.Services.FollowService.Rest.csproj", "Rest/"]
COPY ["Services/FollowService/Domain/Kwetter.Services.FollowService.Domain.csproj", "Domain/"]
COPY ["Services/FollowService/Persistence/Kwetter.Services.FollowService.Persistence.csproj", "Persistence/"]
COPY ["Services/FollowService/Application/Kwetter.Services.FollowService.Application.csproj", "Application/"]
COPY ["Services/FollowService/Infrastructure/Kwetter.Services.FollowService.Infrastructure.csproj", "Infrastructure/"]

RUN dotnet restore "Rest/Kwetter.Services.FollowService.Rest.csproj"
COPY . .
WORKDIR "/src/Services/FollowService/Rest"
RUN dotnet build "Kwetter.Services.FollowService.Rest.csproj" -c Release -o /app/build
FROM build AS publish
RUN dotnet publish "Kwetter.Services.FollowService.Rest.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Kwetter.Services.FollowService.Rest.dll"]