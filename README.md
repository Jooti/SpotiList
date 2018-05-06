# SpotiList
SpotiList is a Playlist generator based on the recommendations api of Spotify.

## Run application
SpotiList stores client id and client secret of the Spotify in the secrets.json [(more info)](https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-2.1&tabs=visual-studio) file.
Here is the template of the secrets.json file:
```
{
    "spotify": {
      "client_id": "yourClientId_Should_be_here",
      "client_secret": "yourClientSecret_Should_be_here"
    }
}
```
