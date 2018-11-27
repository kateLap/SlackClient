namespace SlackClient.Models
{
    /// <summary>
    /// Defines the director for posting message for the pattern Builder
    /// </summary>
    public class ChatPostMessageDirector : IDirector
    {
        /// <summary>
        /// The builder
        /// </summary>
        private readonly IChatPostMessageBuilder _builder;

        /// <summary>
        /// The director
        /// </summary>
        private readonly IDirector _director;

        /// <summary>
        /// Initializes a new instance of the <see cref="ChatPostMessageDirector"/> class.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <param name="director">The director.</param>
        public ChatPostMessageDirector(IChatPostMessageBuilder builder, IDirector director = null)
        {
            _builder = builder;
            _director = director;
        }
        
        public void Make()
        {
            _director?.Make();
            _builder.BuildAsUser(true);
        }
    }
}
