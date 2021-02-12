using LogProxy.Application.Interfaces.Providers;
using LogProxy.Application.Options;
using LogProxy.Application.Providers.Airtable.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LogProxy.Infrastructure.Providers
{
    public class AirtableApiClient : IAirtableApiClient
    {
        private readonly string _baseUrl;
        private readonly string _apiKey;

        public AirtableApiClient(IOptions<AirtableOptions> options)
        {
            if (options.Value == null)
                throw new ArgumentException("Airtable Options are required", nameof(options));

            _baseUrl = options.Value.BaseUrl;
            if (string.IsNullOrEmpty(_baseUrl))
                throw new ApplicationException($"The Base URL option is required");

            _apiKey = options.Value.ApiKey;
            if (string.IsNullOrEmpty(_apiKey))
                throw new ApplicationException($"The API Key option is required");
        }


        public async Task<MessagesResponse> GetMessagesAsync(GetMessagesRequest request, CancellationToken cancellationToken)
        {
            var parameters = new Dictionary<string, string>
            {
                { "maxRecords" , request.MaxRecords.ToString() },
                { "view" , request.View }
            };
            return await QueryAsync<MessagesResponse>(HttpMethod.Get, $"/Messages", cancellationToken, parameters).ConfigureAwait(false);
        }

        public async Task<MessagesResponse> CreateMessagesAsync(PostMessagesRequest request, CancellationToken cancellationToken)
        {
            return await QueryAsync<MessagesResponse>(HttpMethod.Post, $"/Messages", cancellationToken, body: request).ConfigureAwait(false);
        }

        private async Task<T> QueryAsync<T>(HttpMethod httpMethod, string function, CancellationToken cancellationToken, Dictionary<string, string> parameters = null, object body = null)
        {
            using (var httpClient = new HttpClient()
            {
                Timeout = TimeSpan.FromMinutes(15)
            })
            {
                var url = _baseUrl.TrimEnd('/') + function;

                var content = body is string str ? str : JsonConvert.SerializeObject(body);

                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + _apiKey);

                using (var requestMessage = new HttpRequestMessage(httpMethod, url))
                {
                    if (httpMethod == HttpMethod.Get)
                    {
                        if (parameters != null)
                            url = BuildQuery(url, parameters);
                        requestMessage.RequestUri = new Uri(url);
                    }
                    else
                    {
                        requestMessage.Content = new StringContent(content, Encoding.UTF8, "application/json");
                    }

                    var response = await httpClient.SendAsync(requestMessage, cancellationToken).ConfigureAwait(false);
                    var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                    if (!response.IsSuccessStatusCode)
                        throw new ApplicationException(responseContent);

                    if (typeof(T) == typeof(string))
                        return (T)(object)responseContent;

                    return JsonConvert.DeserializeObject<T>(responseContent);
                }
            }
        }

        private static string BuildQuery(string url, Dictionary<string, string> query)
        {
            var builder = new StringBuilder();
            foreach (var pair in query)
            {
                builder.Append($"{Uri.EscapeDataString(pair.Key)}={pair.Value}&");
            }

            var uriBuilder = new UriBuilder(new Uri(url));
            if (string.IsNullOrEmpty(uriBuilder.Query))
                uriBuilder.Query = builder.ToString();
            else
                uriBuilder.Query += "&" + builder.ToString();

            uriBuilder.Query = uriBuilder.Query.TrimEnd(new char[] { '&' });

            var result = uriBuilder.Uri.ToString();

            return result;
        }
    }
}
