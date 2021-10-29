FROM mcr.microsoft.com/dotnet/aspnet:3.1-focal AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:3.1-focal AS build

WORKDIR /src
COPY ["nuget.config", ""]
COPY ["Test06.csproj", "./"]
RUN dotnet restore "Test06.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "Test06.csproj" -c Release -o /app/build
ADD ./directory ./app/directory

VOLUME ./directory /app/directory

FROM build AS publish

RUN dotnet publish "Test06.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV FS_URL_API_TEST07="http://172.24.208.1:44345"
ENTRYPOINT ["dotnet", "Test06.dll"]