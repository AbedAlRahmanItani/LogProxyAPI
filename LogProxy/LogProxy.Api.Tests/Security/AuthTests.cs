﻿using LogProxy.Api.Tests.Common;
using LogProxy.Application.CQRS.Auth.Models;
using LogProxy.Application.CQRS.Auth.Queries;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace LogProxy.Api.Tests.Security
{
    [Collection("Integration")]
    public class AuthTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public AuthTests(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GivenLoginRequest_WhenAuth_ThenAPostReturnsTheJwtToken()
        {
            var query = new GetAuthenticationTokenQuery
            {
                UserName = "abed.itani",
                Password = "123456"
            };

            AuthenticationToken authenticationToken;
            using (var postAuthResponse = await _client.PostAsJsonAsync($"/api/Auth", query))
            {
                Assert.True(postAuthResponse.IsSuccessStatusCode);
                authenticationToken = await postAuthResponse.Content.ReadAsAsync<AuthenticationToken>();

                Assert.NotEmpty(authenticationToken.Token);
            }
        }

        [Fact]
        public async Task GivenLoginRequest_WhenAuth_ThenAPostReturnsErrorWhenUsernameIsWrong()
        {
            var query = new GetAuthenticationTokenQuery
            {
                UserName = "abed.itani.1",
                Password = "123456"
            };

            using (var postAuthResponse = await _client.PostAsJsonAsync($"/api/Auth", query))
            {
                Assert.False(postAuthResponse.IsSuccessStatusCode);
                Assert.Equal(HttpStatusCode.InternalServerError, postAuthResponse.StatusCode);
            }
        }

        [Fact]
        public async Task GivenLoginRequest_WhenAuth_ThenAPostReturnsErrorWhenPasswordIsWrong()
        {
            var query = new GetAuthenticationTokenQuery
            {
                UserName = "abed.itani",
                Password = "123457"
            };

            using (var postAuthResponse = await _client.PostAsJsonAsync($"/api/Auth", query))
            {
                Assert.False(postAuthResponse.IsSuccessStatusCode);
                Assert.Equal(HttpStatusCode.InternalServerError, postAuthResponse.StatusCode);
            }
        }
    }
}
