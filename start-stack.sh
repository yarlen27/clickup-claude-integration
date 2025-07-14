#!/bin/bash

# Start ClickUp Integration Stack
echo "🚀 Starting ClickUp Integration Stack..."

# Check if .env file exists
if [ ! -f ".env" ]; then
    echo "⚠️ .env file not found"
    echo "💡 Running setup script first..."
    ./setup-secrets.sh
fi

# Create logs directory
mkdir -p logs

# Create Docker network if it doesn't exist
./docker-network.sh

# Start the stack
echo "🐳 Starting Docker containers..."
docker-compose up -d

# Wait for services to be ready
echo "⏳ Waiting for services to start..."
sleep 10

# Check service health
echo "🔍 Checking service health..."

# Check Redis
if docker-compose exec redis redis-cli ping > /dev/null 2>&1; then
    echo "✅ Redis is healthy"
else
    echo "❌ Redis is not responding"
fi

# Check API
if curl -f http://localhost:${API_PORT:-5000}/health > /dev/null 2>&1; then
    echo "✅ ClickUp API is healthy"
else
    echo "❌ ClickUp API is not responding"
fi

echo "📊 Container status:"
docker-compose ps

echo "🎉 Stack startup completed!"
echo "📋 Access points:"
echo "   • API: http://localhost:${API_PORT:-5000}"
echo "   • API Health: http://localhost:${API_PORT:-5000}/health"
echo "   • API Docs: http://localhost:${API_PORT:-5000}/swagger"
echo "   • MCP Server: localhost:${MCP_PORT:-3000}"
echo "   • Redis: localhost:6379"
echo ""
echo "📝 Useful commands:"
echo "   • View logs: docker-compose logs -f"
echo "   • Stop stack: docker-compose down"
echo "   • Restart: docker-compose restart"