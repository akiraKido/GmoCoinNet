#!/bin/bash

BASE_REF=$1
SHA=$2
GITHUB_TOKEN=$3
REPO=$4
PR_NUMBER=$5

# Get the diff and format it into a description
DIFF_SUMMARY=$(git diff --stat "$BASE_REF" "$SHA")
DETAILED_CHANGES=$(git diff --unified=0 "$BASE_REF" "$SHA" | grep '^[+-]' | grep -v '^[+-]\{3\}')

# Create a structured description
DESCRIPTION=$(cat << 'EOF'
## Automated PR Description

### Files Changed
\`\`\`
${DIFF_SUMMARY}
\`\`\`

### Detailed Changes
\`\`\`diff
${DETAILED_CHANGES}
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