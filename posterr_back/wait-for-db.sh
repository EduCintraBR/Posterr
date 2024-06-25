#!/bin/bash
# wait-for-db.sh

set -e

host="$1"
shift
port="$1"
shift

until nc -z -v -w30 $host $port
do
  echo "Waiting for database connection at $host:$port..."
  sleep 1
done

echo "Database is up!"
