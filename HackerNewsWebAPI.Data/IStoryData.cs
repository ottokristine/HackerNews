using HackerNewsWebAPI.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HackerNewsWebAPI.Data
{
    public interface IStoryData
    {
        public List<Story> GetStories();
    }
}
