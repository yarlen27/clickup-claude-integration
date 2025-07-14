import { createWriteStream, WriteStream } from 'fs';
import { mkdir } from 'fs/promises';
import { dirname } from 'path';

export enum LogLevel {
  DEBUG = 0,
  INFO = 1,
  WARN = 2,
  ERROR = 3
}

export interface LogEntry {
  timestamp: string;
  level: string;
  message: string;
  application: string;
  environment: string;
  correlationId?: string;
  userId?: string;
  properties?: Record<string, any>;
  error?: {
    name: string;
    message: string;
    stack?: string;
  };
}

export class Logger {
  private static instance: Logger;
  private logLevel: LogLevel;
  private fileStream?: WriteStream;
  private readonly application = 'ClickUp.MCP.Server';
  private readonly environment = process.env.NODE_ENV || 'development';

  private constructor() {
    this.logLevel = this.getLogLevelFromEnv();
    this.initializeFileLogging();
  }

  public static getInstance(): Logger {
    if (!Logger.instance) {
      Logger.instance = new Logger();
    }
    return Logger.instance;
  }

  private getLogLevelFromEnv(): LogLevel {
    const level = process.env.LOG_LEVEL?.toUpperCase();
    switch (level) {
      case 'DEBUG': return LogLevel.DEBUG;
      case 'INFO': return LogLevel.INFO;
      case 'WARN': return LogLevel.WARN;
      case 'ERROR': return LogLevel.ERROR;
      default: return LogLevel.INFO;
    }
  }

  private async initializeFileLogging(): Promise<void> {
    try {
      const logDir = './logs';
      await mkdir(logDir, { recursive: true });
      
      const logFile = `${logDir}/mcp-server-${new Date().toISOString().split('T')[0]}.log`;
      this.fileStream = createWriteStream(logFile, { flags: 'a' });
    } catch (error) {
      console.error('Failed to initialize file logging:', error);
    }
  }

  private shouldLog(level: LogLevel): boolean {
    return level >= this.logLevel;
  }

  private createLogEntry(level: LogLevel, message: string, properties?: Record<string, any>, error?: Error): LogEntry {
    const entry: LogEntry = {
      timestamp: new Date().toISOString(),
      level: LogLevel[level],
      message,
      application: this.application,
      environment: this.environment
    };

    if (properties) {
      entry.properties = properties;
    }

    if (error) {
      entry.error = {
        name: error.name,
        message: error.message,
        stack: error.stack
      };
    }

    return entry;
  }

  private writeLog(entry: LogEntry): void {
    const logLine = JSON.stringify(entry);
    
    // Console output with color coding
    const colorCode = this.getColorCode(entry.level);
    console.log(`${colorCode}[${entry.timestamp}] [${entry.level}] [${entry.application}] ${entry.message}\x1b[0m`);
    
    // File output
    if (this.fileStream) {
      this.fileStream.write(logLine + '\n');
    }
  }

  private getColorCode(level: string): string {
    switch (level) {
      case 'DEBUG': return '\x1b[36m'; // Cyan
      case 'INFO': return '\x1b[32m';  // Green
      case 'WARN': return '\x1b[33m';  // Yellow
      case 'ERROR': return '\x1b[31m'; // Red
      default: return '\x1b[0m';       // Reset
    }
  }

  public debug(message: string, properties?: Record<string, any>): void {
    if (this.shouldLog(LogLevel.DEBUG)) {
      const entry = this.createLogEntry(LogLevel.DEBUG, message, properties);
      this.writeLog(entry);
    }
  }

  public info(message: string, properties?: Record<string, any>): void {
    if (this.shouldLog(LogLevel.INFO)) {
      const entry = this.createLogEntry(LogLevel.INFO, message, properties);
      this.writeLog(entry);
    }
  }

  public warn(message: string, properties?: Record<string, any>): void {
    if (this.shouldLog(LogLevel.WARN)) {
      const entry = this.createLogEntry(LogLevel.WARN, message, properties);
      this.writeLog(entry);
    }
  }

  public error(message: string, error?: Error, properties?: Record<string, any>): void {
    if (this.shouldLog(LogLevel.ERROR)) {
      const entry = this.createLogEntry(LogLevel.ERROR, message, properties, error);
      this.writeLog(entry);
    }
  }

  public setCorrelationId(correlationId: string): void {
    // Implementation for request correlation
  }

  public close(): void {
    if (this.fileStream) {
      this.fileStream.end();
    }
  }
}

// Export singleton instance
export const logger = Logger.getInstance();