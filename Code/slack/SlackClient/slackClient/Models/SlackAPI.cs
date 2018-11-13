using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SlackClient.Models.Response;
using SlackClient.Models.Types;

namespace SlackClient.Models
{
    public class SlackAPI
    {
        public SlackResponse Response { get; private set; }

        private readonly string _token;

        private readonly HttpClient _httpClient;

        private static readonly Uri SlackApiRoot = new Uri("https://slack.com/api/");

        public static readonly JsonSerializerSettings SerializerSettings;


        public SlackAPI(string token)
        {
            _token = token;

            var handler = new HttpClientHandler()
            {
                AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip,
            };

            _httpClient = HttpClientFactory.Create(handler);

            var headers = _httpClient.DefaultRequestHeaders;
            headers.AcceptEncoding.Clear();

            headers.AcceptCharset.Clear();
            headers.AcceptCharset.ParseAdd("utf-8");

            headers.Accept.Clear();
            headers.Accept.ParseAdd("application/json");

        }

        static SlackAPI()
        {
            SerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new SlackPropertyNamesContractResolver(),         
                Formatting = Formatting.Indented,                                    
                NullValueHandling = NullValueHandling.Ignore,                           
            };
            SerializerSettings.Converters.Add(new StringEnumConverter());
            SerializerSettings.Converters.Add(new EpochDateTimeConverter());      
        }


        private async Task Request<T>(string methodName, params KeyValuePair<string, string>[] args)
            where T : SlackResponse
        {
            var url = new Uri(SlackApiRoot, $"{methodName}?token={Uri.EscapeUriString(_token)}");

            var request = new HttpRequestMessage(HttpMethod.Post, url);

            if (args != null && args.Length > 0)
            {
                request.Content = new FormUrlEncodedContent(args);
            }

            SlackResponse resInfo;
            try
            {
                var response = await _httpClient.SendAsync(request).ConfigureAwait(false);
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                resInfo = JsonConvert.DeserializeObject<T>(content, SerializerSettings);
            }
            catch (Exception e)
            {
                throw new SlackClientException("Error during the HTTP request process", e);
            }
            Response = resInfo;
        }
       
        protected static KeyValuePair<string, string> Pair(string key, object value)
        {
            return Pair(key, value.ToString());
        }

        protected static KeyValuePair<string, string> Pair(string key, string value)
        {
            return new KeyValuePair<string, string>(key, value);
        }
      
        public async Task AuthTest()
        {
            await Request<AuthTestResponse>("auth.test");
        }

        public async Task ChannelsHistory(string channelId)
        {
            await Request<ChannelsHistoryResponse>("channels.history", Pair("channel", channelId));
        }

        public async Task ChannelsList()
        {
            await ChannelsList(false);
        }

        public async Task ChannelsList(bool excludeArchived)
        {
            await Request<ChannelsListResponse>("channels.list", Pair("exclude_archived", excludeArchived ? 1 : 0));
        }

        public async Task ChannelsKick(string channelId, string userId)
        {
            await Request<SlackResponse>("channels.kick", Pair("channel", channelId), Pair("user", userId));
        }

        public async Task UsersList()
        {
            await Request<UsersListResponse>("users.list");
        }

        public async Task IMList()
        {
            await Request<IMListResponse>("im.list");
        }

        public async Task IMHistory(string channelId)
        {
            await Request<ChannelsHistoryResponse>("im.history", Pair("channel", channelId));
        }

        public async Task Send(IEnumerable<KeyValuePair<string, string>> model)
        {
            await Request<ChatPostMessageResponse>("chat.postMessage", model.ToArray());
        }

        // TODO: Реализовать шаблон Строитель.
        public async Task ChatPostMessage(
         string channelId,
         string text,
         string username = null,
         bool? asUser = null,
         bool? linkNames = null,
         bool? unfurlLinks = null,
         bool? unfurlMedia = null,
         string iconUrl = null,
         string iconEmoji = null)
        {

            var args = new List<KeyValuePair<string, string>>
            {
                // Required arguments
                Pair("channel", channelId),
                Pair("text", text)
            };

            // Optional arguments
            if (!string.IsNullOrWhiteSpace(username))
            {
                args.Add(Pair("username", username));
            }

            if (asUser != null)
            {
                args.Add(Pair("as_user", asUser));
            }

            if (linkNames != null)
            {
                args.Add(Pair("link_names", linkNames == true ? "1" : "0"));
            }

            if (unfurlLinks != null)
            {
                args.Add(Pair("unfurl_links", unfurlLinks));
            }

            if (unfurlMedia != null)
            {
                args.Add(Pair("unfurl_media", unfurlLinks));
            }

            if (!string.IsNullOrWhiteSpace(iconUrl))
            {
                args.Add(Pair("icon_url", iconUrl));
            }

            if (!string.IsNullOrWhiteSpace(iconEmoji))
            {
                args.Add(Pair("icon_emoji", iconEmoji));
            }

            await Request<ChatPostMessageResponse>("chat.postMessage", args.ToArray());
        }
    }
}
