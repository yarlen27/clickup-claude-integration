FROM redis:7-alpine

# Copy custom configuration
COPY redis.conf /usr/local/etc/redis/redis.conf

# Create log directory
RUN mkdir -p /var/log/redis && chown redis:redis /var/log/redis

# Set proper permissions
RUN chown redis:redis /usr/local/etc/redis/redis.conf

# Expose port
EXPOSE 6379

# Use custom configuration
CMD ["redis-server", "/usr/local/etc/redis/redis.conf"]