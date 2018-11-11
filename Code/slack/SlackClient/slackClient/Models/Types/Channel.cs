namespace SlackClient.Models.Types
{
    public class Channel : AbstractChannel
    {
        public bool IsChannel { get; set; }
        public bool IsMember { get; set; }
        public bool IsGeneral { get; set; }
    }
}
