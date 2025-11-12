#!/bin/bash
set -e

# Required environment variables:
# DOCKERHUB_USERNAME - your Docker Hub username
# DOCKERHUB_PASSWORD - your Docker Hub password or personal access token
# REPOSITORY - repository name (e.g. 'raulcv/addadult')
# KEEP_LAST - number of most recent tags to keep (e.g. 5)

KEEP_LAST=${KEEP_LAST:-5}

echo "Logging into Docker Hub..."
TOKEN=$(curl -s -H "Content-Type: application/json" -X POST -d "{\"username\": \"$DOCKERHUB_USERNAME\", \"password\": \"$DOCKERHUB_PASSWORD\"}" https://hub.docker.com/v2/users/login/ | jq -r .token)

if [ -z "$TOKEN" ]; then
  echo "Failed to get authentication token."
  exit 1
fi

echo "Fetching tags for repository $REPOSITORY..."
TAGS=$(curl -s -H "Authorization: JWT $TOKEN" "https://hub.docker.com/v2/repositories/$REPOSITORY/tags/?page_size=100" | jq -r '.results | sort_by(.last_updated) | .[].name')

# Convert tags to array
mapfile -t TAG_ARRAY <<<"$TAGS"

TOTAL_TAGS=${#TAG_ARRAY[@]}
DELETE_COUNT=$((TOTAL_TAGS - KEEP_LAST))

if [ "$DELETE_COUNT" -le 0 ]; then
  echo "No tags to delete, total tags ($TOTAL_TAGS) <= keep last ($KEEP_LAST)."
  exit 0
fi

echo "Deleting $DELETE_COUNT old tags..."

for ((i=0; i < DELETE_COUNT; i++)); do
  TAG="${TAG_ARRAY[i]}"
  echo "Deleting tag: $TAG"
  DELETE_RESP=$(curl -s -o /dev/null -w "%{http_code}" -X DELETE -H "Authorization: JWT $TOKEN" "https://hub.docker.com/v2/repositories/$REPOSITORY/tags/$TAG/")
  if [ "$DELETE_RESP" -ne 204 ]; then
    echo "Failed to delete tag $TAG, status code $DELETE_RESP"
  else
    echo "Deleted tag $TAG"
  fi
done
