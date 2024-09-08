#!/bin/sh
echo -ne '\033c\033]0;PongClone\a'
base_path="$(dirname "$(realpath "$0")")"
"$base_path/PongClone.x86_64" "$@"
