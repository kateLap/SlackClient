using System.Collections.Generic;
using SlackClient.Models.Types;

namespace SlackClient.Models.Response
{
    public class ChannelsListResponse : SlackResponse
    {
        /// <summary>
        /// Gets or sets the list of channels in current workspace
        /// </summary>
        public IEnumerable<Channel> Channels { get; set; }
    }
}
