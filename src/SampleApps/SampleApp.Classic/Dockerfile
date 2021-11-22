FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine as build

# Build

WORKDIR /usr/src/app

COPY Simplify.Web/ Simplify.Web/
COPY SampleApps/SampleApp.Classic/ SampleApps/SampleApp.Classic/

WORKDIR /usr/src/app/SampleApps/SampleApp.Classic
RUN dotnet restore SampleApp.Classic.csproj

RUN dotnet publish SampleApp.Classic.csproj -c release -o out

# Run

FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine

WORKDIR /usr/src/app

COPY --from=build /usr/src/app/SampleApps/SampleApp.Classic/out out

ENV ASPNETCORE_URLS http://*:5000

CMD dotnet /usr/src/app/out/SampleApp.Classic.dll
