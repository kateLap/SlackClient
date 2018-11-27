using System;
using SlackClient.Models.Types;
using Newtonsoft.Json;

namespace SlackClient.Models.Response
{
    class ChatPostMessageResponse : SlackResponse
    {
        /// <summary>
        /// Gets or sets the timestamp of sending message
        /// </summary>
        [JsonProperty(PropertyName = "ts")]
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Gets or sets the channel ID where the message sent
        /// </summary>
        public string Channel { get; set; }

        /// <summary>
        /// Gets or sets the message that was sent
        /// </summary>
        public Message Message { get; set; }
    }
}
