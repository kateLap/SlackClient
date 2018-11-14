using System;
using System.Collections.Generic;
using System.Text;

namespace SlackClient.Models.Response
{
    public class SetTopicResponse : SlackResponse
    {
        public string Topic { get; set; }
    }
}
