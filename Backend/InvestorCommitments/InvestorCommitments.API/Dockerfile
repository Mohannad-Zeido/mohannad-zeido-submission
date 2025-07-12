FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["InvestorCommitments.API/InvestorCommitments.API.csproj", "InvestorCommitments.API/"]
RUN dotnet restore "InvestorCommitments.API/InvestorCommitments.API.csproj"
COPY . .
WORKDIR "/src/InvestorCommitments.API"
RUN dotnet build "./InvestorCommitments.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./InvestorCommitments.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "InvestorCommitments.API.dll"]
