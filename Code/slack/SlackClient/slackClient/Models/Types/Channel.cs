namespace SlackClient.Models.Types
{
    public class Channel : AbstractChannel
    {
        /// <summary>
        ///  Will be true if this channel has a channel type
        /// </summary>
        public bool IsChannel { get; set; }

        /// <summary>
        ///  Will be true if the calling member is part of the channel
        /// </summary>
        public bool IsMember { get; set; }

        /// <summary>
        ///  Will be true if this channel is the "general" channel that includes
        ///  all regular team members. In most teams this is called #general but some teams
        ///  have renamed it.
        /// </summary>
        public bool IsGeneral { get; set; }
    }
}
