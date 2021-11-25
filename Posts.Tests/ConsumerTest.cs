using Moq;
using Moq.Protected;
using Posts.Configuration;
using Posts.Consume;
using Posts.Model;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Posts.Tests
{
    public class ConsumerTest
    {
        [Fact]
        public async Task ConsumeAsync_WithGetAsync_GetsModelCorrectly()
        {
            // Arrange
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(@"[
                    {""id"": 1, ""userId"": 1, ""body"": ""Body1"", ""title"": ""Title1""},
                    {""id"": 2, ""userId"": 2, ""body"": ""Body2"", ""title"": ""Title2""}
                ]")
            };

            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(response);

            var httpClient = new HttpClient(mockHttpMessageHandler.Object);
            var config = new Config { EndpointUrl = "https://any.valid.domain" };
            var consumer = new Consumer(httpClient, config);

            // Act
            var posts = await consumer.ConsumeAsync();

            // Assert
            Assert.NotNull(posts);

            var expectedPosts = new[]
            {
                new Post {Id = 1, UserId = 1, Body = "Body1", Title = "Title1"},
                new Post {Id = 2, UserId = 2, Body = "Body2", Title = "Title2"}
            };
            Assert.Equal(expectedPosts, posts);

            mockHttpMessageHandler.Protected()
                .Verify("SendAsync", Times.Exactly(1), ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get), ItExpr.IsAny<CancellationToken>());
        }
    }
}