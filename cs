#!/bin/bash

#$HOME/.bash_profile
handle_one_result() {
  echo "I got here"
  if [ -f "$1" ]; then
    nvim "$1"
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


case $1 in 
  # fizz
  # fz -s --see -> prints the found directory
  -s | --see)
          echo "PRINTS THE FOUNDS DIRECTORY PATH"
          ;;
  #fz -l --look -> prints the directory contents
  -l | --look)
          echo "PRINTS THE DIRECTORY CONTENTS"
          ;;
 # fz -t --time -> fz does not use it's usual 1Month period
  -t | --time)
          echo "Skips 2M argument"
          ;;
 # fz -d -> if fz finds a file it still goes to directorywithout opening a file
  -d)
          echo "Goes directory into a directory even if a file is found in its place"
          ;;
  # fz -f --force --first -> if fz finds multiple targets it takes the one closest to you in path
  -f | --force | --first)
          echo "If finds multiple targets takes you to the one closest to you"
          ;;
      esac

regex_string="$2"
if [ "$regex_string" ]; then
  first_step=$(find "$PWD" -atime -62 -name "*$regex_string*" )
  echo "$first_step"
  result_count=$(echo "$first_step" | wc -l)

  if [ "$result_count" -eq 1 ]; then
    handle_one_result "$first_step"
  elif [ "$result_count" -gt 1 ]; then
    echo "$first_step" | handle_multiple_results
  fi
fi
#directory="$3"

# if fz finds more than one output it automatically prints the directory paths
  # if fz finds only one path it goes there
  # if fz finds only a file it uses nvims to it
  # if fz finds multiple directories  in the same path it will bring you to the one closest to your cwd
 #case $2 in

