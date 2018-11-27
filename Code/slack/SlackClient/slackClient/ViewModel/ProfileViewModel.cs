using System;
using SlackClient.Models;
using SlackClient.Models.Response;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SlackClient.ViewModels
{
    public class ProfileViewModel : SlackPageViewModel
    {
        /// <summary>
        /// Gets or sets the update command.
        /// </summary>
        /// <value>
        /// The update command.
        /// </value>
        public ICommand UpdateCommand { protected set; get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProfileViewModel"/> class.
        /// </summary>
        /// <param name="page">The current page.</param>
        public ProfileViewModel(Page page)
        {
            this.page = page;
            UpdateCommand = new Command(Update);
            Update();
        }

        /// <summary>
        /// The current page
        /// </summary>
        private readonly Page page;

        /// <summary>
        /// The user's name
        /// </summary>
        private string _userName;

        public string UserName
        {
            get => _userName;
            set
            {
                if (_userName == value) return;
                _userName = value;
                OnPropertyChanged("UserName");
            }
        }

        /// <summary>
        /// The user's image
        /// </summary>
        private string _userImage;

        public string UserImage
        {
            get => _userImage;
            set
            {
                if (_userImage == value) return;
                _userImage = value;
                OnPropertyChanged("UserImage");
            }
        }

        /// <summary>
        /// The user's email
        /// </summary>
        private string _userEmail;

        public string UserEmail
        {
            get => _userEmail;
            set
            {
                if (_userEmail == value) return;
                _userEmail = value;
                OnPropertyChanged("UserEmail");
            }
        }

        /// <summary>
        /// The workspace URL
        /// </summary>
        private string _workspaceUrl;

        public string WorkspaceUrl
        {
            get => _workspaceUrl;
            set
            {
                if (_workspaceUrl == value) return;
                _workspaceUrl = value;
                OnPropertyChanged("WorkspaceUrl");
            }
        }

        /// <summary>
        /// The workspace team
        /// </summary>
        private string _workspaceTeam;

        public string WorkspaceTeam
        {
            get => _workspaceTeam;
            set
            {
                if (_workspaceTeam == value) return;
                _workspaceTeam = value;
                OnPropertyChanged("WorkspaceTeam");
            }
        }

        /// <summary>
        /// Updates this page.
        /// </summary>
        private async void Update()
        {
            try
            {
                await Slack.AuthTest();
                var auth = (AuthTestResponse)Slack.Response;

                await Slack.UsersList();
                var users = (UsersListResponse)Slack.Response;

                var user = users.Members.Single(x => x.Id == auth.UserId);

                UserImage = user.Profile.Image512;

                UserName = user.Profile.RealName;

                UserEmail = user.IsPrimaryOwner.ToString();

                WorkspaceUrl = auth.Url;

                WorkspaceTeam = auth.Team;
            }
            catch(SlackClientException e)
            {
                await page.DisplayAlert("Error!", e.Message, "Ok");
            }
        }
    }
}
