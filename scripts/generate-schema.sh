#!/bin/bash
set -e

dotnet ef migrations script \
  --idempotent \
  --output ../schema.sql \
  --project ../OrderService.Data/OrderService.Data.csproj \
  --startup-project ../OrderService/OrderService.csproj

echo "✅ Schema script generated at ../schema.sql"