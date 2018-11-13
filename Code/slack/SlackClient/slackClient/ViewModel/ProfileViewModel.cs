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
        private string userName;
        private string userImage;
        private string userEmail;
        private string workspaceUrl;
        private string workspaceTeam;

        private Page page;

        public string UserName
        {
            get { return userName; }
            set
            {
                if (userName != value)
                {
                    userName = value;
                    OnPropertyChanged("UserName");
                }
            }
        }

        public string UserImage
        {
            get { return userImage; }
            set
            {
                if (userImage != value)
                {
                    userImage = value;
                    OnPropertyChanged("UserImage");
                }
            }
        }

        public string UserEmail
        {
            get { return userEmail; }
            set
            {
                if (userEmail != value)
                {
                    userEmail = value;
                    OnPropertyChanged("UserEmail");
                }
            }
        }

        public string WorkspaceUrl
        {
            get { return workspaceUrl; }
            set
            {
                if (workspaceUrl != value)
                {
                    workspaceUrl = value;
                    OnPropertyChanged("WorkspaceUrl");
                }
            }
        }

        public string WorkspaceTeam
        {
            get { return workspaceTeam; }
            set
            {
                if (workspaceTeam != value)
                {
                    workspaceTeam = value;
                    OnPropertyChanged("WorkspaceTeam");
                }
            }
        }

        public ICommand UpdateCommand { protected set; get; }

        public ProfileViewModel(Page page)
        {
            this.page = page;
            UpdateCommand = new Command(Update);
            Update();
        }

        private async void Update()
        {
            try
            {
                await slack.AuthTest();
                var auth = (AuthTestResponse)slack.Response;

                await slack.UsersList();
                var users = (UsersListResponse)slack.Response;

                var user = users.Members.Where(x => x.Id == auth.UserId).Single();
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
