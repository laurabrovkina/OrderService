FROM postgres:15.13-alpine3.20

# Copy database DDL script to the /docker-entrypoint-initdb.d directory
# PostgresSQL will automatically execute script in this directory on the first startup 
COPY initdb.sql /docker-entrypoint-initdb.d/

EXPOSE 5432