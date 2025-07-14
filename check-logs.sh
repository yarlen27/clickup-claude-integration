#!/bin/bash

# Check logging and monitoring status
echo "📊 Checking logging and monitoring status..."

# Check if logs directory exists
if [ -d "logs" ]; then
    echo "✅ Logs directory exists"
    
    # Check for recent log files
    LOG_COUNT=$(find logs -name "*.log" -mtime -1 2>/dev/null | wc -l)
    if [ "$LOG_COUNT" -gt 0 ]; then
        echo "✅ Found $LOG_COUNT recent log files"
        echo "📋 Recent log files:"
        find logs -name "*.log" -mtime -1 -exec ls -lh {} \;
    else
        echo "⚠️ No recent log files found"
    fi
else
    echo "❌ Logs directory not found"
    echo "💡 Creating logs directory..."
    mkdir -p logs
fi

# Check container logs if running
echo ""
echo "🐳 Checking container logs..."

if docker-compose ps | grep -q "Up"; then
    echo "📊 Container status:"
    docker-compose ps
    
    echo ""
    echo "📝 Recent API logs (last 10 lines):"
    docker-compose logs --tail=10 clickup-api
    
    echo ""
    echo "📝 Recent MCP Server logs (last 10 lines):"
    docker-compose logs --tail=10 mcp-server
else
    echo "⚠️ Containers not running"
    echo "💡 Start with: docker-compose up -d"
fi

# Check monitoring services if available
echo ""
echo "📈 Checking monitoring services..."

if curl -s http://localhost:9090 > /dev/null 2>&1; then
    echo "✅ Prometheus available at http://localhost:9090"
else
    echo "❌ Prometheus not accessible"
fi

if curl -s http://localhost:3001 > /dev/null 2>&1; then
    echo "✅ Grafana available at http://localhost:3001"
else
    echo "❌ Grafana not accessible"
fi

echo "🏁 Logging and monitoring check completed"