using Posts.Filter;
using Posts.Model;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Posts.Tests
{
    public class FiltererTest
    {
        private readonly IFilterer<Post> _filterer;

        public FiltererTest()
        {
            _filterer = new Filterer();
        }

        [Fact]
        public void Filter_WithIds_ReturnsOnlyPostsWithIds()
        {
            var ids = new[] { 1, 3, 5 };
            var posts = new List<Post>
            {
                new Post {Id = 1, UserId = 1, Body = "Body1", Title = "Title1"},
                new Post {Id = 2, UserId = 2, Body = "Body2", Title = "Title2"},
                new Post {Id = 3, UserId = 3, Body = "Body3", Title = "Title3"}
            };

            var filteredPosts = _filterer.Filter(posts, ids).ToList();

            var expectedPosts = new List<Post>
            {
                new Post {Id = 1, UserId = 1, Body = "Body1", Title = "Title1"},
                new Post {Id = 3, UserId = 3, Body = "Body3", Title = "Title3"}
            };

            Assert.Equal(expectedPosts, filteredPosts);
        }
    }
}