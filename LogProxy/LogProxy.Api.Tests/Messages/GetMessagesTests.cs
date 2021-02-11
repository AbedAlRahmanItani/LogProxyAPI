using LogProxy.Api.Tests.Common;
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
