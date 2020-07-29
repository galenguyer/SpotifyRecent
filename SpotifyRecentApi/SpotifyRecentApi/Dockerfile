#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 28473

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["SpotifyRecentApi/SpotifyRecentApi.csproj", "SpotifyRecentApi/"]
RUN dotnet restore "SpotifyRecentApi/SpotifyRecentApi.csproj"
COPY . .
WORKDIR "/src/SpotifyRecentApi"
RUN dotnet build "SpotifyRecentApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SpotifyRecentApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
RUN curl -L https://yt-dl.org/downloads/latest/youtube-dl -o /usr/local/bin/youtube-dl
RUN chmod a+rx /usr/local/bin/youtube-dl
ENTRYPOINT ["dotnet", "SpotifyRecentApi.dll"]