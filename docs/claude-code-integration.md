# Claude Code Integration Architecture

## Tool Structure for Claude Code

Claude Code can discover and use tools through multiple patterns. For ClickUp integration, we recommend the **CLI + JSON Schema** approach.

### 1. CLI Tool Structure

```bash
# Main CLI entry point
clickup <command> <subcommand> [options]

# Examples
clickup task create "Implement auth" --assign juan --priority high
clickup member find juan
clickup analyze delays --project "Web App"
clickup workflow trigger "deploy-notification"
```

### 2. Tool Discovery

Claude Code discovers tools through:

#### A. Command Help with JSON Schema
```bash
clickup --help-json
{
  "name": "clickup",
  "description": "ClickUp project management integration tools",
  "version": "1.0.0",
  "commands": [
    {
      "name": "task",
      "description": "Task management operations",
      "subcommands": ["create", "update", "assign", "list", "analyze"]
    },
    {
      "name": "member",
      "description": "Team member operations", 
      "subcommands": ["find", "list", "workload"]
    }
  ]
}
```

#### B. Individual Command Schemas
```bash
clickup task create --help-json
{
  "command": "clickup task create",
  "description": "Create a new task with intelligent assignment",
  "usage": "clickup task create <title> [options]",
  "parameters": {
    "title": {
      "type": "string", 
      "required": true,
      "description": "Task title or natural language description"
    },
    "--assign": {
      "type": "string",
      "description": "Assign to user (name, email, or ID)"
    },
    "--priority": {
      "type": "string",
      "enum": ["low", "normal", "high", "urgent"],
      "description": "Task priority level"
    },
    "--due": {
      "type": "string",
      "description": "Due date (natural language: '2 weeks', '2025-01-15')"
    },
    "--project": {
      "type": "string", 
      "description": "Project or space name"
    },
    "--tags": {
      "type": "string",
      "description": "Comma-separated tags"
    }
  },
  "examples": [
    "clickup task create 'Implement user authentication'",
    "clickup task create 'Bug fix' --assign juan --priority high --due '3 days'",
    "clickup task create 'Code review' --assign 'juan,yarlen' --project 'Web App'"
  ],
  "output": {
    "type": "json",
    "schema": {
      "task_id": "string",
      "title": "string", 
      "url": "string",
      "assignees": ["string"],
      "created_at": "string"
    }
  }
}
```

### 3. Tool Registration

#### A. .claude/tools.json (Project Level)
```json
{
  "tools": [
    {
      "name": "clickup",
      "path": "./bin/clickup",
      "description": "ClickUp project management integration",
      "categories": ["project-management", "task-tracking"],
      "discovery_command": "clickup --help-json"
    }
  ]
}
```

#### B. ~/.claude/global-tools.json (User Level)  
```json
{
  "global_tools": [
    {
      "name": "clickup",
      "path": "/usr/local/bin/clickup",
      "description": "ClickUp integration",
      "auto_discover": true
    }
  ]
}
```

### 4. Execution Model

#### A. Direct CLI Execution
```python
# Claude Code executes
result = subprocess.run([
  "clickup", "task", "create", 
  "Implement user auth",
  "--assign", "juan",
  "--priority", "high"
], capture_output=True, text=True)

task_data = json.loads(result.stdout)
```

#### B. Interactive Mode
```bash
# Claude Code can ask for clarification
$ clickup task create "authentication"
❓ Multiple projects found. Which project?
  1. Web App Frontend  
  2. Mobile App
  3. Backend API
👤 Claude: Please specify project: Web App Frontend

✅ Task created: "Implement authentication" 
   ID: ABC-123 | Assigned: juan | Project: Web App Frontend
```

### 5. Output Standards

#### A. Machine-Readable Output
```bash
clickup task create "title" --format json
{
  "success": true,
  "data": {
    "task_id": "ABC-123",
    "title": "Implement authentication",
    "url": "https://app.clickup.com/t/ABC-123",
    "assignees": [{"id": 81340798, "name": "juan"}],
    "project": "Web App Frontend",
    "created_at": "2025-07-13T18:30:00Z"
  },
  "metadata": {
    "execution_time": "1.2s",
    "api_calls": 2
  }
}
```

#### B. Human-Readable Output  
```bash
clickup task create "title" --format human
✅ Task created successfully!
   
   📋 Title: Implement authentication
   🆔 ID: ABC-123  
   👤 Assigned: juan
   🏗️ Project: Web App Frontend
   🔗 URL: https://app.clickup.com/t/ABC-123
   
   ⏱️ Created in 1.2s
```

### 6. Error Handling

```bash
clickup task create "title" --assign "nonexistent"
{
  "success": false,
  "error": {
    "code": "MEMBER_NOT_FOUND",
    "message": "Member 'nonexistent' not found",
    "suggestions": [
      "Did you mean 'juan' (juan@27cobalto.com)?",
      "Use 'clickup member list' to see available members"
    ]
  }
}
```

## Implementation Priority

1. **Core CLI with JSON schemas** 
2. **Tool discovery mechanism**
3. **Standard output formats**
4. **Error handling with suggestions**
5. **Interactive mode for ambiguous inputs**

This approach allows Claude Code to:
- 🔍 **Discover** available tools automatically
- 📖 **Understand** what each tool does via schemas  
- 🎯 **Execute** tools with proper parameters
- 🔄 **Parse** structured responses
- ❓ **Handle** errors and ambiguity gracefully