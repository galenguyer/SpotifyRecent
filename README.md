# SpotifyRecent
An ASP.NET + Typescript/React web app to show my recent plays on Spotify

This was an idea I had so I could learn a few new things. There's two main components to the project.

### Backend API
The backend is written in ASP.NET. I learned how OAuth2 works and implemented a client for it from scratch. The app sends the user to the login screen, and handles the callback. After receving that token and exchanging it for an access token and refresh token, it valides that the user who logged in has the same email as set in the config. If the emails don't match, the login is rejected.
To run in Docker, first go to the SpotifyRecentApi/SpotifyRecentApi/ folder and run `docker build -f Dockerfile ..`. This generates the docker file you can run however you wish.

##### Docker Compose
Example docker-compose file to work with traefik for the backend (because that's what I use):
```
version: "3.7"

services:
  spotifyapi:
    image: spotifyrecentapi:latest # docker.galenguyer.com/chef/spotifyrecentapi:latest works as well
    container_name: spotifyapi
    restart: always
    labels:
      - "traefik.enable=true"
      - "traefik.http.routers.spotifyapi.rule=(Host(`galenguyer.com`) && PathPrefix(`/api/spotify`))"
      - "traefik.http.routers.spotifyapi.entrypoints=https"
      - "traefik.http.routers.spotifyapi.service=spotifyapi"
      - "traefik.http.routers.spotifyapi.tls=true"
      - "traefik.http.routers.spotifyapi.tls.certresolver=letsencrypt"
      - "traefik.http.services.spotifyapi.loadbalancer.server.port=28473"
    networks:
      - traefik
    volumes:
      - type: volume
        source: spotifyapi
        target: /app/files

networks:
  traefik:
    name: traefik
    driver: bridge

volumes:
  spotifyapi:
```

### Frontend Webpage
The frontend is written with Typescript and React. I've done a bit of React before, but I've never used TypeScript. While some of the stricter requirements imposed by TypeScript were frustrating at first, it's nice to ensure the that I know the type I'm passing. TypeScript allowed me to use Intellisense in VS Code, which made writing the frontend easier and safer.
