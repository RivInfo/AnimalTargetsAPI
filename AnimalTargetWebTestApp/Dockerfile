FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["AnimalTargetWebTestApp/AnimalTargetWebTestApp.csproj", "AnimalTargetWebTestApp/"]
RUN dotnet restore "AnimalTargetWebTestApp/AnimalTargetWebTestApp.csproj"
COPY . .
WORKDIR "/src/AnimalTargetWebTestApp"
RUN dotnet build "AnimalTargetWebTestApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AnimalTargetWebTestApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AnimalTargetWebTestApp.dll"]
