using Xamarin.Forms;

namespace SlackClient
{
	public partial class App : Application
	{
		public App ()
		{
            MainPage = new SlackClient.Views.MainAuthPage();
        }

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
