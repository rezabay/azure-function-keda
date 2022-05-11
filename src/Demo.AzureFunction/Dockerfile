FROM mcr.microsoft.com/dotnet/sdk:6.0 AS installer-env

# Build requires 3.1 SDK
COPY --from=mcr.microsoft.com/dotnet/core/sdk:3.1 /usr/share/dotnet /usr/share/dotnet

COPY ./src/Build/ ./src/dotnet-function-app/Build/
COPY ./src/Demo.Common/ ./src/dotnet-function-app/Demo.Common/
COPY ./src/Demo.AzureFunction/ ./src/dotnet-function-app/Demo.AzureFunction/

RUN cd /src/dotnet-function-app && \
    mkdir -p /home/site/wwwroot && \
    ls && \
    dotnet publish Demo.AzureFunction/Demo.AzureFunction.csproj -c Release --output /home/site/wwwroot

# To enable ssh & remote debugging on app service change the base image to the one below
# FROM mcr.microsoft.com/azure-functions/dotnet:4-appservice
FROM mcr.microsoft.com/azure-functions/dotnet:4
ENV AzureWebJobsScriptRoot=/home/site/wwwroot \
    AzureFunctionsJobHost__Logging__Console__IsEnabled=true

ENV ASPNETCORE_URLS=http://*:5000/

COPY --from=installer-env ["/home/site/wwwroot", "/home/site/wwwroot"]