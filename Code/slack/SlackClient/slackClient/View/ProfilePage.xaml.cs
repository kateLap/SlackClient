using SlackClient.ViewModels;
using Xamarin.Forms;

namespace SlackClient.Views
{
	public partial class ProfilePage : ContentPage
	{
		public ProfilePage ()
		{
			InitializeComponent ();
            this.BindingContext = new ProfileViewModel(this);
        }
	}
}