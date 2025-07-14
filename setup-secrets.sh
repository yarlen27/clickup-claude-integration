#!/bin/bash

# Setup development secrets and environment
echo "🔐 Setting up development secrets and environment..."

# Initialize user secrets for .NET API
echo "🔧 Initializing .NET user secrets..."
if dotnet user-secrets list --project src/ClickUp.Web.API &>/dev/null; then
    echo "✅ User secrets already initialized"
else
    dotnet user-secrets init --project src/ClickUp.Web.API
    echo "✅ User secrets initialized"
fi

# Prompt for ClickUp API token if not set
if ! dotnet user-secrets list --project src/ClickUp.Web.API | grep -q "ClickUp:ApiToken"; then
    echo "🔑 ClickUp API token not found in user secrets"
    read -p "Enter your ClickUp API token (or press Enter to skip): " token
    if [ ! -z "$token" ]; then
        dotnet user-secrets set "ClickUp:ApiToken" "$token" --project src/ClickUp.Web.API
        echo "✅ ClickUp API token saved to user secrets"
    else
        echo "⚠️ Skipped ClickUp API token setup"
    fi
else
    echo "✅ ClickUp API token already configured"
fi

# Create .env file from template if it doesn't exist
if [ ! -f ".env" ]; then
    echo "📄 Creating .env file from template..."
    cp .env.template .env
    echo "✅ .env file created - please review and update values"
else
    echo "✅ .env file already exists"
fi

# Set executable permissions on scripts
chmod +x validate-env.sh
chmod +x test-redis.sh
chmod +x build-containers.sh
chmod +x docker-network.sh

echo "🎉 Development environment setup completed!"
echo "📋 Next steps:"
echo "   1. Review and update .env file with your configuration"
echo "   2. Run ./validate-env.sh to verify setup"
echo "   3. Start development with docker-compose up -d"