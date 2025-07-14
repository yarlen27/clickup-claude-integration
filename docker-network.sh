#!/bin/bash

# Create Docker network for ClickUp integration services
echo "🌐 Creating Docker network for ClickUp integration..."

# Create network if it doesn't exist
if ! docker network ls | grep -q "clickup-network"; then
    docker network create clickup-network
    echo "✅ Created clickup-network"
else
    echo "ℹ️ clickup-network already exists"
fi

# Display network info
docker network inspect clickup-network --format "{{.Name}}: {{.Driver}} ({{.Scope}})"
echo "🚀 Network ready for container communication"