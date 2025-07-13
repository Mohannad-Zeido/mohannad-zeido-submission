FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
COPY ["Database/investor_commitments.db", "./Databases123/investor_commitments.db"]
WORKDIR /src
COPY Database/investor_commitments.db ./Databases/investor_commitments.db
COPY ["Database/investor_commitments.db", "./Databases123/investor_commitments.db"]
COPY ["InvestorCommitmentsAPI/InvestorCommitments.API.csproj", "InvestorCommitments.API/"]
RUN dotnet restore "InvestorCommitmentsAPI/InvestorCommitments.API.csproj"
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
