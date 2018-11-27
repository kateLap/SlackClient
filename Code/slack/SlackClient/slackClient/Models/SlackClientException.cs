using System;
using SlackClient.Models.Response;

namespace SlackClient.Models
{
    /// <summary>
    /// Defines Slack client exception
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class SlackClientException : Exception
    {

        public SlackClientException()
        {
        }

        public SlackClientException(string message) : base(message)
        {
        }

        public SlackClientException(string message, Exception innerException) : base(message, innerException)
        {
        }      

        public SlackResponse Response { get; set; }
    }
}
