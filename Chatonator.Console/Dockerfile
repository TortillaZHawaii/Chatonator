FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Chatonator.Console/Chatonator.Console.csproj", "Chatonator.Console/"]
RUN dotnet restore "Chatonator.Console/Chatonator.Console.csproj"
COPY . .
WORKDIR "/src/Chatonator.Console"
RUN dotnet build "Chatonator.Console.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Chatonator.Console.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Chatonator.Console.dll"]
