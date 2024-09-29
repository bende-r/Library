# Базовый образ Node.js
FROM node:16 as build

# Директория для приложения
WORKDIR /app

# Копируем package.json и package-lock.json
COPY package*.json ./

# Устанавливаем зависимости
RUN npm install

# Копируем весь исходный код
COPY . .

# Строим React-приложение
RUN npm run build

# Базовый образ для Nginx
FROM nginx:alpine

# Копируем собранное приложение из предыдущего этапа в Nginx
COPY --from=build /app/build /usr/share/nginx/html

# Expose порт
EXPOSE 80

# Запускаем Nginx
CMD ["nginx", "-g", "daemon off;"]
