FROM mcr.microsoft.com/dotnet/aspnet:3.1-focal AS base
WORKDIR /app
EXPOSE 9090
EXPOSE 80
EXPOSE 9987

FROM mcr.microsoft.com/dotnet/sdk:3.1-focal AS build
WORKDIR /src
COPY ["Test06.csproj", "./"]
RUN dotnet restore "Test06.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "Test06.csproj" -c Release -o /app/build
ADD ./directory ./app/directory
ENV TZ=America/Tegucigalpa

VOLUME ./directory /app/directory

FROM build AS publish
RUN dotnet publish "Test06.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Test06.dll"]
