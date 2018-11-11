using System;
using SlackClient.Models.Types;
using Newtonsoft.Json;

namespace SlackClient.Models.Response
{
    class ChatPostMessageResponse : SlackResponse
    {
        [JsonProperty(PropertyName = "ts")]
        public DateTime Timestamp { get; set; }
        public string Channel { get; set; }
        public Message Message { get; set; }
    }
}
