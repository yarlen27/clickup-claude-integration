#!/bin/bash

# ClickUp Integration - GitHub Issues Creation Script
# Creates all 42 issues organized by sprints/milestones

echo "🚀 Creating 42 GitHub issues for ClickUp Integration project..."

# SPRINT 1: Infrastructure (8 issues) - Milestone 1
echo "📦 Creating Sprint 1: Infrastructure issues..."

gh issue create --title "Setup .NET Web API project structure" \
  --body "Create the .NET 8.0 Web API project with Clean Architecture structure

## Tasks:
- [ ] Create ClickUp.Web.API project
- [ ] Setup Clean Architecture folders (Controllers, Services, Models)
- [ ] Configure dependency injection
- [ ] Add basic health check endpoint
- [ ] Setup Swagger/OpenAPI documentation

## Acceptance Criteria:
- API project compiles successfully
- Health check endpoint responds
- Swagger UI accessible at /swagger
- Ready for ClickUp service integration

## Dependencies:
- None (foundational task)" \

gh issue create --title "Create MCP Server Node.js project" \
  --body "Setup Node.js + TypeScript MCP server for Claude Code integration

## Tasks:
- [ ] Initialize Node.js project with TypeScript
- [ ] Install MCP protocol dependencies
- [ ] Setup basic project structure (src/, dist/, types/)
- [ ] Configure tsconfig.json and package.json
- [ ] Add basic MCP server skeleton

## Acceptance Criteria:
- Node.js project compiles successfully
- TypeScript configuration working
- MCP dependencies installed
- Basic server structure ready

## Dependencies:
- None (foundational task)" \

gh issue create --title "Configure Docker containers setup" \
  --body "Create Docker containers for .NET API and MCP server

## Tasks:
- [ ] Create Dockerfile for .NET API
- [ ] Create Dockerfile for Node.js MCP server
- [ ] Configure multi-stage builds for optimization
- [ ] Setup Docker networking between containers
- [ ] Test container builds locally

## Acceptance Criteria:
- Both containers build successfully
- Containers can communicate via Docker network
- Images are optimized (multi-stage builds)
- Ready for docker-compose integration

## Dependencies:
- #1: .NET Web API project
- #2: MCP Server project" \

gh issue create --title "Setup Redis caching layer" \
  --body "Configure Redis for member and project data caching

## Tasks:
- [ ] Add Redis Docker container configuration
- [ ] Install Redis client libraries (.NET and Node.js)
- [ ] Setup Redis connection configuration
- [ ] Create basic cache key patterns
- [ ] Add Redis health checks

## Acceptance Criteria:
- Redis container runs successfully
- Both API and MCP server can connect to Redis
- Cache operations (get/set) working
- Health checks passing

## Dependencies:
- Docker infrastructure setup" \

gh issue create --title "Configure environment variables and secrets" \
  --body "Setup secure configuration management for all services

## Tasks:
- [ ] Define environment variables schema
- [ ] Setup .env file templates
- [ ] Configure user secrets for development
- [ ] Add environment validation
- [ ] Document configuration requirements

## Acceptance Criteria:
- All services use environment variables
- Secrets are properly secured
- Development setup documented
- Configuration validation working

## Dependencies:
- None (foundational task)" \

gh issue create --title "Create docker-compose configuration" \
  --body "Orchestrate all services with docker-compose for development

## Tasks:
- [ ] Create docker-compose.yml file
- [ ] Configure service dependencies and networks
- [ ] Setup volume mounts for development
- [ ] Add environment variable overrides
- [ ] Create docker-compose.override.yml for local dev

## Acceptance Criteria:
- Single command starts all services (docker-compose up)
- Services can communicate properly
- Development volumes mounted correctly
- Environment configuration working

## Dependencies:
- #1: .NET API Dockerfile
- #2: MCP Server Dockerfile  
- #4: Redis configuration" \

gh issue create --title "Setup CI/CD pipeline basics" \
  --body "Configure GitHub Actions for automated builds and tests

## Tasks:
- [ ] Create .github/workflows/ci.yml
- [ ] Setup .NET build and test workflow
- [ ] Setup Node.js build and test workflow
- [ ] Configure Docker image builds
- [ ] Add basic linting and code quality checks

## Acceptance Criteria:
- Automated builds on pull requests
- Tests run automatically
- Docker images build in CI
- Code quality checks pass

## Dependencies:
- #5: Environment configuration" \

gh issue create --title "Configure logging and monitoring" \
  --body "Setup structured logging and basic monitoring for all services

## Tasks:
- [ ] Configure Serilog in .NET API
- [ ] Setup structured logging in Node.js MCP server
- [ ] Add correlation IDs for request tracing
- [ ] Configure log levels and outputs
- [ ] Add basic health check endpoints

## Acceptance Criteria:
- Structured logs from all services
- Request correlation working
- Log levels configurable
- Health checks accessible

## Dependencies:
- #1: .NET API project
- #2: MCP Server project" \

# SPRINT 2: Core Integration (6 issues) - Milestone 2
echo "🔌 Creating Sprint 2: Core Integration issues..."

gh issue create --title "Implement ClickUp API client wrapper" \
  --body "Create robust ClickUp API client with error handling and rate limiting

## Tasks:
- [ ] Extend existing ClickUpService with new endpoints
- [ ] Add comprehensive error handling and retry logic
- [ ] Implement rate limiting to respect ClickUp API limits
- [ ] Add request/response logging
- [ ] Create API client interface for dependency injection

## Acceptance Criteria:
- All required ClickUp API endpoints accessible
- Rate limiting prevents API quota exhaustion
- Errors handled gracefully with proper logging
- Client is testable and injectable

## Dependencies:
- #1: .NET Web API project structure" \

gh issue create --title "Create member management service" \
  --body "Implement comprehensive member search and assignment functionality

## Tasks:
- [ ] Extend existing member service with advanced features
- [ ] Add natural language member search
- [ ] Implement workload analysis capabilities
- [ ] Add member availability checking
- [ ] Create assignment recommendation engine

## Acceptance Criteria:
- Natural language member search working ('juan' → user ID)
- Workload analysis provides accurate data
- Assignment recommendations based on capacity
- All member operations cached appropriately

## Dependencies:
- #9: ClickUp API client wrapper" \

gh issue create --title "Implement project hierarchy service" \
  --body "Create service for managing ClickUp project structure (Spaces, Folders, Lists)

## Tasks:
- [ ] Extend existing hierarchy service
- [ ] Add project structure validation
- [ ] Implement hierarchy traversal and search
- [ ] Add project creation templates
- [ ] Create structure export/import functionality

## Acceptance Criteria:
- Complete project hierarchy accessible
- Structure validation prevents inconsistencies
- Fast hierarchy search and traversal
- Templates for common project structures

## Dependencies:
- #9: ClickUp API client wrapper" \

gh issue create --title "Setup rate limiting and error handling" \
  --body "Implement comprehensive error handling and API rate limiting

## Tasks:
- [ ] Add Polly for resilience patterns
- [ ] Implement exponential backoff for retries
- [ ] Add circuit breaker for API failures
- [ ] Create custom exception types
- [ ] Add error response standardization

## Acceptance Criteria:
- API calls never exceed ClickUp rate limits
- Transient errors handled with retries
- Circuit breaker prevents cascading failures
- Consistent error responses to clients

## Dependencies:
- #9: ClickUp API client wrapper" \

gh issue create --title "Create caching strategy implementation" \
  --body "Implement Redis caching for frequently accessed ClickUp data

## Tasks:
- [ ] Add Redis caching to member service
- [ ] Implement project hierarchy caching
- [ ] Add cache invalidation strategies
- [ ] Create cache warming for critical data
- [ ] Add cache performance monitoring

## Acceptance Criteria:
- Member data cached for 15 minutes
- Project hierarchy cached for 5 minutes
- Cache invalidation works properly
- Significant performance improvement measurable

## Dependencies:
- #4: Redis caching layer
- #10: Member management service
- #11: Project hierarchy service" \

gh issue create --title "Add API authentication and security" \
  --body "Implement secure authentication and authorization for the API

## Tasks:
- [ ] Add API key authentication
- [ ] Implement request validation middleware
- [ ] Add CORS configuration
- [ ] Setup request rate limiting per client
- [ ] Add security headers

## Acceptance Criteria:
- Only authenticated requests allowed
- Request validation prevents malformed data
- CORS properly configured for MCP server
- Rate limiting per client implemented
- Security headers protect against common attacks

## Dependencies:
- #1: .NET Web API project structure" \

# SPRINT 3: MCP Integration (5 issues) - Milestone 3
echo "🛠️ Creating Sprint 3: MCP Integration issues..."

gh issue create --title "Implement MCP server handshake" \
  --body "Implement Model Context Protocol handshake and initialization

## Tasks:
- [ ] Add MCP protocol handshake implementation
- [ ] Setup client-server capability negotiation
- [ ] Implement protocol version management
- [ ] Add connection lifecycle management
- [ ] Create handshake error handling

## Acceptance Criteria:
- MCP handshake completes successfully with Claude Code
- Capability negotiation works properly
- Protocol versions handled correctly
- Connection errors handled gracefully

## Dependencies:
- #2: MCP Server Node.js project" \

gh issue create --title "Create tool discovery mechanism" \
  --body "Implement MCP tool discovery for Claude Code integration

## Tasks:
- [ ] Create tool registry and discovery service
- [ ] Implement tool capability enumeration
- [ ] Add tool metadata and descriptions
- [ ] Create dynamic tool loading
- [ ] Add tool availability checking

## Acceptance Criteria:
- Claude Code can discover all available tools
- Tool metadata properly formatted
- Dynamic tool loading works
- Tool availability reflects backend status

## Dependencies:
- #15: MCP server handshake" \

gh issue create --title "Define tool schemas and validation" \
  --body "Create JSON schemas for all MCP tools and implement validation

## Tasks:
- [ ] Define JSON schemas for all Phase 1 tools
- [ ] Implement schema validation middleware
- [ ] Add parameter type checking
- [ ] Create schema documentation generation
- [ ] Add validation error responses

## Acceptance Criteria:
- All tools have complete JSON schemas
- Parameter validation prevents invalid requests
- Clear validation error messages
- Schema documentation auto-generated

## Dependencies:
- #16: Tool discovery mechanism" \

gh issue create --title "Implement tool execution pipeline" \
  --body "Create execution pipeline for MCP tool requests

## Tasks:
- [ ] Add tool request routing
- [ ] Implement execution context management
- [ ] Add tool response formatting
- [ ] Create execution error handling
- [ ] Add execution logging and monitoring

## Acceptance Criteria:
- Tool requests routed to correct handlers
- Execution context properly managed
- Responses formatted consistently
- Errors handled and logged appropriately

## Dependencies:
- #17: Tool schemas and validation" \

gh issue create --title "Setup Claude Code integration" \
  --body "Complete end-to-end integration with Claude Code

## Tasks:
- [ ] Test MCP server with Claude Code
- [ ] Add integration documentation
- [ ] Create development setup guide
- [ ] Add troubleshooting documentation
- [ ] Test all tool discovery and execution

## Acceptance Criteria:
- Claude Code successfully connects to MCP server
- All tools discoverable and executable
- Integration documentation complete
- Development setup guide tested

## Dependencies:
- #18: Tool execution pipeline" \

# SPRINT 4: Task Tools (6 issues) - Milestone 4
echo "🎯 Creating Sprint 4: Task Tools issues..."

gh issue create --title "Implement create_task tool" \
  --body "Implement natural language task creation with smart assignment

## Tasks:
- [ ] Create create_task MCP tool
- [ ] Add natural language processing for task details
- [ ] Implement smart member assignment
- [ ] Add due date parsing ('next Friday', 'in 2 weeks')
- [ ] Integrate with project hierarchy service

## Acceptance Criteria:
- Natural language task creation working
- Smart assignment based on workload/skills
- Flexible due date parsing
- Tasks created in correct project context

## Dependencies:
- #10: Member management service
- #11: Project hierarchy service" \

gh issue create --title "Implement update_task tool" \
  --body "Create tool for updating existing tasks with validation

## Tasks:
- [ ] Create update_task MCP tool
- [ ] Add task validation and existence checking
- [ ] Implement partial update support
- [ ] Add update conflict detection
- [ ] Create update history tracking

## Acceptance Criteria:
- Task updates work reliably
- Validation prevents invalid updates
- Partial updates supported
- Update conflicts detected and handled

## Dependencies:
- #20: create_task tool (similar patterns)" \

gh issue create --title "Implement assign_task tool" \
  --body "Create natural language task assignment tool

## Tasks:
- [ ] Create assign_task MCP tool
- [ ] Add natural language member resolution
- [ ] Implement multi-member assignment
- [ ] Add assignment validation
- [ ] Create assignment notification system

## Acceptance Criteria:
- Natural language assignment working ('assign to Juan')
- Multiple members can be assigned
- Assignment validation prevents errors
- Notifications sent appropriately

## Dependencies:
- #10: Member management service" \

gh issue create --title "Implement get_task tool" \
  --body "Create tool for retrieving task details and information

## Tasks:
- [ ] Create get_task MCP tool
- [ ] Add flexible task lookup (ID, name, URL)
- [ ] Implement task detail formatting
- [ ] Add related task discovery
- [ ] Create task export functionality

## Acceptance Criteria:
- Tasks retrievable by multiple identifiers
- Complete task information returned
- Related tasks discoverable
- Export functionality working

## Dependencies:
- #11: Project hierarchy service" \

gh issue create --title "Implement search_tasks tool" \
  --body "Create comprehensive task search functionality

## Tasks:
- [ ] Create search_tasks MCP tool
- [ ] Add flexible search criteria (status, assignee, tags, dates)
- [ ] Implement natural language search queries
- [ ] Add search result ranking
- [ ] Create saved search functionality

## Acceptance Criteria:
- Flexible search criteria supported
- Natural language queries working
- Results ranked by relevance
- Saved searches available

## Dependencies:
- #10: Member management service
- #11: Project hierarchy service" \

gh issue create --title "Implement set_due_date tool" \
  --body "Create natural language due date setting tool

## Tasks:
- [ ] Create set_due_date MCP tool
- [ ] Add natural language date parsing
- [ ] Implement timezone handling
- [ ] Add date validation and conflict checking
- [ ] Create recurring date support

## Acceptance Criteria:
- Natural language dates parsed correctly
- Timezone handling works properly
- Date validation prevents invalid dates
- Recurring dates supported

## Dependencies:
- #20: create_task tool (date parsing patterns)" \

# SPRINT 5: Member & Structure Tools (8 issues) - Milestone 5
echo "👥 Creating Sprint 5: Member & Structure Tools issues..."

gh issue create --title "Implement find_member tool" \
  --body "Create advanced member search and resolution tool

## Tasks:
- [ ] Create find_member MCP tool
- [ ] Add fuzzy matching for member names
- [ ] Implement role-based search
- [ ] Add skill/expertise search
- [ ] Create member profile display

## Acceptance Criteria:
- Fuzzy matching finds members with typos
- Role-based search working
- Skill search returns relevant members
- Member profiles displayed clearly

## Dependencies:
- Sprint 2 core services completed" \

gh issue create --title "Implement list_team_members tool" \
  --body "Create comprehensive team member listing tool

## Tasks:
- [ ] Create list_team_members MCP tool
- [ ] Add team/workspace filtering
- [ ] Implement member role display
- [ ] Add activity status indicators
- [ ] Create member statistics

## Acceptance Criteria:
- Team members listed with roles
- Activity status visible
- Statistics provide useful insights
- Filtering works correctly

## Dependencies:
- Sprint 2 core services completed" \

gh issue create --title "Implement get_member_workload tool" \
  --body "Create workload analysis and capacity planning tool

## Tasks:
- [ ] Create get_member_workload MCP tool
- [ ] Add task count and complexity analysis
- [ ] Implement capacity calculations
- [ ] Add workload visualization data
- [ ] Create overload detection

## Acceptance Criteria:
- Accurate workload calculations
- Capacity analysis helpful for planning
- Overload detection prevents burnout
- Visualization data useful

## Dependencies:
- Sprint 2 core services completed
- Sprint 4 task tools for data" \

gh issue create --title "Implement suggest_assignee tool" \
  --body "Create intelligent assignment recommendation tool

## Tasks:
- [ ] Create suggest_assignee MCP tool
- [ ] Add workload-based recommendations
- [ ] Implement skill matching
- [ ] Add availability checking
- [ ] Create recommendation scoring

## Acceptance Criteria:
- Recommendations based on multiple factors
- Skill matching works effectively
- Availability considered in suggestions
- Scoring helps choose best option

## Dependencies:
- #26: find_member tool
- #28: get_member_workload tool" \

gh issue create --title "Implement get_project_hierarchy tool" \
  --body "Create comprehensive project structure display tool

## Tasks:
- [ ] Create get_project_hierarchy MCP tool
- [ ] Add interactive hierarchy display
- [ ] Implement structure validation
- [ ] Add hierarchy search functionality
- [ ] Create structure export

## Acceptance Criteria:
- Clear hierarchy visualization
- Structure validation catches issues
- Search within hierarchy working
- Export functionality useful

## Dependencies:
- Sprint 2 core services completed" \

gh issue create --title "Implement create_space tool" \
  --body "Create tool for setting up new project spaces

## Tasks:
- [ ] Create create_space MCP tool
- [ ] Add space template support
- [ ] Implement permission configuration
- [ ] Add feature selection
- [ ] Create space initialization

## Acceptance Criteria:
- Spaces created with proper configuration
- Templates speed up setup
- Permissions configured correctly
- Features selected appropriately

## Dependencies:
- Sprint 2 core services completed" \

gh issue create --title "Implement create_list tool" \
  --body "Create tool for setting up task lists with configurations

## Tasks:
- [ ] Create create_list MCP tool
- [ ] Add list template support
- [ ] Implement custom field configuration
- [ ] Add automation setup
- [ ] Create list initialization

## Acceptance Criteria:
- Lists created with proper setup
- Templates available for common use cases
- Custom fields configured correctly
- Automation rules applied

## Dependencies:
- Sprint 2 core services completed" \

gh issue create --title "Implement create_folder tool" \
  --body "Create tool for organizing projects with folders

## Tasks:
- [ ] Create create_folder MCP tool
- [ ] Add folder hierarchy support
- [ ] Implement permission inheritance
- [ ] Add folder template support
- [ ] Create folder organization tools

## Acceptance Criteria:
- Folders created in correct hierarchy
- Permissions inherited properly
- Templates available
- Organization tools helpful

## Dependencies:
- Sprint 2 core services completed" \

# SPRINT 6: Analytics & Testing (9 issues) - Milestone 6
echo "📊 Creating Sprint 6: Analytics & Testing issues..."

gh issue create --title "Implement analyze_delays tool" \
  --body "Create comprehensive project delay analysis tool

## Tasks:
- [ ] Create analyze_delays MCP tool
- [ ] Add bottleneck detection algorithms
- [ ] Implement delay prediction models
- [ ] Add delay cause analysis
- [ ] Create actionable recommendations

## Acceptance Criteria:
- Bottlenecks identified accurately
- Delay predictions help planning
- Cause analysis provides insights
- Recommendations are actionable

## Dependencies:
- All Sprint 4 task tools completed" \

gh issue create --title "Implement find_overdue_tasks tool" \
  --body "Create overdue task identification and analysis tool

## Tasks:
- [ ] Create find_overdue_tasks MCP tool
- [ ] Add overdue severity calculation
- [ ] Implement impact analysis
- [ ] Add escalation recommendations
- [ ] Create overdue prevention insights

## Acceptance Criteria:
- Overdue tasks identified with severity
- Impact analysis shows consequences
- Escalation recommendations helpful
- Prevention insights reduce future overdues

## Dependencies:
- All Sprint 4 task tools completed" \

gh issue create --title "Implement generate_workload_report tool" \
  --body "Create comprehensive team workload reporting tool

## Tasks:
- [ ] Create generate_workload_report MCP tool
- [ ] Add team capacity analysis
- [ ] Implement workload distribution charts
- [ ] Add burnout risk detection
- [ ] Create capacity planning recommendations

## Acceptance Criteria:
- Workload reports provide clear insights
- Capacity analysis helps planning
- Burnout risk identified early
- Planning recommendations actionable

## Dependencies:
- All Sprint 5 member tools completed" \

gh issue create --title "Implement task_completion_trends tool" \
  --body "Create task completion and productivity trend analysis tool

## Tasks:
- [ ] Create task_completion_trends MCP tool
- [ ] Add velocity calculations
- [ ] Implement trend visualization data
- [ ] Add productivity insights
- [ ] Create performance recommendations

## Acceptance Criteria:
- Completion trends show clear patterns
- Velocity calculations accurate
- Productivity insights valuable
- Performance recommendations helpful

## Dependencies:
- All Sprint 4 task tools completed" \

gh issue create --title "Create unit tests for core services" \
  --body "Implement comprehensive unit test suite for core services

## Tasks:
- [ ] Add unit tests for ClickUp API client
- [ ] Test member management service
- [ ] Test project hierarchy service
- [ ] Add caching service tests
- [ ] Test authentication and security

## Acceptance Criteria:
- 90%+ code coverage for core services
- All critical paths tested
- Edge cases and error conditions covered
- Tests run fast and reliably

## Dependencies:
- Sprint 2 core services completed" \

gh issue create --title "Create integration tests for tools" \
  --body "Implement integration test suite for all MCP tools

## Tasks:
- [ ] Add integration tests for task tools
- [ ] Test member management tools
- [ ] Test project structure tools  
- [ ] Test analytics tools
- [ ] Add end-to-end workflow tests

## Acceptance Criteria:
- All tools tested with real API calls
- Integration points verified
- Workflow scenarios tested
- Tests can run against test environment

## Dependencies:
- All Sprint 4 and 5 tools completed" \

gh issue create --title "Setup end-to-end testing" \
  --body "Create complete end-to-end testing with Claude Code integration

## Tasks:
- [ ] Setup E2E testing framework
- [ ] Add Claude Code integration tests
- [ ] Test complete user workflows
- [ ] Add performance testing
- [ ] Create test data management

## Acceptance Criteria:
- Complete workflows tested end-to-end
- Claude Code integration verified
- Performance benchmarks established
- Test data managed properly

## Dependencies:
- Sprint 3 MCP integration completed" \

gh issue create --title "Create user documentation" \
  --body "Create comprehensive user documentation for ClickUp integration

## Tasks:
- [ ] Write installation and setup guide
- [ ] Document all available tools
- [ ] Create usage examples and tutorials
- [ ] Add troubleshooting guide
- [ ] Create video demonstrations

## Acceptance Criteria:
- Setup guide enables easy installation
- All tools documented with examples
- Tutorials cover common workflows
- Troubleshooting guide comprehensive

## Dependencies:
- All functionality completed" \

gh issue create --title "Create developer setup guide" \
  --body "Create comprehensive developer documentation and contribution guide

## Tasks:
- [ ] Write development environment setup
- [ ] Document architecture and design decisions
- [ ] Create contribution guidelines
- [ ] Add code style and standards
- [ ] Create debugging and testing guides

## Acceptance Criteria:
- Developers can setup environment easily
- Architecture well documented
- Contribution process clear
- Code standards defined

## Dependencies:
- All development completed" \

echo "✅ Created all 42 GitHub issues successfully!"
echo "🎯 Issues organized across 6 sprints/milestones"
echo "📊 Ready for development and project management"