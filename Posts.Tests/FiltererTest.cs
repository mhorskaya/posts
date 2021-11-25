using System.Collections.Generic;
using System.Linq;
using Posts.Filterer;
using Posts.Models;
using Xunit;

namespace Posts.Tests
{
    public class FiltererTest
    {
        private readonly IFilterer<Post> _filterer;

        public FiltererTest()
        {
            _filterer = new Filterer.Filterer();
        }

        [Fact]
        public void Filter_WithIds_ReturnsOnlyPostsWithIds()
        {
            // Arrange
            var posts = new List<Post>
            {
                new Post {Id = 1, UserId = 1, Body = "Body1", Title = "Title1"},
                new Post {Id = 2, UserId = 2, Body = "Body2", Title = "Title2"},
                new Post {Id = 3, UserId = 3, Body = "Body3", Title = "Title3"}
            };
            var ids = new List<int> { 1, 3, 5 };

            // Act
            var filteredPosts = _filterer.Filter(posts, ids);

            // Assert
            Assert.True(filteredPosts.All(p => ids.Contains(p.Id)));
        }
    }
}