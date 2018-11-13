namespace SlackClient.Models
{
    public class ChatPostMessageDirector : IDirector
    {
        private readonly IChatPostMessageBuilder _builder;

        private readonly IDirector _director;

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
