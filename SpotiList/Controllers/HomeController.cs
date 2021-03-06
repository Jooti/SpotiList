﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using SpotiList.Models;
using SpotiList.Spotify;

namespace SpotiList.Controllers
{
    public class HomeController : Controller
    {
        private IRecentTracks _recentTracks { get; set; }
        private ISavedAlbums _savedAlbums { get; set; }
        private IRecommendation _recommendation { get; set; }
        private IPlaylistSaver _playlistSaver { get; set; }
        private IConfiguration _configuration { get; set; }

        public HomeController(IRecentTracks recentTracks, ISavedAlbums savedAlbums, 
            IRecommendation recommendation, IPlaylistSaver playlistSaver,
            IConfiguration configuration)
        {
            _recentTracks = recentTracks;
            _savedAlbums = savedAlbums;
            _recommendation = recommendation;
            _playlistSaver = playlistSaver;
            _configuration = configuration;
        }

        public async Task<IActionResult> Index()
        {
            if (string.IsNullOrWhiteSpace(_configuration["spotify:client_id"]) ||
                string.IsNullOrWhiteSpace(_configuration["spotify:client_secret"]))
            {
                return RedirectToAction("readme");
            }
            HomeViewModel model = new HomeViewModel();
            if (User.Identity.IsAuthenticated)
            {
                model.RecentTracks = await _recentTracks.GetTracksAsync();

                model.SavedArtists = await _savedAlbums.GetArtistsAsync();
            }
            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> Recommend(RecommendFormViewModel form)
        {
            var recommaendations = await _recommendation.GetRecommendationsAsync(form);
            return View(recommaendations);
        }

        [Authorize]
        public async Task<IActionResult> SavePlaylist(SavePlaylistFormViewModel form)
        {
            ViewData["url"] = await _playlistSaver.SaveAsync(form);
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult ReadMe()
        {
            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
