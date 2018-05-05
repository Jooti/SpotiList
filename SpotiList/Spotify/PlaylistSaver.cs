using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using SpotiList.Models;

namespace SpotiList.Spotify
{
    public class PlaylistSaver : IPlaylistSaver
    {
        private HttpContext _httpContext;

        public PlaylistSaver(IHttpContextAccessor httpContextAccessor)
        {
            _httpContext = httpContextAccessor.HttpContext;
        }

        public async Task<string> SaveAsync(SavePlaylistFormViewModel form)
        {
            var userId = _httpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

            
            JObject playlistCreateResult = await PostRequest(
                JObject.FromObject(new { name = form.Name }), 
                $"https://api.spotify.com/v1/users/{userId}/playlists");
            var playlistId = playlistCreateResult["id"].ToString();
            var playlistUrl = playlistCreateResult["external_urls"]["spotify"].ToString();

            JObject playlistAddTracksResult = await PostRequest(
                JObject.FromObject(new { uris = form.GenerateSpotifyUris() }), 
                $"https://api.spotify.com/v1/users/{userId}/playlists/{playlistId}/tracks");

            return playlistUrl;
        }

        private async Task<JObject> PostRequest(JObject content, string url)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            string accessToken = await _httpContext.GetTokenAsync("access_token");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            request.Content = new StringContent(
                content.ToString(), Encoding.UTF8, "application/json");
            var httpClient = new HttpClient();
            var response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, _httpContext.RequestAborted);

            response.EnsureSuccessStatusCode();
            var result = JObject.Parse(await response.Content.ReadAsStringAsync());
            return result;
        }
    }
}
