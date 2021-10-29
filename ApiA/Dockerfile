FROM mcr.microsoft.com/dotnet/aspnet:3.1-focal AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:3.1-focal AS build

WORKDIR /src
COPY ["ApiA.csproj", "./"]
COPY ["nuget.config", "./"]
RUN dotnet restore "ApiA.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "ApiA.csproj" -c Release -o /app/build
ADD ./directory ./app/directory

VOLUME ./directory /app/directory

FROM build AS publish

RUN dotnet publish "ApiA.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV FS_URL_API_B="http://172.24.208.1:44345"
ENTRYPOINT ["dotnet", "ApiA.dll"]