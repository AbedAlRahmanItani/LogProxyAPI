using LogProxy.Api.Tests.Common;
using LogProxy.Application.CQRS.Auth.Models;
using LogProxy.Application.CQRS.Auth.Queries;
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

            var command = new CreateMessagesCommand
            {
                Messages = new List<MessagesViewModel>
                {
                    new MessagesViewModel
                    {
                        Id = "1",
                        ReceivedAt = DateTime.UtcNow,
                        Title = "Title 1.1",
                        Text = "Text 1.1"
                    },
                    new MessagesViewModel
                    {
                        Id = "1",
                        ReceivedAt = DateTime.UtcNow,
                        Title = "Title 1.2",
                        Text = "Text 1.2"
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
