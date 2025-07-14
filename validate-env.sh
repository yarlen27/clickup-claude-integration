#!/bin/bash

# Environment Configuration Validation Script
echo "🔍 Validating environment configuration..."

# Check if .env file exists
if [ ! -f ".env" ]; then
    echo "⚠️ .env file not found"
    echo "💡 Copy .env.template to .env and configure your values"
    echo "   cp .env.template .env"
else
    echo "✅ .env file found"
fi

# Check required environment variables
echo "🔧 Checking required environment variables..."

REQUIRED_VARS=(
    "CLICKUP_API_TOKEN"
    "REDIS_CONNECTION_STRING"
    "API_PORT"
    "MCP_PORT"
)

MISSING_VARS=()

for var in "${REQUIRED_VARS[@]}"; do
    if [ -z "${!var}" ]; then
        MISSING_VARS+=("$var")
        echo "❌ $var is not set"
    else
        echo "✅ $var is configured"
    fi
done

# Check .NET user secrets
echo "🔐 Checking .NET user secrets..."
if dotnet user-secrets list --project src/ClickUp.Web.API 2>/dev/null | grep -q "ClickUp:ApiToken"; then
    echo "✅ ClickUp API token configured in user secrets"
else
    echo "⚠️ ClickUp API token not found in user secrets"
    echo "💡 Run: dotnet user-secrets set \"ClickUp:ApiToken\" \"your_token\" --project src/ClickUp.Web.API"
fi

# Summary
if [ ${#MISSING_VARS[@]} -eq 0 ]; then
    echo "🎉 All required environment variables are configured!"
else
    echo "❌ Missing variables: ${MISSING_VARS[*]}"
    echo "📋 Please configure these variables in your .env file"
    exit 1
fi