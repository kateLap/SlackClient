namespace SlackClient.Models.Response
{
    public class AuthTestResponse : SlackResponse
    {
        /// <summary>
        /// The URL address of the current channel
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Defines the team of the current channel
        /// </summary>
        public string Team { get; set; }

        /// <summary>
        /// Profile name from which the user is authorized
        /// </summary>
        public string User { get; set; }

        /// <summary>
        /// ID of the current team
        /// </summary>
        public string TeamId { get; set; }

        /// <summary>
        /// Profile ID from which the user is authorized
        /// </summary>
        public string UserId { get; set; }
    }
}