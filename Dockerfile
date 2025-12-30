FROM mcr.microsoft.com/playwright/dotnet:v1.57.0-noble
WORKDIR /e2e
COPY . .
RUN dotnet build
