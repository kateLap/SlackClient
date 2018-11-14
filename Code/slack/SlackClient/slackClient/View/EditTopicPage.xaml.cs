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
    public partial class EditTopicPage : ContentPage
    {
        public EditTopicViewModel ViewModel { get; private set; }
        public EditTopicPage(EditTopicViewModel vm)
        {
            InitializeComponent();
            this.BindingContext = new EditTopicViewModel(this);
            ViewModel = vm;
            this.BindingContext = ViewModel;
        }
    }
}