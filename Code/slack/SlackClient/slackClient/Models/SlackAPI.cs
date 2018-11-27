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
    public class SlackApi
    {
        /// <summary>
        /// Gets the response of the HTTP request
        /// </summary>
        public SlackResponse Response { get; private set; }

        /// <summary>
        /// The token of user's profile
        /// </summary>
        private readonly string _token;

        /// <summary>
        /// The HTTP client for requests
        /// </summary>
        private readonly HttpClient _httpClient;

        /// <summary>
        /// The slack API base URL address
        /// </summary>
        private static readonly Uri SlackApiRoot = new Uri("https://slack.com/api/");

        /// <summary>
        /// The serializer settings
        /// </summary>
        public static readonly JsonSerializerSettings SerializerSettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="SlackApi"/> class.
        /// </summary>
        /// <param name="token">The user's token</param>
        public SlackApi(string token)
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

        /// <summary>
        /// Static constructor which initializes the <see cref="SlackApi"/> class.
        /// </summary>
        static SlackApi()
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

        /// <summary>
        /// The main method which make HTTP request
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="args">The arguments.</param>
        /// <returns></returns>
        /// <exception cref="SlackClientException">Error during the HTTP request process</exception>
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

        /// <summary>
        /// Authentication test which returns information of the current workspace
        /// </summary>
        public async Task AuthTest()
        {
            await Request<AuthTestResponse>("auth.test");
        }

        /// <summary>
        /// Gets the list of channels messages
        /// </summary>
        public async Task ChannelsHistory(string channelId)
        {
            await Request<ChannelsHistoryResponse>("channels.history", Pair("channel", channelId));
        }

        /// <summary>
        /// Get a channels list
        /// </summary>
        public async Task ChannelsList()
        {
            await ChannelsList(false);
        }

        /// <summary>
        /// Get a channels list including excludeArchived parameter
        /// </summary>
        public async Task ChannelsList(bool excludeArchived)
        {
            await Request<ChannelsListResponse>("channels.list", Pair("exclude_archived", excludeArchived ? 1 : 0));
        }

        /// <summary>
        /// Kicks user from the channel
        /// </summary>
        /// <param name="channelId">The channel identifier.</param>
        /// <param name="userId">The user identifier.</param>
        public async Task ChannelsKick(string channelId, string userId)
        {
            await Request<SlackResponse>("channels.kick", Pair("channel", channelId), Pair("user", userId));
        }

        /// <summary>
        /// Gets users list of the workspace
        /// </summary>
        public async Task UsersList()
        {
            await Request<UsersListResponse>("users.list");
        }

        /// <summary>
        /// Gets list of IMs messaged
        /// </summary>
        public async Task IMList()
        {
            await Request<IMListResponse>("im.list");
        }

        /// <summary>
        /// Gets the list of IMs messages
        /// </summary>
        /// <param name="channelId">The channel identifier.</param>
        public async Task IMHistory(string channelId)
        {
            await Request<ChannelsHistoryResponse>("im.history", Pair("channel", channelId));
        }

        /// <summary>
        /// Sets topic of a channel.
        /// </summary>
        /// <param name="channelId">The channel identifier.</param>
        /// <param name="topic">The topic.</param>
        public async Task ChannelsSetTopic(string channelId, string topic)
        {
            await Request<SetTopicResponse>("channels.setTopic", Pair("channel", channelId), Pair("topic", topic));
        }

        public async Task Send(IEnumerable<KeyValuePair<string, string>> model)
        {
            await Request<ChatPostMessageResponse>("chat.postMessage", model.ToArray());
        }

        /// <summary>
        /// Sends message to a channel or individually to a user
        /// </summary>
        /// <param name="channelId">The channel identifier.</param>
        /// <param name="text">The text.</param>
        /// <param name="username">The username.</param>
        /// <param name="asUser">As user.</param>
        /// <param name="linkNames">The link names.</param>
        /// <param name="unfurlLinks">The unfurl links.</param>
        /// <param name="unfurlMedia">The unfurl media.</param>
        /// <param name="iconUrl">The icon URL.</param>
        /// <param name="iconEmoji">The icon emoji.</param>
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
                Pair("channel", channelId),
                Pair("text", text)
            };

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
