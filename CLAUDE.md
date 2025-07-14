# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview
This is a ClickUp integration toolkit for Claude Code, enabling automated project management, task creation, delay analysis, and workflow optimization through the ClickUp API via MCP (Model Context Protocol).

## Architecture
- **Primary Stack**: .NET 8.0 with Clean Architecture + DDD + CQRS
- **Integration Layer**: MCP Server (Node.js + TypeScript)
- **Deployment**: Docker containerized stack
- **Pattern**: Result Pattern for error handling
- **Validation**: FluentValidation for input validation
- **Logging**: Serilog with structured logging
- **Testing**: xUnit + FluentAssertions + NSubstitute
- **Cache**: Redis for member/project caching
- **API Integration**: HttpClient with Polly for resilience

## Project Status
✅ **PROOF OF CONCEPT COMPLETED**
- Base .NET API with full ClickUp CRUD operations
- Member management and natural language assignment
- Complete hierarchy: Teams → Spaces → Folders → Lists → Tasks
- Rate limiting and error handling implemented

🏗️ **PLANNED ARCHITECTURE**
```
Claude Code ←→ MCP Server ←→ .NET API ←→ ClickUp API
           JSON-RPC      HTTP REST    HTTP REST
```

## Core Modules Structure
```
src/
├── ClickUp.Core/           # Domain entities and business logic ✅
├── ClickUp.Infrastructure/ # ClickUp API integration ✅
├── ClickUp.Console/        # CLI tool for testing ✅
├── ClickUp.Web.API/        # REST API (planned)
└── mcp-server/             # MCP Server for Claude Code (planned)
```

## Containerized Stack (Planned)
```yaml
services:
  mcp-server:    # Node.js MCP Server (Port 3000)
  clickup-api:   # .NET API Backend (Port 5000)  
  redis:         # Cache layer (Port 6379)
```

## Development Commands
```bash
# Build solution
dotnet build

# Run tests
dotnet test

# Run current console POC
dotnet run --project src/ClickUp.Console

# Future containerized stack
docker-compose up -d
```

## Validated ClickUp Capabilities
- ✅ **Teams/Workspaces**: Get team members with roles
- ✅ **Spaces**: Create and manage project spaces
- ✅ **Folders**: Organize projects hierarchically
- ✅ **Lists**: Create lists within folders or spaces
- ✅ **Tasks**: Full CRUD with assignments, due dates, priorities
- ✅ **Members**: Natural language search and assignment
- ✅ **Rate Limiting**: Respect ClickUp API limits (100-10000 req/min)

## Member Management Features
- **Natural Language Assignment**: "assign to Juan" → resolve to user ID
- **Flexible Search**: by username, email, or ID
- **Team Structure Mapping**: roles (owner, admin, member)
- **Assignment Resolution**: ["juan", "yarlen@27cobalto.com", "81585056"] → [81340798, 114172087, 81585056]

## Planned MCP Tools for Claude Code
- `create_task` - Natural language task creation with smart assignment
- `find_member` - Resolve team members by name/email
- `analyze_delays` - Project delay analysis and bottleneck detection
- `assign_optimal` - Workload-based smart assignment
- `create_project_structure` - Bootstrap projects with templates

## Key Configuration
- ClickUp API token in user secrets or environment variables
- Docker networking for container communication
- Redis caching for frequently accessed data
- MCP server registration in Claude Code settings

## Development Patterns
- Use `IClickUpService` interface for all ClickUp API operations
- All API operations return `Result<T>` for consistent error handling
- Cache team members and project data in Redis
- MCP tools expose JSON schemas for Claude Code discovery
- Structured logging across all components

## Important Notes
- Never log ClickUp API tokens or sensitive workspace data
- Always validate ClickUp responses before processing
- Implement proper rate limiting to respect ClickUp API limits
- MCP server handles protocol translation for Claude Code
- Containerized deployment for easy distribution and scaling