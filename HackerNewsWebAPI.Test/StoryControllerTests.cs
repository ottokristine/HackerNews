using HackerNewsWebAPI.Controllers;
using HackerNewsWebAPI.Core;
using HackerNewsWebAPI.Data;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace HackerNewsWebAPI.Test
{
    [TestClass]
    public class StoryControllerTests
    {
        private StoryController controller;
        private List<Story> fakeStoryList = new List<Story>();

        //before each test we want to set up the controller
        [TestInitialize]
        public void SetUp()
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

            //clear out the fake list and add in new fake story
            fakeStoryList = new List<Story>();
            fakeStoryList.Add(fakeStory);

            //when the storydata service is used, return fake array 
            var storyDataServiceMock = new Mock<IStoryData>();
            storyDataServiceMock.Setup(x => x.GetStories()).Returns(fakeStoryList);

            var memoryCacheMock = new Mock<IMemoryCache>();

            controller = new StoryController(storyDataServiceMock.Object, memoryCacheMock.Object);

        }
        [TestMethod]
        public void ControllerShouldReturnStories()
        {
            var stories = controller._storyData.GetStories();

            Assert.IsNotNull(stories, "return is null");
            Assert.AreEqual(stories[0].id, 1, "id of item in stories list is incorrect");
            

        }
    }
}
