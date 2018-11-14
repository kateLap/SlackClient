using SlackClient.ViewModels;
using Xamarin.Forms;

namespace SlackClient.Views
{
	public partial class ChatsListPage : ContentPage
	{
		public ChatsListPage ()
		{
			InitializeComponent ();
            this.BindingContext = new ChatsListViewModel(this) { Navigation = this.Navigation };
        }
	}
}