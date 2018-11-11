using System.Collections.Generic;
using Newtonsoft.Json;
using SlackClient.Models.Types;

namespace SlackClient.Models.Response
{
    public class IMListResponse : SlackResponse
    {
        [JsonProperty(PropertyName = "Ims")]
        public IEnumerable<IMChannel> Channels { get; set; }
    }
}
