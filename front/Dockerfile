ARG APP_VER
FROM node:lts-buster-slim as build-deps
ENV GENERATE_SOURCEMAP=false
ENV REACT_APP_USERNAME=
ENV REACT_APP_PASSWORD=
ENV REACT_APP_NAME="K-CRM-APP-PROD"
ENV REACT_APP_API_URL=""
ENV REACT_APP_GIT_SHA=$APP_VER
# ignore files in dockerignore
COPY package.json .
COPY package-lock.json .
RUN NODE_ENV=development npm install

COPY . .
RUN NODE_ENV=production npm run build

FROM nginx:1.19.0-alpine
RUN apk add --no-cache curl
COPY --from=build-deps /build /usr/share/nginx/html
COPY nginx/nginx.conf /etc/nginx/conf.d/default.conf
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]
