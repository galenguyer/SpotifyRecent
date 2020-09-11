#!/usr/bin/env bash
# build, tag, and push docker images

# exit if a command fails
set -o errexit

# exit if required variables aren't set
set -o nounset

cd SpotifyRecentApi
docker build -f Dockerfile -t docker.galenguyer.com/chef/spotifyrecentapi:latest -t spotifyrecentapi:latest ..
docker push docker.galenguyer.com/chef/spotifyrecentapi:latest
