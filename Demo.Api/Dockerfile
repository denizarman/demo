#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["Demo.Api/Demo.Api.csproj", "Demo.Api/"]
COPY ["Demo.Service/Demo.Service.csproj", "Demo.Service/"]
COPY ["Demo.Data/Demo.Data.csproj", "Demo.Data/"]
COPY ["Demo.Core/Demo.Core.csproj", "Demo.Core/"]
RUN dotnet restore "Demo.Api/Demo.Api.csproj"
COPY . .
WORKDIR "/src/Demo.Api"
RUN dotnet build "Demo.Api.csproj" -c Release -o /app/build

FROM ubuntu:21.04
RUN touch deneme.txt
RUN curl xxxxxx