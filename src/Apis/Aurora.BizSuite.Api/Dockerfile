# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Directory.Build.props", "."]
COPY ["Apis/Aurora.BizSuite.Api/Aurora.BizSuite.Api.csproj", "Apis/Aurora.BizSuite.Api/"]
COPY ["Framework/Aurora.Framework.Infrastructure/Aurora.Framework.Infrastructure.csproj", "Framework/Aurora.Framework.Infrastructure/"]
COPY ["Framework/Aurora.Framework.Application/Aurora.Framework.Application.csproj", "Framework/Aurora.Framework.Application/"]
COPY ["Framework/Aurora.Framework.Core/Aurora.Framework.Core.csproj", "Framework/Aurora.Framework.Core/"]
COPY ["Framework/Aurora.Framework.Presentation/Aurora.Framework.Presentation.csproj", "Framework/Aurora.Framework.Presentation/"]
COPY ["Framework/Aurora.Framework.Identity/Aurora.Framework.Identity.csproj", "Framework/Aurora.Framework.Identity/"]
COPY ["Integrations/Aurora.BizSuite.IntegrationEvents/Aurora.BizSuite.IntegrationEvents.csproj", "Integrations/Aurora.BizSuite.IntegrationEvents/"]
COPY ["Modules/Companies/Aurora.BizSuite.Companies.Infrastructure/Aurora.BizSuite.Companies.Infrastructure.csproj", "Modules/Companies/Aurora.BizSuite.Companies.Infrastructure/"]
COPY ["Modules/Companies/Aurora.BizSuite.Companies.Application/Aurora.BizSuite.Companies.Application.csproj", "Modules/Companies/Aurora.BizSuite.Companies.Application/"]
COPY ["Modules/Companies/Aurora.BizSuite.Companies.Domain/Aurora.BizSuite.Companies.Domain.csproj", "Modules/Companies/Aurora.BizSuite.Companies.Domain/"]
COPY ["Modules/Companies/Aurora.BizSuite.Companies.Presentation/Aurora.BizSuite.Companies.Presentation.csproj", "Modules/Companies/Aurora.BizSuite.Companies.Presentation/"]
COPY ["Modules/Inventory/Aurora.BizSuite.Inventory.Infrastructure/Aurora.BizSuite.Inventory.Infrastructure.csproj", "Modules/Inventory/Aurora.BizSuite.Inventory.Infrastructure/"]
COPY ["Modules/Inventory/Aurora.BizSuite.Inventory.Application/Aurora.BizSuite.Inventory.Application.csproj", "Modules/Inventory/Aurora.BizSuite.Inventory.Application/"]
COPY ["Modules/Inventory/Aurora.BizSuite.Inventory.Domain/Aurora.BizSuite.Inventory.Domain.csproj", "Modules/Inventory/Aurora.BizSuite.Inventory.Domain/"]
COPY ["Modules/Inventory/Aurora.BizSuite.Inventory.Presentation/Aurora.BizSuite.Inventory.Presentation.csproj", "Modules/Inventory/Aurora.BizSuite.Inventory.Presentation/"]
COPY ["Modules/Items/Aurora.BizSuite.Items.Infrastructure/Aurora.BizSuite.Items.Infrastructure.csproj", "Modules/Items/Aurora.BizSuite.Items.Infrastructure/"]
COPY ["Modules/Items/Aurora.BizSuite.Items.Application/Aurora.BizSuite.Items.Application.csproj", "Modules/Items/Aurora.BizSuite.Items.Application/"]
COPY ["Modules/Items/Aurora.BizSuite.Items.Domain/Aurora.BizSuite.Items.Domain.csproj", "Modules/Items/Aurora.BizSuite.Items.Domain/"]
COPY ["Modules/Items/Aurora.BizSuite.Items.Presentation/Aurora.BizSuite.Items.Presentation.csproj", "Modules/Items/Aurora.BizSuite.Items.Presentation/"]
RUN dotnet restore "./Apis/Aurora.BizSuite.Api/Aurora.BizSuite.Api.csproj"
COPY . .
WORKDIR "/src/Apis/Aurora.BizSuite.Api"
RUN dotnet build "./Aurora.BizSuite.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Aurora.BizSuite.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Aurora.BizSuite.Api.dll"]