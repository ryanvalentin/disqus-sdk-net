using ExampleApp.UWP.ViewModels;

using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace ExampleApp.UWP.Views
{
    public sealed partial class ArticleFeedMasterPage : Page
    {
        public ArticleFeedMasterViewModel ViewModel { get; } = new ArticleFeedMasterViewModel();
        public ArticleFeedMasterPage()
        {
            InitializeComponent();
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            await ViewModel.LoadDataAsync(WindowStates.CurrentState);
        }
    }
}
