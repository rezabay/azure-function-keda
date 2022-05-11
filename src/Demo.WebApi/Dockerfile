FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build

ARG BUILD_NUMBER
RUN echo "Build Version: $BUILD_NUMBER"

# Copy files and restore solution
WORKDIR /code
COPY ./src/Build/ ./src/Build/
COPY ./src/Demo.Common/ ./src/Demo.Common/
COPY ./src/Demo.WebApi/ ./src/Demo.WebApi/

# Publish
FROM build AS publish
WORKDIR /code/src/Demo.WebApi

ARG BUILD_NUMBER
RUN echo "Publishing Version: $BUILD_NUMBER"

RUN dotnet publish Demo.WebApi.csproj /p:VersionNumber=$BUILD_NUMBER -c Release -o /app

# Final app container
FROM mcr.microsoft.com/dotnet/aspnet:6.0.0 AS runtime
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Full PGO
ENV DOTNET_TieredPGO 1 
ENV DOTNET_TC_QuickJitForLoops 1 
ENV DOTNET_ReadyToRun 0

WORKDIR /app

COPY --from=publish /app ./

ENTRYPOINT ["dotnet", "Demo.WebApi.dll"]
