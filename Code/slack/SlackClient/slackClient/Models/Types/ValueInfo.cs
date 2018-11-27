using System;

namespace SlackClient.Models.Types
{
    /// <summary>
    /// The type to define the topic of the channel
    /// </summary>
    public class ValueInfo
    {
        /// <summary>
        /// The topic
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets the topic creator ID
        /// </summary>
        public string Creator { get; set; }

        /// <summary>
        /// Gets or sets the time of last set
        /// </summary>
        public DateTime LastSet { get; set; }
    }
}