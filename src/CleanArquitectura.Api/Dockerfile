FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5021

ENV ASPNETCORE_URLS=http://+:5021

USER app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src

COPY ["src/CleanArquitectura.Domain/CleanArquitectura.Domain.csproj", "src/CleanArquitectura.Domain/"]
COPY ["src/CleanArquitectura.Application/CleanArquitectura.Application.csproj", "src/CleanArquitectura.Application/"]
COPY ["src/CleanArquitectura.Infrastructure/CleanArquitectura.Infrastructure.csproj", "src/CleanArquitectura.Infrastructure/"]

COPY ["src/CleanArquitectura.Api/CleanArquitectura.Api.csproj", "src/CleanArquitectura.Api/"]

RUN dotnet restore "src/CleanArquitectura.Api/CleanArquitectura.Api.csproj"
COPY . .
WORKDIR "/src/src/CleanArquitectura.Api"
RUN dotnet build "CleanArquitectura.Api.csproj" -c Debug -o /app/build

FROM build AS publish
RUN dotnet publish "CleanArquitectura.Api.csproj" -c Debug -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CleanArquitectura.Api.dll"]
