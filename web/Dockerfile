# Используем Node.js для сборки React-приложения
FROM node:18 AS build
WORKDIR /app

# Копируем package.json и package-lock.json
COPY package*.json ./
RUN npm install

# Копируем все остальные файлы и строим приложение
COPY . .
RUN npm run build

CMD ["npm", "start"]
