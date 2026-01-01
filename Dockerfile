FROM mcr.microsoft.com/playwright/dotnet:v1.57.0-noble

# Install .NET 10 SDK
RUN curl -sSL https://dot.net/v1/dotnet-install.sh | bash /dev/stdin --channel 10.0 --install-dir /usr/share/dotnet

WORKDIR /e2e
COPY . .
# RUN dotnet build