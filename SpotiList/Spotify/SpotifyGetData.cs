using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace SpotiList.Spotify
{
    public class SpotifyGetData
    {
        private HttpContext _httpContext;

        public SpotifyGetData(IHttpContextAccessor httpContextAccessor)
        {
            _httpContext = httpContextAccessor.HttpContext;
        }

        public async Task<JObject> GetSpotifyDataAsync(string url)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            string accessToken = await _httpContext.GetTokenAsync("access_token");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var httpClient = new HttpClient();
            var response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, _httpContext.RequestAborted);
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized ||
                response.StatusCode == System.Net.HttpStatusCode.Forbidden)
            {
                return null;
            }
            response.EnsureSuccessStatusCode();
            return JObject.Parse(await response.Content.ReadAsStringAsync());
        }
    }
}
