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
        public ObservableCollection<UsersProfileViewModel> Users { get; set; }

        public ICommand UpdateCommand { protected set; get; }
        public INavigation Navigation { get; set; }

        UsersProfileViewModel selectedUser;

        private Page page;

        public UsersListViewModel(Page page)
        {
            this.page = page;
            Users = new ObservableCollection<UsersProfileViewModel>();
            Update();
            UpdateCommand = new Command(Update);
        }

        public UsersProfileViewModel SelectedUser
        {
            get => selectedUser;
            set
            {
                if (selectedUser != value)
                {
                    UsersProfileViewModel tempUser = value;


                   // tempUser.Channels.Clear();////
                    tempUser.UpdateChannels();

                    selectedUser = null;
                    OnPropertyChanged("SelectedUser");
                    Navigation.PushAsync(new UsersProfilePage(tempUser));
                }
            }
        }

        private bool isUpdating = false;
        public bool IsUpdating
        {
            get => isUpdating;
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


                await slack.IMList();//
                var ims = (IMListResponse)slack.Response;//


                await slack.UsersList();//
                UsersListResponse users = (UsersListResponse)slack.Response;//

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

                    var newUser = new UsersProfileViewModel(page)
                    {
                        User = userName,
                        UserId = item.User,
                        UserImage = image,
                        UserEmail = email,
                        Slack = this.slack
                    };

                    await slack.AuthTest();//
                    var profile = (AuthTestResponse)slack.Response;//
                    string id = profile.UserId;//
                    
                    if (item.User != id)////////////////
                    {
                        Users.Add(newUser);
                    }
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
