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
  # No database needed - ClickUp API is source of truth
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

## Comprehensive MCP Tools for Claude Code (50+ tools)

### 🎯 Task Management (8 tools)
- `create_task` - Natural language task creation with smart assignment
- `update_task` - Modify existing tasks with validation
- `assign_task` - Natural language member assignment  
- `delete_task` - Remove tasks with confirmation
- `get_task` - Retrieve task details by ID or name
- `search_tasks` - Find tasks by criteria (status, assignee, tags)
- `copy_task` - Duplicate tasks with customization
- `move_task` - Transfer tasks between lists/projects

### 👥 Member Operations (6 tools)
- `find_member` - Resolve team members by name/email/role
- `get_member_workload` - Analyze member task distribution
- `list_team_members` - Display team roster with roles
- `suggest_assignee` - Recommend optimal assignment based on workload
- `get_member_availability` - Check member status and activity
- `member_task_history` - View member's task completion patterns

### 🏗️ Project Structure (8 tools)
- `create_project_structure` - Bootstrap projects with templates
- `get_project_hierarchy` - Display complete project structure
- `clone_project_structure` - Duplicate project organization
- `archive_project` - Archive projects with data preservation
- `sync_project_status` - Update project progress indicators
- `validate_project_structure` - Check hierarchy consistency
- `export_project_data` - Extract project information
- `import_project_template` - Apply predefined structures

### 📊 Analytics & Insights (8 tools)
- `analyze_delays` - Project delay analysis and bottleneck detection
- `generate_workload_report` - Team capacity and distribution analysis
- `task_completion_trends` - Track productivity patterns over time
- `identify_blockers` - Find tasks blocking project progress
- `calculate_project_velocity` - Measure team delivery speed
- `predict_completion_dates` - Estimate project timelines
- `compare_project_performance` - Benchmark across projects
- `generate_burndown_chart` - Visual progress tracking

### 🔄 Workflow Automation (6 tools)
- `create_workflow_trigger` - Set up automated actions
- `assign_by_expertise` - Auto-assign based on member skills
- `escalate_overdue_tasks` - Automatic priority/assignment changes
- `batch_status_update` - Update multiple tasks simultaneously
- `auto_create_recurring_tasks` - Set up repeating work items
- `workflow_health_check` - Monitor automation effectiveness

### 🔍 Search & Discovery (4 tools)
- `smart_search` - Advanced search across all ClickUp entities
- `find_related_tasks` - Discover connected work items
- `search_by_natural_language` - Query using conversational language
- `filter_builder` - Create complex search criteria

### 📅 Date & Time Management (8 tools)
- `set_due_date` - Natural language date setting ("next Friday", "in 2 weeks")
- `extend_deadline` - Modify due dates with notifications
- `schedule_task_sequence` - Chain dependent tasks with dates
- `find_overdue_tasks` - Identify past-due items with analysis
- `optimize_timeline` - Suggest better scheduling
- `calendar_integration` - Sync with external calendars
- `time_zone_management` - Handle multi-timezone teams
- `deadline_reminder_setup` - Configure automatic notifications

### 📋 Subtasks & Task Hierarchy (6 tools)
- `create_subtask` - Break down tasks into smaller components
- `convert_to_subtask` - Transform existing tasks into subtasks
- `promote_subtask` - Convert subtasks to independent tasks
- `subtask_progress_rollup` - Calculate parent task completion
- `reorganize_task_hierarchy` - Restructure task relationships
- `bulk_subtask_operations` - Manage multiple subtasks efficiently

### ⏱️ Time Tracking & Estimates (6 tools)
- `add_time_estimate` - Set expected completion times
- `track_time_spent` - Log actual work time
- `compare_estimate_vs_actual` - Analyze estimation accuracy
- `generate_timesheet` - Create time reports for members
- `project_time_analysis` - Aggregate time data across projects
- `improve_estimation` - Learn from historical data patterns

### 🔗 Dependencies & Relationships (4 tools)
- `add_task_dependency` - Link tasks with blocking relationships
- `find_dependency_chain` - Trace task dependencies
- `resolve_dependency_conflicts` - Identify and fix circular dependencies
- `critical_path_analysis` - Find the longest dependency chain

### 📈 Status & Progress Tracking (4 tools)
- `update_task_status` - Change task states with validation
- `bulk_status_update` - Update multiple tasks efficiently
- `status_transition_rules` - Define workflow state rules
- `progress_percentage_update` - Set completion percentages

### 🎯 Priority & Planning (4 tools)
- `set_task_priority` - Assign importance levels with reasoning
- `reorder_by_priority` - Sort tasks by importance and urgency
- `priority_matrix_analysis` - Eisenhower matrix categorization
- `sprint_planning_assistant` - Help organize sprint backlogs

### 🌌 Space Management (8 tools)
- `create_space` - Set up new project spaces with templates
- `configure_space_settings` - Customize features and permissions
- `space_member_management` - Add/remove space members
- `archive_space` - Archive spaces with data preservation
- `space_analytics` - Generate space-specific reports
- `duplicate_space` - Clone entire spaces
- `space_health_check` - Validate space configuration
- `migrate_space_data` - Transfer data between spaces

### 📁 Folder Management (6 tools)
- `create_folder` - Organize projects within spaces
- `reorganize_folders` - Restructure folder hierarchy
- `folder_permission_management` - Control access levels
- `archive_folder` - Archive with task preservation
- `folder_template_apply` - Apply organizational templates
- `folder_analytics` - Generate folder-specific insights

### 📝 List Management (6 tools)
- `create_list` - Set up task containers with configurations
- `customize_list_views` - Configure display and sorting options
- `list_template_management` - Create and apply list templates
- `merge_lists` - Combine lists intelligently
- `list_automation_setup` - Configure list-specific automations
- `list_performance_metrics` - Analyze list productivity

### 🌐 Cross-Space Operations (4 tools)
- `cross_space_search` - Search across multiple spaces
- `move_tasks_between_spaces` - Transfer tasks with context preservation
- `sync_cross_space_data` - Keep related data synchronized
- `global_workspace_analytics` - Workspace-wide reporting

## Implementation Phases

### Phase 1: Core Functionality (MVP) - 18 tools
**Essential for basic ClickUp integration**

#### 🎯 Essential Task Operations (6 tools)
- `create_task` - Natural language task creation with smart assignment
- `update_task` - Modify existing tasks  
- `assign_task` - Natural language member assignment
- `get_task` - Retrieve task details
- `search_tasks` - Find tasks by criteria
- `set_due_date` - Natural language date setting

#### 👥 Basic Member Management (4 tools)  
- `find_member` - Resolve team members by name/email
- `list_team_members` - Display team roster
- `get_member_workload` - Basic workload analysis
- `suggest_assignee` - Recommend assignment

#### 🏗️ Project Structure Basics (4 tools)
- `get_project_hierarchy` - Display project structure
- `create_space` - Set up new spaces
- `create_list` - Set up task containers
- `create_folder` - Organize within spaces

#### 📊 Core Analytics (4 tools)
- `analyze_delays` - Delay analysis and bottlenecks
- `find_overdue_tasks` - Identify past-due items
- `generate_workload_report` - Team capacity analysis
- `task_completion_trends` - Basic productivity tracking

### Phase 2: Advanced Features (32 tools)
**Enhanced functionality and automation**

#### 🎯 Advanced Task Management (3 tools)
- `delete_task`, `copy_task`, `move_task`

#### 👥 Advanced Member Operations (2 tools)
- `get_member_availability`, `member_task_history`

#### 🏗️ Advanced Project Structure (4 tools)
- `create_project_structure`, `clone_project_structure`, `archive_project`, `validate_project_structure`

#### 📊 Advanced Analytics (4 tools)
- `identify_blockers`, `calculate_project_velocity`, `predict_completion_dates`, `generate_burndown_chart`

#### 🔄 Workflow Automation (6 tools)
- Complete automation suite for triggers, escalation, and batch operations

#### 🔍 Search & Discovery (4 tools)
- Advanced search capabilities and natural language queries

#### 📅 Advanced Date Management (6 tools)
- Timeline optimization, calendar integration, timezone management

#### 📋 Subtasks & Hierarchy (4 tools)
- Complete subtask management and hierarchy restructuring

### Phase 3: Enterprise Features (22 tools)
**Complex workflows and enterprise analytics**

#### ⏱️ Time Tracking & Estimates (6 tools)
- Complete time management and estimation improvement

#### 🔗 Dependencies & Relationships (4 tools)
- Task dependency management and critical path analysis

#### 📈 Status & Progress (4 tools)
- Advanced status management and workflow rules

#### 🎯 Priority & Planning (4 tools)
- Strategic planning and priority matrix analysis

#### 🌐 Cross-Space Operations (4 tools)
- Multi-workspace management and synchronization

## Complex Workflow Handling Strategy

### **Phase 1 Approach: Simple Tool Chaining**
Based on 2025 best practices for workflow orchestration:
- **Start Small, Scale Gradually**: Linear tool chains → Conditional workflows → Complex orchestration
- **MCP Hierarchical Agent Pattern**: Claude Code as orchestrator, tools as specialized agents
- **Modular & Composable Design**: Each tool = independent building block with single responsibility

### **Workflow Patterns**

#### **Linear Chain Pattern**
```
User: "Create task for Juan due next Friday"
Flow: find_member("Juan") → create_task(assignee=found_id, due="Friday")
```

#### **Data Dependency Chain**
```
User: "Create task in Web App project"  
Flow: get_project_hierarchy() → find space → create_task(list_id)
```

#### **Validation Chain**
```
User: "Assign overdue tasks to available member"
Flow: find_overdue_tasks() → get_member_workload() → assign_task()
```

### **Tool Communication Pattern**
- Each tool returns context for next step
- Claude Code maintains workflow state
- Graceful error handling with suggestions
- Clear data flow validation

## Deployment Strategy

### **Target Environment**
- **Primary Use**: Internal operations at Cobalto Soft
- **Distribution**: Private GitHub repository + Docker containers
- **Infrastructure**: Self-hosted, no external dependencies

### **Rollout Phases**
1. **Dev Team Validation** - Core functionality testing
2. **Team Expansion** - Full Phase 1 deployment  
3. **Company-wide** - Phase 1 + selected Phase 2 tools

### **Architecture Benefits**
- ✅ **No Database Required**: ClickUp API as single source of truth
- ✅ **Simple Infrastructure**: MCP Server + .NET API + Redis cache
- ✅ **Direct Control**: Internal deployment and updates
- ✅ **Scalable Design**: Phase-based feature expansion

## GitHub Issues Implementation Plan

### **Issue Categories & Breakdown (42 total)**

#### **🏗️ Infrastructure (8 issues)**
1. Setup .NET Web API project structure
2. Create MCP Server Node.js project  
3. Configure Docker containers setup
4. Setup Redis caching layer
5. Configure environment variables and secrets
6. Create docker-compose configuration
7. Setup CI/CD pipeline basics
8. Configure logging and monitoring

#### **🔌 Core Integration (6 issues)**
9. Implement ClickUp API client wrapper
10. Create member management service
11. Implement project hierarchy service  
12. Setup rate limiting and error handling
13. Create caching strategy implementation
14. Add API authentication and security

#### **🛠️ MCP Protocol (5 issues)**
15. Implement MCP server handshake
16. Create tool discovery mechanism
17. Define tool schemas and validation
18. Implement tool execution pipeline
19. Setup Claude Code integration

#### **🎯 Phase 1 Tools - Task Operations (6 issues)**  
20. Implement create_task tool
21. Implement update_task tool
22. Implement assign_task tool
23. Implement get_task tool
24. Implement search_tasks tool
25. Implement set_due_date tool

#### **👥 Phase 1 Tools - Member Management (4 issues)**
26. Implement find_member tool
27. Implement list_team_members tool
28. Implement get_member_workload tool
29. Implement suggest_assignee tool

#### **🏗️ Phase 1 Tools - Project Structure (4 issues)**
30. Implement get_project_hierarchy tool
31. Implement create_space tool
32. Implement create_list tool
33. Implement create_folder tool

#### **📊 Phase 1 Tools - Analytics (4 issues)**
34. Implement analyze_delays tool
35. Implement find_overdue_tasks tool
36. Implement generate_workload_report tool
37. Implement task_completion_trends tool

#### **🧪 Testing & Documentation (5 issues)**
38. Create unit tests for core services
39. Create integration tests for tools
40. Setup end-to-end testing
41. Create user documentation
42. Create developer setup guide

### **Implementation Timeline & Dependencies**

#### **SPRINT 1: Foundation (Issues 1-8) - Week 1**
```
Parallel:
├── #1: .NET Web API project ⚡ (no dependencies)
├── #2: MCP Server Node.js ⚡ (no dependencies)  
└── #5: Environment variables ⚡ (no dependencies)

Sequential:
#1 → #3: Docker containers (needs .NET API)
#2 → #6: docker-compose (needs both projects)
#5 → #7: CI/CD pipeline (needs env config)
#8: Logging (can start anytime)
```

#### **SPRINT 2: Core Services (Issues 9-14) - Week 2**
```
Dependencies:
#1 → #9: ClickUp API client (needs .NET API)
#9 → #10: Member management (needs API client)
#9 → #11: Project hierarchy (needs API client)
#9 → #12: Rate limiting (needs API client)

Parallel after #9:
├── #10: Member management
├── #11: Project hierarchy  
├── #12: Rate limiting
├── #13: Caching (depends on #4: Redis)
└── #14: Authentication (independent)
```

#### **SPRINT 3: MCP Integration (Issues 15-19) - Week 3**
```
Dependencies:
#2 → #15: MCP handshake (needs MCP Server)
#15 → #16: Tool discovery (needs handshake)
#16 → #17: Tool schemas (needs discovery)
#17 → #18: Tool execution (needs schemas)
#18 → #19: Claude Code integration (needs execution)

Sequential: #15 → #16 → #17 → #18 → #19
```

#### **SPRINT 4: Task Tools (Issues 20-25) - Week 4**
```
Dependencies:
#10, #11 → #20: create_task (needs members + hierarchy)
#20 → #21: update_task (similar to create)
#10 → #22: assign_task (needs member service)

Parallel after dependencies:
├── #20: create_task ⚡
├── #21: update_task ⚡  
├── #22: assign_task ⚡
├── #23: get_task ⚡
├── #24: search_tasks ⚡
└── #25: set_due_date ⚡
```

#### **SPRINT 5: Member & Structure Tools (Issues 26-33) - Week 5**
```
All depend on core services (Sprint 2 completed)

Parallel:
├── #26-29: Member tools ⚡
└── #30-33: Structure tools ⚡
```

#### **SPRINT 6: Analytics & Testing (Issues 34-42) - Week 6**
```
Analytics depend on all previous tools:
#20-33 → #34-37: Analytics tools

Testing can start after Sprint 4:
#20-25 → #38-40: Testing
#41-42: Documentation (independent)
```

#### **Critical Path**
```
Foundation → Core Services → MCP Integration → Tools Implementation
     ↓            ↓              ↓                   ↓
   Week 1      Week 2         Week 3             Week 4-6
```

## Claude Code Auto-Approve Configuration

Para evitar confirmaciones constantes durante desarrollo, configurar auto-approve:

### **Método 1: Comando Slash**
```
/settings auto_approve_bash_commands true
```

### **Método 2: Archivo de Settings**
Crear/editar `~/.claude/settings.json`:
```json
{
  "auto_approve_bash_commands": true,
  "auto_approve_file_edits": false
}
```

### **Método 3: Settings Locales del Proyecto**
En `.claude/settings.local.json`, agregar comandos permitidos:
```json
{
  "permissions": {
    "allow": [
      "Bash(dotnet:*)",
      "Bash(git:*)",
      "Bash(gh:*)",
      "Bash(npm:*)",
      "Bash(node:*)",
      "Bash(docker:*)",
      "Bash(curl:*)",
      "Bash(echo:*)",
      "Bash(mkdir:*)",
      "Bash(chmod:*)",
      "Bash(cat:*)",
      "Bash(cp:*)",
      "Bash(mv:*)",
      "Bash(rm:*)",
      "Bash(sleep:*)",
      "Bash(kill:*)",
      "Bash(ps:*)"
    ]
  }
}
```

**⚠️ Importante**: Reiniciar Claude Code después de cambiar configuración.

## Key Configuration
- ClickUp API token in user secrets or environment variables
- Docker networking for container communication
- Redis caching for frequently accessed data
- MCP server registration in Claude Code settings
- Auto-approve configurado para desarrollo eficiente

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