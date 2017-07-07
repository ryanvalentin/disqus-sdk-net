using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

using ExampleApp.UWP.Helpers;
using ExampleApp.UWP.Models;
using ExampleApp.UWP.Services;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace ExampleApp.UWP.ViewModels
{
    public class ArticleFeedMasterViewModel : Observable
    {
        const string NarrowStateName = "NarrowState";
        const string WideStateName = "WideState";

        private VisualState _currentState;

        private Article _selected;
        public Article Selected
        {
            get { return _selected; }
            set { Set(ref _selected, value); }
        }

        public ICommand ItemClickCommand { get; private set; }
        public ICommand StateChangedCommand { get; private set; }

        public ObservableCollection<Article> SampleItems { get; private set; } = new ObservableCollection<Article>();

        public ArticleFeedMasterViewModel()
        {
            ItemClickCommand = new RelayCommand<ItemClickEventArgs>(OnItemClick);
            StateChangedCommand = new RelayCommand<VisualStateChangedEventArgs>(OnStateChanged);
        }

        public async Task LoadDataAsync(VisualState currentState)
        {
            _currentState = currentState;
            SampleItems.Clear();

            var data = await SampleDataService.GetSampleModelDataAsync();

            foreach (var item in data)
            {
                SampleItems.Add(item);
            }
            Selected = SampleItems.First();
        }

        private void OnStateChanged(VisualStateChangedEventArgs args)
        {
            _currentState = args.NewState;
        }

        private void OnItemClick(ItemClickEventArgs args)
        {
            if (args?.ClickedItem is Article item)
            {
                if (_currentState.Name == NarrowStateName)
                {
                    NavigationService.Navigate<Views.ArticleDetailPage>(item);
                }
                else
                {
                    Selected = item;
                }
            }
        }
    }
}
