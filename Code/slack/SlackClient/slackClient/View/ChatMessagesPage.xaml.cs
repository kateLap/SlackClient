using Xamarin.Forms;
using SlackClient.ViewModels;


namespace SlackClient.Views
{
    public partial class ChatMessagesPage : ContentPage
    {
        public MessagesListViewModel ViewModel { get; private set; }
        public ChatMessagesPage(MessagesListViewModel chat)
        {
            InitializeComponent();
            this.BindingContext = new MessagesListViewModel(this);
            ViewModel = chat;
            this.BindingContext = ViewModel;
        }
    }
}