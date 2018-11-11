using System.Collections.Generic;
using SlackClient.Models.Types;

namespace SlackClient.Models.Response
{
    public class ChannelsListResponse : SlackResponse
    {
        public IEnumerable<Channel> Channels { get; set; }
    }
}
