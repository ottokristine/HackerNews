using HackerNewsWebAPI.Core;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using System.Text.Json;
using System.Net.Http.Headers;

namespace HackerNewsWebAPI.Data
{
    public class StoryData: IStoryData
    {
        public HttpClient client { get; } 

        public StoryData(HttpClient client) { 
        
            //initialize the client
            client.BaseAddress = new Uri("https://hacker-news.firebaseio.com/v0/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            
            this.client = client;
        }

        public List<Story> GetStories()
        {
            var stories = GetStoriesAsync().Result;

            //stories without urls are useless to our application, filter those out here
            return stories.FindAll(s => s.url != null && s.url != "");
        }
        private async Task<List<Story>> GetStoriesAsync()
        {
            var stories = new List<Story>(); ;
            //api does not have option of getting array of objects, therefore need to get the ids of the new stories and built out array with their data.
            //var task = await GetNewStoriesIds();
            var newStoryIds = await GetNewStoriesIds();
            for (int i = 0; i < newStoryIds.Length; i++)
            {
                var newStory = await GetStoryForID(newStoryIds[i]);
                if (newStory != null)
                {
                    stories.Add(newStory);
                }

            }

            
            return stories;
         }

        //get a story object for a story id
        private async Task<Story> GetStoryForID(int id)
        {
            var urlString = $"item/{id}.json?print=pretty";
            var response = await client.GetAsync(urlString);
            response.EnsureSuccessStatusCode();

            using var responseStream = await response.Content.ReadAsStreamAsync();
            var newStory = await JsonSerializer.DeserializeAsync<Story>(responseStream);
            return newStory;        
        }

        //get all of the newest tory ids
        private async Task<int[]> GetNewStoriesIds()
        {
            var response = await client.GetAsync("newstories.json?print=pretty");
            response.EnsureSuccessStatusCode();

            using var responseStream = await response.Content.ReadAsStreamAsync();
            var idArray = await JsonSerializer.DeserializeAsync<int[]>(responseStream);
            return idArray;
        }

    }

}
