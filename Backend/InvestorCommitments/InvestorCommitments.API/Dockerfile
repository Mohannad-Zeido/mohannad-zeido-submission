FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy individual project files
COPY ["Backend/InvestorCommitments/InvestorCommitments.API/InvestorCommitments.API.csproj", "InvestorCommitments.API/"]
COPY ["Backend/InvestorCommitments/InvestorCommitments.API/InvestorCommitments.API.csproj", "InvestorCommitments.API/"]
COPY ["Backend/InvestorCommitments/InvestorCommitments.Infrastructure/InvestorCommitments.Infrastructure.csproj", "InvestorCommitments.Infrastructure/"]

# Copy entire source tree
COPY Backend/InvestorCommitments .

# Build
WORKDIR /src/InvestorCommitments.API
RUN dotnet restore
RUN dotnet build "InvestorCommitments.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
RUN dotnet publish "InvestorCommitments.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app

COPY ["Backend/InvestorCommitments/Database/investor_commitments.db", "Database/investor_commitments.db"]
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "InvestorCommitments.API.dll"]
