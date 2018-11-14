using System.Collections.ObjectModel;
using SlackClient.Models;
using SlackClient.Models.Response;
using SlackClient.Models.Types;
using SlackClient.Views;
using System.Windows.Input;
using Xamarin.Forms;
using System.Linq;

namespace SlackClient.ViewModels
{
    public class IMListViewModel : SlackPageViewModel
    {
        public ObservableCollection<IMMessagesListViewModel> IMs { get; set; }

        public ICommand UpdateCommand { protected set; get; }
        public INavigation Navigation { get; set; }

        IMMessagesListViewModel selectedIM;

        private Page page;

        public IMListViewModel(Page page)
        {
            this.page = page;
            IMs = new ObservableCollection<IMMessagesListViewModel>();           
            Update();
            UpdateCommand = new Command(Update);
        }

        public IMMessagesListViewModel SelectedIM
        {
            get { return selectedIM; }
            set
            {
                if (selectedIM != value)
                {
                    IMMessagesListViewModel tempChat = value;
                    tempChat.Messages.Clear();
                    tempChat.SendMessage();
                    
                    selectedIM = null;
                    OnPropertyChanged("SelectedIM");
                    Navigation.PushAsync(new IMMessagesPage(tempChat));
                }
            }
        }

        private bool isUpdating = false;
        public bool IsUpdating
        {
            get { return isUpdating; }
            set
            {
                isUpdating = value;
                OnPropertyChanged("IsUpdating");
            }
        }

        private async void Update()
        {
            try
            {
                IsUpdating = true;
                await slack.IMList();
                var channels = (IMListResponse)slack.Response;

                await slack.UsersList();
                UsersListResponse users = (UsersListResponse)slack.Response;

                IMs.Clear();

                foreach (IMChannel currentChannel in channels.Channels)
                {

                string userName;
                string image;

                var user = users.Members.Where(x => x.Id == currentChannel.User);
                {
                    userName = "";
                    image = "";
                }

                if (user.Count() != 0)
                {
                    userName = user.First().Profile.RealName;
                    image = user.First().Profile.Image72;
                }

                var newIM = new IMMessagesListViewModel(page)
                {
                    IMUser = userName,
                    IMCreatedTime = currentChannel.Created.ToString(),
                    IMId = currentChannel.Id,
                    IMUserImage = image,
                    Slack = this.slack
                };

                IMs.Add(newIM);
            }
                IsUpdating = false;
            }
            
                catch (SlackClientException e)
                {
                IsUpdating = false;
                await page.DisplayAlert("Error!", e.Message, "Ok");
                }
        }
    }
}
