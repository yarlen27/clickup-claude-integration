#!/bin/bash

# Build Docker containers for ClickUp integration
echo "🐳 Building Docker containers for ClickUp integration..."

# Build .NET API container
echo "🔨 Building .NET API container..."
docker build -f src/ClickUp.Web.API/Dockerfile -t clickup-api:latest .

# Build MCP Server container  
echo "🔨 Building MCP Server container..."
docker build -f src/mcp-server/Dockerfile -t clickup-mcp-server:latest .

echo "✅ All containers built successfully!"
echo "📋 Available images:"
docker images | grep -E "(clickup-api|clickup-mcp-server)"