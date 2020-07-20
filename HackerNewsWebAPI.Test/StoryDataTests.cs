using HackerNewsWebAPI.Core;
using HackerNewsWebAPI.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit.Sdk;

namespace HackerNewsWebAPI.Test
{
    [TestClass]
    public class StoryDataTests
    {
        private IStoryData _storyData;

        //before each test we want to set up the storyDataObject
        [TestInitialize]
        public void SetUp() {
        }
        [TestMethod]
        public async Task ShouldReturnStories()
        {
            //here we will connent to the actial web service
            var httpClient = new HttpClient();
            this._storyData = new StoryData(httpClient);


            //assuming here that stories should be returned, if it is null either connection is broken or API has been flushed. Either way, renders our application useless and should be investigated.
            //this connects the to the real api and checks out that connection
            var stories = this._storyData.GetStories();
            Assert.IsNotNull(stories, "The amount of stories was null");
            Assert.IsTrue(stories.Count > 0, "The amount of stories was not greater than 0");
        }

        public void ShouldFilterStoriesWithoutUrl()
        {
            var fakeStory = new Story
            {
                by = "kotto",
                descendants = 10,
                id = 1,
                score = 10,
                time = 10000,
                title = "Fake Mock Title",
                type = "Article",
                url = "http://test.com"
            };

            var fakeStoryNullUrl = new Story
            {
                by = "kotto",
                descendants = 10,
                id = 2,
                score = 10,
                time = 10000,
                title = "Fake Mock Title No URL",
                type = "Article",
                url = null
            };

            var fakeStoryBlankUrl = new Story
            {
                by = "kotto",
                descendants = 10,
                id = 3,
                score = 10,
                time = 10000,
                title = "Fake Mock Title Blank URL",
                type = "Article",
                url = ""
            };

            //create a list and insert the items with the correct url, null url and black url
            var fakeStoryList = new List<Story>();;
            fakeStoryList.Add(fakeStory);
            fakeStoryList.Add(fakeStoryNullUrl);
            fakeStoryList.Add(fakeStoryBlankUrl);

            //when the storydata service is used, return fake array 
            var storyDataServiceMock = new Mock<IStoryData>();
            storyDataServiceMock.Setup(x => x.GetStories()).Returns(fakeStoryList);

        }
    }
}