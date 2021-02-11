using LogProxy.Api.Tests.Common;
using LogProxy.Application.CQRS.Messages.Commands;
using LogProxy.Application.CQRS.Messages.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace LogProxy.Api.Tests.Messages
{
    [Collection("Integration")]
    public class PostMessagesTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public PostMessagesTests(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GivenMessageRequest_WhenPost_ThenAPostReturnsThePostedMessages()
        {
            var command = new CreateMessagesCommand
            {
                Messages = new List<MessagesViewModel>
                {
                    new MessagesViewModel
                    {
                        Id = 2,
                        ReceivedAt = DateTime.UtcNow,
                        Title = "Title 2",
                        Text = "Text 2"
                    },
                    new MessagesViewModel
                    {
                        Id = 3,
                        ReceivedAt = DateTime.UtcNow,
                        Title = "Title 3",
                        Text = "Text 3"
                    }
                }
            };

            List<MessagesViewModel> messages;
            using (var postMessagesResponse = await _client.PostAsJsonAsync($"/api/Messages", command))
            {
                Assert.True(postMessagesResponse.IsSuccessStatusCode);
                messages = await postMessagesResponse.Content.ReadAsAsync<List<MessagesViewModel>>();

                Assert.True(messages.Count == command.Messages.Count);
            }
        }
    }
}
