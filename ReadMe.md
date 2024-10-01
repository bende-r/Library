# Приложение Library

Данное Web-приложение разработано для имитации библиотеки. Для работы с базой дынных используется .Net Core, с использованием EF Core.
Для разработки клиентской части приложения использован React.

## Начало работы

Перед запуском приложения у вас в системе должно быть установлено следующее программное обеспечение:

- .Net 8.0, Entity Framework Core
- Microsoft SQL Server 2022
- Node.js

## Запуск

Клонируйте репозиторий:

```
git clone https://github.com/bende-r/Library.git
```

Перейдите в репозиторий проекта

```
cd Library
```

### Настройка подключения к серверу

В файле appsettings.json укажите используемый SQL сервер и имя будущей базы данных. Например:

```
"ConnectionStrings": {
  "DefaultConnection": "Server=.\\SQLEXPRESS; Database=LibraryDatabase; Trusted_Connection=True; TrustServerCertificate=True;"
},
```

### Выполните миграцию для создания базы данных

```
dotnet ef database update --project Infrastructure\Infrastructure.csproj --startup-project API\API.csproj
```

### Для запуска сервера:

1. Перейдите в папку Api

```
cd API
```

2. Запустите сервер

```
dotnet run
```

### Для запуска веб-клиента

1. Перейдите в папку web

```
cd web
```

2. Установите зависимости

```
npm install
```

3. Запустите проект

```
npm start
```

4. В браузере откройте

```
http://localhost:3000
```

## Использование

После выполнения миграции в базе данных будут созданы базовые учётные записи

```
login: admin@library.com
password: Admin123!

user@library.com
User123!
```
