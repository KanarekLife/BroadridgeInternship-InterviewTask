FROM node:18 AS build
WORKDIR /app
COPY . ./
RUN yarn
RUN yarn build

FROM nginx:alpine
COPY --from=build /app/public /usr/share/nginx/html