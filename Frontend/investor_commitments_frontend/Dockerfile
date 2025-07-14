# Stage 1: Build the application
FROM node:18-alpine AS builder

WORKDIR /app

COPY Frontend/investor_commitments_frontend/package.json Frontend/investor_commitments_frontend/package-lock.json ./
RUN npm install

COPY Frontend/investor_commitments_frontend/. .
RUN npm run build

# Stage 2: Serve with nginx
FROM nginx:alpine

# Remove default nginx config
RUN rm /etc/nginx/conf.d/default.conf

# Copy custom nginx config
COPY Frontend/investor_commitments_frontend/nginx.conf /etc/nginx/conf.d

# Copy build files from previous stage
COPY --from=builder /app/dist /usr/share/nginx/html

EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]
