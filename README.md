# ClimbSearch

ClimbSearch is a powerful bash script that aids in navigating the directory tree on Linux. By looking through higher hierarchical directories, it finds directories or files that match your input and then takes action based on the type of match.

## Features

- **Directory Matching**: If the script matches a directory, it automatically changes your current directory to the matched directory.
- **File Matching**: If a file is matched, it's opened in vim for editing or viewing.
- **Hierarchy Navigation**: Looks through parent directories recursively to find matches.

## Installation TBD


## Usage

To use ClimbSearch, simply run: ***cs -lsdxvds [pattern]***
Replace `[pattern]` with the name or pattern of the directory or file you're searching for.

## Example

Suppose you're in the directory `/home/user/documents` and you want to quickly move to the `/home/user/documents/projects/2023` directory or open a file named `2023.md` if it exists.

Just run: ***cs 2023***

If `2023` is a directory, your current directory will change to `/home/user/documents/projects/2023/`. If `2023.md` is a file in one of the parent directories, it will open in vim.

## Dependencies

- **Vim**

## Contributing

Feel free to open issues or PRs if you have suggestions, improvements, or fixes.



