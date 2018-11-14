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
    public partial class UsersListPage : ContentPage
    {
        public UsersListPage()
        {
            InitializeComponent();
            this.BindingContext = new UsersListViewModel(this) { Navigation = this.Navigation };
        }
    }
}