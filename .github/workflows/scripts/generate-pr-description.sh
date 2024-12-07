#!/bin/bash

BASE_REF=$1
SHA=$2
GITHUB_TOKEN=$3
REPO=$4
PR_NUMBER=$5

# Get a summary of changes (number of files, insertions, deletions)
CHANGE_STATS=$(git diff --shortstat "$BASE_REF" "$SHA")

# Get list of modified files with their types
declare -A FILE_CHANGES
while IFS= read -r line; do
    status=$(echo "$line" | cut -f1)
    file=$(echo "$line" | cut -f2)
    
    # Get the directory/category of the file
    category=$(dirname "$file" | cut -d'/' -f1)
    FILE_CHANGES["$category"]+="$status:$file "
done < <(git diff --name-status "$BASE_REF" "$SHA")

# Generate summary paragraph
SUMMARY="This PR "

# Add file count and general changes
SUMMARY+="$CHANGE_STATS. "

# Add category-specific changes
for category in "${!FILE_CHANGES[@]}"; do
    changes="${FILE_CHANGES[$category]}"
    
    case $category in
        "GmoCoinNet")
            SUMMARY+="In the main library, "
            ;;
        "GmoCoinNet.Examples")
            SUMMARY+="In the examples, "
            ;;
        ".github")
            SUMMARY+="In the GitHub workflows, "
            ;;
        *)
            SUMMARY+="In $category, "
            ;;
    esac
    
    # Count modifications by type
    added=$(echo "$changes" | tr ' ' '\n' | grep '^A:' | wc -l)
    modified=$(echo "$changes" | tr ' ' '\n' | grep '^M:' | wc -l)
    deleted=$(echo "$changes" | tr ' ' '\n' | grep '^D:' | wc -l)
    
    if [ $added -gt 0 ]; then
        SUMMARY+="added $added file(s)"
        [ $modified -gt 0 ] || [ $deleted -gt 0 ] && SUMMARY+=", "
    fi
    if [ $modified -gt 0 ]; then
        SUMMARY+="modified $modified file(s)"
        [ $deleted -gt 0 ] && SUMMARY+=", "
    fi
    if [ $deleted -gt 0 ]; then
        SUMMARY+="removed $deleted file(s)"
    fi
    SUMMARY+=". "
done

# Create a structured description
DESCRIPTION=$(cat << EOF
## Automated PR Description

### Summary
$SUMMARY

### Modified Files
\`\`\`
$(git diff --stat "$BASE_REF" "$SHA")
\`\`\`

> Note: This description was automatically generated. Please update it with more context if needed.
EOF
)

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