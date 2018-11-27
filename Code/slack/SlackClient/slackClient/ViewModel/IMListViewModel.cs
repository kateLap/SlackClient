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
        /// <summary>
        /// Gets or sets the IMs list.
        /// </summary>
        /// <value>
        /// The i ms.
        /// </value>
        public ObservableCollection<IMMessagesListViewModel> IMs { get; set; }

        /// <summary>
        /// Gets or sets the update command.
        /// </summary>
        /// <value>
        /// The update command.
        /// </value>
        public ICommand UpdateCommand { protected set; get; }

        /// <summary>
        /// Gets or sets the navigation of this app.
        /// </summary>
        /// <value>
        /// The navigation.
        /// </value>
        public INavigation Navigation { get; set; }

        /// <summary>
        /// The selected im dialog
        /// </summary>
        IMMessagesListViewModel selectedIM;

        /// <summary>
        /// The current page
        /// </summary>
        private readonly Page _page;

        /// <summary>
        /// Initializes a new instance of the <see cref="IMListViewModel"/> class.
        /// </summary>
        /// <param name="page">The page.</param>
        public IMListViewModel(Page page)
        {
            this._page = page;
            IMs = new ObservableCollection<IMMessagesListViewModel>();           
            Update();
            UpdateCommand = new Command(Update);
        }

        /// <summary>
        /// Gets or sets the selected im.
        /// </summary>
        /// <value>
        /// The selected im.
        /// </value>
        public IMMessagesListViewModel SelectedIM
        {
            get => selectedIM;
            set
            {
                if (selectedIM == value) return;
                var tempChat = value;
                tempChat.Messages.Clear();
                tempChat.SendMessage();
                    
                selectedIM = null;
                OnPropertyChanged("SelectedIM");
                Navigation.PushAsync(new IMMessagesPage(tempChat));
            }
        }

        /// <summary>
        /// The updating page flag
        /// </summary>
        private bool _isUpdating = false;

        public bool IsUpdating
        {
            get => _isUpdating;
            set
            {
                _isUpdating = value;
                OnPropertyChanged("IsUpdating");
            }
        }

        /// <summary>
        /// Updates this page.
        /// </summary>
        private async void Update()
        {
            try
            {
                IsUpdating = true;
                await Slack.IMList();
                var channels = (IMListResponse)Slack.Response;

                await Slack.UsersList();
                var users = (UsersListResponse)Slack.Response;

                IMs.Clear();

                foreach (var currentChannel in channels.Channels)
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

                var newIM = new IMMessagesListViewModel(_page)
                {
                    ImUser = userName,
                    ImCreatedTime = currentChannel.Created.ToString(),
                    IMId = currentChannel.Id,
                    IMUserImage = image,
                    Slack = this.Slack
                };

                IMs.Add(newIM);
            }
                IsUpdating = false;
            }
            
                catch (SlackClientException e)
                {
                IsUpdating = false;
                await _page.DisplayAlert("Error!", e.Message, "Ok");
                }
        }
    }
}
