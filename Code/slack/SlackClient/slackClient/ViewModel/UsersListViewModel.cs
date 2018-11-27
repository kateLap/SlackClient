using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using SlackClient.Models;
using SlackClient.Models.Response;
using SlackClient.Models.Types;
using SlackClient.Views;
using Xamarin.Forms;

namespace SlackClient.ViewModels
{
    public class UsersListViewModel : SlackPageViewModel
    {
        /// <summary>
        /// Gets or sets the users list.
        /// </summary>
        /// <value>
        /// The users.
        /// </value>
        public ObservableCollection<UsersProfileViewModel> Users { get; set; }

        /// <summary>
        /// Gets or sets the update command.
        /// </summary>
        /// <value>
        /// The update command.
        /// </value>
        public ICommand UpdateCommand { protected set; get; }

        /// <summary>
        /// Gets or sets the navigation.
        /// </summary>
        /// <value>
        /// The navigation.
        /// </value>
        public INavigation Navigation { get; set; }

        /// <summary>
        /// The selected user
        /// </summary>
        private UsersProfileViewModel _selectedUser;

        /// <summary>
        /// The current page
        /// </summary>
        private readonly Page _page;

        /// <summary>
        /// Initializes a new instance of the <see cref="UsersListViewModel"/> class.
        /// </summary>
        /// <param name="page">The current page.</param>
        public UsersListViewModel(Page page)
        {
            this._page = page;
            Users = new ObservableCollection<UsersProfileViewModel>();
            Update();
            UpdateCommand = new Command(Update);
        }

        /// <summary>
        /// Gets or sets the selected user.
        /// </summary>
        /// <value>
        /// The selected user.
        /// </value>
        public UsersProfileViewModel SelectedUser
        {
            get => _selectedUser;
            set
            {
                if (_selectedUser == value) return;
                var tempUser = value;

                tempUser.UpdateChannels();

                _selectedUser = null;
                OnPropertyChanged("SelectedUser");
                Navigation.PushAsync(new UsersProfilePage(tempUser));
            }
        }

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
                var ims = (IMListResponse)Slack.Response;

                await Slack.UsersList();
                var users = (UsersListResponse)Slack.Response;

                Users.Clear();

                foreach (var item in ims.Channels)
                {
                    string userName;
                    string image;
                    string email;

                    var user = users.Members.Where(x => x.Id == item.User);
                    {
                        userName = "";
                        image = "";
                        email = "";
                    }

                    if (user.Count() != 0)
                    {
                        userName = user.First().Profile.RealName;
                        image = user.First().Profile.Image72;
                        email = user.First().Name;
                    }

                    var newUser = new UsersProfileViewModel(_page)
                    {
                        User = userName,
                        UserId = item.User,
                        UserImage = image,
                        UserEmail = email,
                        Slack = this.Slack
                    };

                    await Slack.AuthTest();
                    var profile = (AuthTestResponse)Slack.Response;
                    var id = profile.UserId;
                    
                    if (item.User != id)
                    {
                        Users.Add(newUser);
                    }
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
