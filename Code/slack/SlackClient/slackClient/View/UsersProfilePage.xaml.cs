using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SlackClient.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SlackClient.Views
{
    public partial class UsersProfilePage : ContentPage
    {
        public UsersProfileViewModel ViewModel { get; private set; }
        public UsersProfilePage(UsersProfileViewModel profile)
        {
            InitializeComponent();
            this.BindingContext = new UsersProfileViewModel(this);
            ViewModel = profile;
            this.BindingContext = ViewModel;
        }
    }
}