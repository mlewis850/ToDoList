# # Stage 1: Build the Angular application
# FROM node:18-alpine AS build
# WORKDIR /app
# COPY package*.json ./
# RUN npm install
# COPY . . 
# RUN npm run build

# # Stage 2: Serve the application with Nginx
# FROM nginx:alpine
# COPY --from=build /app/dist/ToDoList /usr/share/nginx/html
# EXPOSE 80
# CMD ["nginx", "-g", "daemon off;"]



FROM node:18-alpine 
WORKDIR /app
COPY package*.json ./
RUN npm install
COPY . . 
RUN npm install  @angular/cli
RUN npm run build
EXPOSE 4200
CMD ["ngserve"]
