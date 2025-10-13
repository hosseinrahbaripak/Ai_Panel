FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-Ai_Panel

WORKDIR /app

COPY ./Ai_Panel/Ai_Panel.csproj ./Ai_Panel/Ai_Panel.csproj
COPY ./Ai_Panel.Application/Ai_Panel.Application.csproj ./Ai_Panel.Application/Ai_Panel.Application.csproj
COPY ./Ai_Panel.Domain/Ai_Panel.Domain.csproj ./Ai_Panel.Domain/Ai_Panel.Domain.csproj
COPY ./Ai_Panel.Infrastructure/Ai_Panel.Infrastructure.csproj ./Ai_Panel.Infrastructure/Ai_Panel.Infrastructure.csproj
COPY ./DataLayer/Ai_Panel.Persistence.csproj ./DataLayer/Ai_Panel.Persistence.csproj

WORKDIR /app/Ai_Panel

RUN dotnet restore

WORKDIR /app

COPY ./Ai_Panel ./Ai_Panel
COPY ./Ai_Panel.Application ./Ai_Panel.Application
COPY ./Ai_Panel.Domain ./Ai_Panel.Domain
COPY ./Ai_Panel.Infrastructure ./Ai_Panel.Infrastructure
COPY ./DataLayer ./DataLayer

WORKDIR /app/Ai_Panel

RUN dotnet build

RUN dotnet publish -c Release -o /app/Ai_Panel/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime-Ai_Panel
WORKDIR /app
COPY --from=build-Ai_Panel /app/Ai_Panel/publish .

# Set the entry point
ENTRYPOINT ["dotnet", "Ai_Panel.dll"] 