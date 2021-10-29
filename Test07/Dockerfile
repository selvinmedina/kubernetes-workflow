FROM mcr.microsoft.com/dotnet/aspnet:5.0-focal AS base

WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0-focal AS build

WORKDIR /src
COPY ["nuget.config", ""]
COPY ["Test07.csproj", "./"]
RUN dotnet restore "Test07.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "Test07.csproj" -c Release -o /app/build
FROM build AS publish
RUN dotnet publish "Test07.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV FS_URL_API_TEST06="http://172.24.208.1:4432"
ENTRYPOINT ["dotnet", "Test07.dll"]
