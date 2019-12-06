# SpotifyRecent
An ASP.NET + Typescript/React web app to show my recent plays on Spotify

This was an idea I had so I could learn a few new things. There's two main components to the project.

### Backend API
The backend is written in ASP.NET. I learned how OAuth2 works and implemented a client for it from scratch. The app sends the user to the login screen, and handles the callback. After receving that token and exchanging it for an access token and refresh token, it valides that the user who logged in has the same email as set in the config. If the emails don't match, the login is rejected.

### Frontend Webpage
The frontend is written with Typescript and React. I've done a bit of React before, but I've never used TypeScript. While some of the stricter requirements imposed by TypeScript were frustrating at first, it's nice to ensure the that I know the type I'm passing. TypeScript allowed me to use Intellisense in VS Code, which made writing the frontend easier and safer.
