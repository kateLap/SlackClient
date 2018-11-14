using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SlackClient.Views
{
	public partial class MainAuthPage : MasterDetailPage
	{
		public MainAuthPage ()
		{
			InitializeComponent ();

            Detail = new NavigationPage(new ProfilePage());

            IsPresented = true;
		}

        private void ButtonDirect(object sender, EventArgs e)
        {
            IsPresented = false;
            Detail = new NavigationPage(new IMListPage());
        }

        private void ButtonChannels(object sender, EventArgs e)
        {
            IsPresented = false;
            Detail = new NavigationPage(new ChatsListPage());
        }

        private void ButtonProfile(object sender, EventArgs e)
        {
            IsPresented = false;
            Detail = new NavigationPage(new ProfilePage());
        }

	    private void ButtonUsers(object sender, EventArgs e)
	    {
	        IsPresented = false;
	        Detail = new NavigationPage(new UsersListPage());
	    }

	    private void ButtonEditTopic(object sender, EventArgs e)
	    {
	        IsPresented = false;
	        Detail = new NavigationPage(new ChannelsListPage());
	    }
    }
}