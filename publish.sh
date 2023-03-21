#!/bin/sh/
assets=()
for asset in ./out/*.nupkg 
do 
assets+=("$asset")
done
for asset in ./out/*.snupkg 
do 
assets+=("$asset")
done
gh release upload "$1" "${assets[@]}"