FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["AnimalTargetUsers/AnimalTargetUsers.csproj", "AnimalTargetUsers/"]
RUN dotnet restore "AnimalTargetUsers/AnimalTargetUsers.csproj"
COPY . .
WORKDIR "/src/AnimalTargetUsers"
RUN dotnet build "AnimalTargetUsers.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AnimalTargetUsers.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AnimalTargetUsers.dll"]
