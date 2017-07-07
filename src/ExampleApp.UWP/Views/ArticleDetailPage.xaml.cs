using ExampleApp.UWP.Models;
using ExampleApp.UWP.ViewModels;

using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace ExampleApp.UWP.Views
{
    public sealed partial class ArticleDetailPage : Page
    {
        public ArticleDetailViewModel ViewModel { get; } = new ArticleDetailViewModel();
        public ArticleDetailPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            ViewModel.Item = e.Parameter as Article;
        }
    }
}
