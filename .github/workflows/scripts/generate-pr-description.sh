#!/bin/bash

BASE_REF=$1
SHA=$2
GITHUB_TOKEN=$3
REPO=$4
PR_NUMBER=$5

# Get list of modified files with their types
declare -A FILE_CHANGES
while IFS= read -r line; do
    status=$(echo "$line" | cut -f1)
    file=$(echo "$line" | cut -f2)
    FILE_CHANGES["$file"]=$status
done < <(git diff --name-status "$BASE_REF" "$SHA")

# Generate contextual summary
SUMMARY=""

# Check for workflow changes
if [[ ${FILE_CHANGES[".github/workflows/ai-review.yml"]} ]]; then
    SUMMARY+="* Added AI Review workflow that uses Claude to automatically check whether pull requests follow contribution guidelines\n"
fi

if [[ ${FILE_CHANGES[".github/workflows/publish.yml"]} ]]; then
    SUMMARY+="* Added automated publishing workflow for NuGet package releases\n"
fi

if [[ ${FILE_CHANGES[".github/workflows/pr-check.yml"]} ]]; then
    SUMMARY+="* Updated pull request check workflow\n"
fi

# Check for documentation changes
if [[ ${FILE_CHANGES["README.md"]} ]] || [[ ${FILE_CHANGES["CONTRIBUTING.md"]} ]]; then
    SUMMARY+="* Updated project documentation\n"
fi

# Check for library changes
for file in "${!FILE_CHANGES[@]}"; do
    if [[ $file == GmoCoinNet/* ]]; then
        if [[ $file == *"/Schema/"* ]]; then
            SUMMARY+="* Modified API schema definitions\n"
            break
        elif [[ $file == *"/Network/"* ]]; then
            SUMMARY+="* Updated network communication handling\n"
            break
        fi
    fi
done

# Check for example changes
if [[ -n "$(echo "${!FILE_CHANGES[@]}" | grep 'GmoCoinNet.Examples')" ]]; then
    SUMMARY+="* Updated example code\n"
fi

# If no specific summary was generated, fall back to generic description
if [ -z "$SUMMARY" ]; then
    SUMMARY="* $(git diff --shortstat "$BASE_REF" "$SHA")\n"
fi

# Create a structured description
DESCRIPTION=$(cat << EOF
## Automated PR Description

### Summary
$SUMMARY

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