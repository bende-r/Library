# Backend Dockerfile (в корневом каталоге проекта)

# Используйте .NET SDK для стадии сборки
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Скопируйте файлы проектов (обратите внимание на пути)
COPY ["API/API.csproj", "API/"]
COPY ["Application/Application.csproj", "Application/"]
COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]
COPY ["Domain/Domain.csproj", "Domain/"]

# Восстановите зависимости
RUN dotnet restore "API/API.csproj"

# Скопируйте весь код и выполните сборку
COPY . .
WORKDIR /src/API
RUN dotnet publish "API.csproj" -c Release -o /app/publish

# Используйте ASP.NET Core Runtime для запуска
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /src
COPY --from=build /src/publish .
ENTRYPOINT ["dotnet", "API.dll"]
