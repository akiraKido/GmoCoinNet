#!/bin/bash

# Make sure we're in the git repository root
cd "$(git rev-parse --show-toplevel)"

BASE_REF=$1
SHA=$2
GITHUB_TOKEN=$3
REPO=$4
PR_NUMBER=$5
CLAUDE_API_KEY=$6

# Get a summary of changes (number of files, insertions, deletions)
CHANGE_STATS=$(git diff --shortstat "$BASE_REF" "$SHA")

# Get list of modified files with their types and changes
CHANGES_JSON="{"
while IFS= read -r line; do
    status=$(echo "$line" | cut -f1)
    file=$(echo "$line" | cut -f2)
    category=$(dirname "$file" | cut -d'/' -f1)
    
    # Escape for JSON
    file=$(echo "$file" | sed 's/"/\\"/g')
    
    case $status in
        "A") change_type="added";;
        "M") change_type="modified";;
        "D") change_type="deleted";;
        *) change_type="changed";;
    esac
    
    if [[ $CHANGES_JSON != "{" ]]; then
        CHANGES_JSON+=","
    fi
    CHANGES_JSON+="\"$file\":{\"type\":\"$change_type\",\"category\":\"$category\"}"
done < <(git diff --name-status "$BASE_REF" "$SHA")
CHANGES_JSON+="}"

# Get the actual diff content
DIFF_CONTENT=$(git diff --unified=1 "$BASE_REF" "$SHA")

# Prepare the prompt for Claude
PROMPT=$(cat << EOF
You are a helpful assistant that summarizes code changes for pull requests. Please provide a natural, human-friendly summary of the following code changes.
Focus on the functional changes and improvements rather than just listing files.
Use bullet points to organize the summary.

Here are the changes:
Stats: $CHANGE_STATS
Changed files: $CHANGES_JSON
Diff preview: $DIFF_CONTENT

Please provide a concise summary focusing on what was actually changed or improved, not just which files were modified.
Format your response as bullet points, starting each point with "* ".
EOF
)

# Debug: Print request data
echo "Debug - Sending request to Claude API..."

# Call Claude API with proper error handling
RESPONSE=$(curl -s -w "\n%{http_code}" https://api.anthropic.com/v1/messages \
    -H "Content-Type: application/json" \
    -H "x-api-key: $CLAUDE_API_KEY" \
    -H "anthropic-version: 2023-06-01" \
    -d @- << EOF
{
    "model": "claude-3-5-sonnet-latest",
    "max_tokens": 4096,
    "messages": [{
        "role": "user",
        "content": $(echo "$PROMPT" | jq -R -s .)
    }]
}
EOF
)

# Split response into body and status code
RESPONSE_BODY=$(echo "$RESPONSE" | sed '$d')
HTTP_STATUS=$(echo "$RESPONSE" | tail -n1)

# Debug: Print response details
echo "Debug - HTTP Status: $HTTP_STATUS"
echo "Debug - Response Body:"
echo "$RESPONSE_BODY" | jq '.'

# Check if the request was successful
if [ "$HTTP_STATUS" -ne 200 ]; then
    echo "Error: Claude API request failed with status $HTTP_STATUS"
    SUMMARY="Error: Unable to generate summary. Please check the Claude API configuration."
else
    # Extract the summary from Claude's response
    SUMMARY=$(echo "$RESPONSE_BODY" | jq -r '.content[0].text // "Error: Unable to extract summary from Claude response"')
fi

# Create a structured description
DESCRIPTION=$(cat << EOF
## Automated PR Description

### Overview
$SUMMARY

### Detailed Changes
\`\`\`
$(git diff --stat "$BASE_REF" "$SHA")
\`\`\`

> Note: This description was automatically generated using AI. Please update it with more context if needed.
EOF
)

# If there's an existing description, add it as a comment
if [ ! -z "$7" ]; then
    DESCRIPTION+=$'\n\n<details><summary>Previous Description</summary>\n\n'"$7"$'\n</details>'
fi

# Update PR description using GitHub API
curl -X PATCH \
  -H "Authorization: token ${GITHUB_TOKEN}" \
  -H "Accept: application/vnd.github.v3+json" \
  "https://api.github.com/repos/${REPO}/pulls/${PR_NUMBER}" \
  -d "{\"body\": $(echo "$DESCRIPTION" | jq -R -s .)}"

# Output the description for GitHub Actions
echo "description<<EOF" >> $GITHUB_OUTPUT
echo "$DESCRIPTION" >> $GITHUB_OUTPUT
echo "EOF" >> $GITHUB_OUTPUT 