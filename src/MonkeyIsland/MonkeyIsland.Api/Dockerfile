ARG APP_NAME=MonkeyIsland.Api
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

ARG BUILDCONFIG=RELEASE
ARG APP_NAME
ARG PORT=80

COPY . ./build/
WORKDIR /build/

RUN dotnet test ./src/MonkeyIsland/${APP_NAME}/${APP_NAME}.csproj

# Build and publish
RUN dotnet publish ./src/MonkeyIsland/${APP_NAME}/${APP_NAME}.csproj -c $BUILDCONFIG -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
ARG APP_NAME
ENV APP_NAME=${APP_NAME}
ENV ASPNETCORE_URLS http://*:${PORT}

WORKDIR /app
COPY --from=build /build/out .

EXPOSE $PORT
EXPOSE 443
ENTRYPOINT dotnet ${APP_NAME}.dll