#!/bin/bash

# Test Redis setup and connectivity
echo "🔍 Testing Redis setup and connectivity..."

# Check if Redis container is running
echo "📊 Checking Redis container status..."
if docker ps | grep -q "redis"; then
    echo "✅ Redis container is running"
else
    echo "❌ Redis container not found"
    echo "💡 Run: docker run -d --name redis-test -p 6379:6379 redis:7-alpine"
fi

# Test Redis connectivity
echo "🔌 Testing Redis connectivity..."
if command -v redis-cli &> /dev/null; then
    if redis-cli ping; then
        echo "✅ Redis connection successful"
        
        # Test basic operations
        echo "🧪 Testing basic Redis operations..."
        redis-cli set test_key "Hello ClickUp Integration"
        RESULT=$(redis-cli get test_key)
        echo "📝 Stored and retrieved: $RESULT"
        redis-cli del test_key
        echo "🗑️ Cleanup completed"
    else
        echo "❌ Redis connection failed"
    fi
else
    echo "⚠️ redis-cli not installed, skipping connectivity test"
fi

echo "🏁 Redis test completed"