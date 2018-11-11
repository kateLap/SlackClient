namespace SlackClient.Models.Response
{
    public class AuthTestResponse : SlackResponse
    {
        public string Url { get; set; }
        public string Team { get; set; }
        public string User { get; set; }
        public string TeamId { get; set; }
        public string UserId { get; set; }
    }
}