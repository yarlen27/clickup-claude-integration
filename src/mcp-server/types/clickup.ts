/**
 * Type definitions for ClickUp API integration
 */

export interface ClickUpConfig {
  apiToken: string;
  baseUrl: string;
}

export interface ToolResult {
  content: Array<{
    type: 'text';
    text: string;
  }>;
}

export interface ClickUpTask {
  id: string;
  name: string;
  description?: string;
  status: string;
  priority?: number;
  due_date?: string;
  assignees: ClickUpUser[];
  tags: string[];
  url: string;
}

export interface ClickUpUser {
  id: number;
  username: string;
  email: string;
  role_key?: string;
}

export interface ClickUpSpace {
  id: string;
  name: string;
  color?: string;
  private: boolean;
  statuses: ClickUpStatus[];
}

export interface ClickUpStatus {
  id: string;
  status: string;
  color: string;
  type: string;
  orderindex: number;
}

export interface ClickUpList {
  id: string;
  name: string;
  orderindex: number;
  content: string;
  status?: ClickUpStatus;
  priority?: ClickUpPriority;
  assignee?: ClickUpUser;
  task_count?: number;
  due_date?: string;
  start_date?: string;
  folder: {
    id: string;
    name: string;
    hidden: boolean;
    access: boolean;
  };
  space: {
    id: string;
    name: string;
    access: boolean;
  };
  inbound_address?: string;
  archived: boolean;
  override_statuses?: boolean;
  permission_level?: string;
}

export interface ClickUpPriority {
  id?: string;
  priority?: string;
  color?: string;
  orderindex?: string;
}