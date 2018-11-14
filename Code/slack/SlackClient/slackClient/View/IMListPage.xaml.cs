using SlackClient.ViewModels;
using Xamarin.Forms;

namespace SlackClient.Views
{
	public partial class IMListPage : ContentPage
	{
        public IMListPage()
        {
            InitializeComponent();
            this.BindingContext = new IMListViewModel(this) { Navigation = this.Navigation };
        }
    }
}