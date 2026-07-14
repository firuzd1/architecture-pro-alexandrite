FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["service-a.csproj", "."]
RUN dotnet restore "service-a.csproj"
COPY . .
RUN dotnet build "service-a.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "service-a.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "service-a.dll"]