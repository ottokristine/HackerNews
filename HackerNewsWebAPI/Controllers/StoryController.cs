using System;
using System.Collections.Generic;
using HackerNewsWebAPI.Core;
using HackerNewsWebAPI.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace HackerNewsWebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StoryController : ControllerBase
    {
        //why is this public? For testing purposes, with more time would mock memory cache but with all of the extension methods is difficult to do.
        public readonly IStoryData _storyData;
        private readonly IMemoryCache _memoryCache;

        public StoryController(IStoryData storyData, IMemoryCache memoryCache)
        {
            this._storyData = storyData;
            this._memoryCache = memoryCache;

        }

        //added into the speed by introducing response caching as well
        [ResponseCache(Duration = 3600)]
        public IActionResult Get()
        { 
            var newStories = new List<Story>();
            //if the data exists in cache, return that. Otherwise query the api
            //if making a large application, I would want to make an Enum that tracks my cache keys. However, for this smaller application seems like overkill.
            if (!_memoryCache.TryGetValue("NewStories", out newStories))
            {
                if (newStories == null)
                {
                    newStories = _storyData.GetStories();
                }
                //here I'm assuming the api will be updated. Therefore, the cached value will expire in 30 minutes.
                _memoryCache.Set("NewStories", newStories, DateTimeOffset.UtcNow.AddMinutes(30));
            }
            return Ok(newStories);
        }

    }
}