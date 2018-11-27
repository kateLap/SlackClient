using System;
using System.Collections.Generic;
using System.Text;

namespace SlackClient.Models.Response
{
    public class SetTopicResponse : SlackResponse
    {
        /// <summary>
        /// Gets or sets new topic of the channel
        /// </summary>
        public string Topic { get; set; }
    }
}
