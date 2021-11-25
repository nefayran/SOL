#build
FROM node:lts-alpine as build
WORKDIR /app
COPY ./SOL.Web .
RUN npm install
RUN npm run build

#production
FROM nginx:stable-alpine as production
COPY --from=build /app/dist /usr/share/nginx/html
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]
