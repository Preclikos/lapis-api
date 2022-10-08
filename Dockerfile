FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY /LapisApi/*.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY ./LapisApi ./
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app

ENV DOTNET_HOSTBUILDER__RELOADCONFIGONCHANGE=false
ENV TZ Europe/Prague
EXPOSE 5000

COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "WebApi.dll"]
