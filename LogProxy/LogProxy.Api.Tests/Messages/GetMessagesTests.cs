using LogProxy.Api.Tests.Common;
using LogProxy.Application.CQRS.Auth.Models;
using LogProxy.Application.CQRS.Auth.Queries;
using LogProxy.Application.CQRS.Messages.Models;
using LogProxy.Application.CQRS.Messages.Queries;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace LogProxy.Api.Tests.Messages
{
    [Collection("Integration")]
    public class GetMessagesTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public GetMessagesTests(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GivenMessageRequest_WhenGet_ThenAGetReturnsSuccessIfAtLeastOneMessageExists()
        {
            var authQuery = new GetAuthenticationTokenQuery
            {
                UserName = "abed.itani",
                Password = "123456"
            };

            AuthenticationToken authenticationToken;
            using (var postAuthResponse = await _client.PostAsJsonAsync($"/api/Auth", authQuery))
            {
                Assert.True(postAuthResponse.IsSuccessStatusCode);
                authenticationToken = await postAuthResponse.Content.ReadAsAsync<AuthenticationToken>();

                Assert.NotEmpty(authenticationToken.Token);
            }

            _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {authenticationToken.Token}");

            var query = new GetMessagesQuery
            {
                MaxRecords = 3,
                View = "Grid view"
            };

            List<MessagesViewModel> messages;
            using (var getMessagesResponse = await _client.GetAsync($"/api/Messages?maxRecords={query.MaxRecords}&view={query.View}"))
            {
                Assert.True(getMessagesResponse.IsSuccessStatusCode);
                messages = await getMessagesResponse.Content.ReadAsAsync<List<MessagesViewModel>>();

                Assert.True(messages.Any());
            }
        }
    }
}
