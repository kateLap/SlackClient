using SlackClient.ViewModels;
using Xamarin.Forms;

namespace SlackClient.Views
{
	public partial class IMMessagesPage : ContentPage
	{
        public IMMessagesListViewModel ViewModel { get; private set; }
        public IMMessagesPage(IMMessagesListViewModel chat)
        {
            InitializeComponent();
            this.BindingContext = new IMMessagesListViewModel(this);
            ViewModel = chat;
            this.BindingContext = ViewModel;
        }
    }
}