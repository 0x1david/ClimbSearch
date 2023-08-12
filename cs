#!/bin/bash

#$HOME/.bash_profile
print_path="false"
print_ls="false"
time_edited=( -atime -62 )
force_cd="false"

handle_one_result() {
  if [ -f "$1" ]; then
    echo "ass"
    echo "$print_ls"
    if [ "$print_path" = "true" ]; then
      echo "$1"
      exit
    elif [ "$print_ls" = "true" ]; then
      ls -la "$1"
      exit
    else
      nvim "$1"
    fi
  elif [ -d "$1" ]; then
    echo "end $1"
  fi
}
handle_multiple_results() {
  current_distance=50
  while IFS= read -r result; do
   iter_distance=$(echo "$result" | tr -cd '/'| wc -c)

   if [ "$current_distance" -gt "$iter_distance" ]; then
     current_distance="$iter_distance"
     placeholder="$result"
   fi
  done
  handle_one_result "$placeholder"
 }

while getopts "sltdf" option; do
 case $option in 
  # fizz
  # fz -s --see -> prints the found directory
  s)
          echo "PRINTS THE FOUNDS DIRECTORY PATH"
          print_path="true"
          ;;
  #fz -l --look -> prints the directory contents
  l)
          echo "PRINTS THE DIRECTORY CONTENTS"
          print_ls="true"
          ;;
 # fz -t --time -> fz does not use it's usual 1Month period
  t)
          echo "Skips 2M argument"
          time_edited=()
          ;;
 # fz -d -> if fz finds a file it still goes to directorywithout opening a file
  d)
          echo "Goes directory into a directory even if a file is found in its place"
          ;;
  *)
          echo "Unknown option: -$OPTARG" >&2
          exit
  # fz -f --force --first -> if fz finds multiple targets it takes the one closest to you in path
    esac
  done

shift "$((OPTIND-1))"


if [ $# -ge 1 ]; then
  regex_string="$1"
  if [ "$regex_string" ]; then
    first_step=$(find "$PWD" "${time_edited[@]}" -name "*$regex_string*" )
    result_count=$(echo "$first_step" | wc -l)

    if [ "$result_count" -eq 1 ]; then
      handle_one_result "$first_step"
    elif [ "$result_count" -gt 1 ]; then
      echo "$first_step" | handle_multiple_results
    fi
  fi
fi
#directory="$3"

# if fz finds more than one output it automatically prints the directory paths
  # if fz finds only one path it goes there
  # if fz finds only a file it uses nvims to it
  # if fz finds multiple directories  in the same path it will bring you to the one closest to your cwd
 #case $2 in

