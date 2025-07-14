#!/usr/bin/env node

/**
 * ClickUp MCP Server
 * Model Context Protocol server for ClickUp integration with Claude Code
 */

import { Server } from '@modelcontextprotocol/sdk/server/index.js';
import { StdioServerTransport } from '@modelcontextprotocol/sdk/server/stdio.js';
import {
  CallToolRequestSchema,
  ErrorCode,
  ListToolsRequestSchema,
  McpError,
} from '@modelcontextprotocol/sdk/types.js';

class ClickUpMCPServer {
  private server: Server;

  constructor() {
    this.server = new Server(
      {
        name: 'clickup-integration',
        version: '1.0.0',
      },
      {
        capabilities: {
          tools: {},
        },
      }
    );

    this.setupToolHandlers();
    this.setupErrorHandling();
  }

  private setupToolHandlers(): void {
    this.server.setRequestHandler(ListToolsRequestSchema, async () => {
      return {
        tools: [
          {
            name: 'health_check',
            description: 'Check MCP server health and connection status',
            inputSchema: {
              type: 'object',
              properties: {},
              required: [],
            },
          },
          {
            name: 'echo',
            description: 'Echo back a message for testing',
            inputSchema: {
              type: 'object',
              properties: {
                message: {
                  type: 'string',
                  description: 'Message to echo back',
                },
              },
              required: ['message'],
            },
          },
        ],
      };
    });

    this.server.setRequestHandler(CallToolRequestSchema, async (request) => {
      const { name, arguments: args } = request.params;

      try {
        switch (name) {
          case 'health_check':
            return {
              content: [
                {
                  type: 'text',
                  text: JSON.stringify({
                    status: 'healthy',
                    server: 'ClickUp MCP Server',
                    version: '1.0.0',
                    timestamp: new Date().toISOString(),
                    capabilities: ['tools'],
                  }, null, 2),
                },
              ],
            };

          case 'echo':
            const message = args?.message as string;
            if (!message) {
              throw new McpError(
                ErrorCode.InvalidParams,
                'Message parameter is required'
              );
            }
            return {
              content: [
                {
                  type: 'text',
                  text: `Echo: ${message}`,
                },
              ],
            };

          default:
            throw new McpError(
              ErrorCode.MethodNotFound,
              `Unknown tool: ${name}`
            );
        }
      } catch (error) {
        const errorMessage = error instanceof Error ? error.message : 'Unknown error';
        throw new McpError(ErrorCode.InternalError, errorMessage);
      }
    });
  }

  private setupErrorHandling(): void {
    this.server.onerror = (error) => {
      console.error('[MCP Error]', error);
    };

    process.on('SIGINT', async () => {
      await this.server.close();
      process.exit(0);
    });
  }

  async run(): Promise<void> {
    const transport = new StdioServerTransport();
    await this.server.connect(transport);
    console.error('ClickUp MCP Server running on stdio');
  }
}

// Start the server
const server = new ClickUpMCPServer();
server.run().catch((error) => {
  console.error('Failed to start MCP server:', error);
  process.exit(1);
});