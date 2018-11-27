using System.Collections.Generic;
using SlackClient.Models.Types;

namespace SlackClient.Models.Response
{
        public class ChannelsHistoryResponse : SlackResponse
        {
        /// <summary>
        /// Gets or sets the list of messages of current channel
        /// </summary>
        public IEnumerable<Message> Messages { get; set; }
        }
}
