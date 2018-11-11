using System.Collections.Generic;
using SlackClient.Models.Types;

namespace SlackClient.Models.Response
{
        public class ChannelsHistoryResponse : SlackResponse
        {
            public IEnumerable<Message> Messages { get; set; }
        }
}
