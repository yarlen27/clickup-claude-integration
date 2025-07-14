# Multi-stage build for Node.js MCP Server
FROM node:20-alpine AS base
WORKDIR /app
RUN apk add --no-cache libc6-compat

FROM base AS deps
COPY src/mcp-server/package*.json ./
RUN npm ci --only=production

FROM base AS build
COPY src/mcp-server/package*.json ./
RUN npm ci
COPY src/mcp-server .
RUN npm run build

FROM base AS runtime
WORKDIR /app

# Create non-root user
RUN addgroup --system --gid 1001 nodejs
RUN adduser --system --uid 1001 mcpserver

# Copy dependencies
COPY --from=deps /app/node_modules ./node_modules
COPY --from=build /app/dist ./dist
COPY --from=build /app/package*.json ./

USER mcpserver

EXPOSE 3000

CMD ["node", "dist/index.js"]