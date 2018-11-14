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
    public partial class ChannelsListPage : ContentPage
    {
        public ChannelsListPage()
        {
            InitializeComponent();
            this.BindingContext = new ChannelsListViewModel(this) { Navigation = this.Navigation };
        }
    }
}