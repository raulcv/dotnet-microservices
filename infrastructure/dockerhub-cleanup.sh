#!/bin/bash
set -e

# Required environment variables:
# DOCKERHUB_USERNAME - your Docker Hub username
# DOCKERHUB_PASSWORD - your personal access token
# REPOSITORY - repository name (e.g. 'raulcv/addadult')
# KEEP_LAST - number of most recent tags to keep (e.g. 5)

if [ -z "$DOCKERHUB_USERNAME" ] || [ -z "$DOCKERHUB_PASSWORD" ] || [ -z "$REPOSITORY" ]; then
  echo "Missing required environment variables."
  echo "DOCKERHUB_USERNAME: ${DOCKERHUB_USERNAME}"
  echo "DOCKERHUB_PASSWORD: ${DOCKERHUB_PASSWORD}"
  echo "REPOSITORY: ${REPOSITORY}"
  exit 1
else 
  echo "Environment variables are set."
  echo "DOCKERHUB_USERNAME: ${DOCKERHUB_USERNAME}"
  echo "DOCKERHUB_PASSWORD: ${DOCKERHUB_PASSWORD}"
  echo "REPOSITORY: ${REPOSITORY}"
fi

KEEP_LAST=${KEEP_LAST:-5}

# echo "Logging into Docker Hub..."
# TOKEN=$(curl -s -H "Content-Type: application/json" -X POST -d "{\"username\": \"$DOCKERHUB_USERNAME\", \"password\": \"$DOCKERHUB_PASSWORD\"}" https://hub.docker.com/v2/users/login/ | jq -r .token)

# if [ -z "$TOKEN" ]; then
#   echo "Failed to get authentication token."
#   exit 1
# fi

AUTH_RESPONSE=$(curl -s -H "Content-Type: application/json" -X POST -d "{\"username\": \"$DOCKERHUB_USERNAME\", \"password\": \"$DOCKERHUB_PASSWORD\"}" https://hub.docker.com/v2/users/login/)

if [ -z "$AUTH_RESPONSE" ]; then
  echo "Failed to get authentication token."
  echo "Response: $AUTH_RESPONSE"
  exit 1
fi

TOKEN=$(echo "$AUTH_RESPONSE" | jq -r .token)

echo "Authentication token: $TOKEN"

if [ "$TOKEN" == "null" ] || [ -z "$TOKEN" ]; then
  echo "Failed to authenticate to Docker Hub. Response:"
  echo "$AUTH_RESPONSE"
  exit 1
fi

echo "Fetching tags for repository $REPOSITORY..."
IMAGES_RESPONSE=$(curl -s -H "Authorization: JWT $TOKEN" "https://hub.docker.com/v2/repositories/$REPOSITORY/tags/?page_size=100")

if [ -z "$IMAGES_RESPONSE" ]; then
  echo "Failed to fetch tags for repository $REPOSITORY."
  exit 1
fi

TAGS=$(echo "$IMAGES_RESPONSE" | jq -r '.results[] | .name')

echo "Fetched tags: $TAGS"
# Convert tags to array
TAG_ARRAY=($TAGS)
echo "Total tags: ${#TAG_ARRAY[@]}"

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
