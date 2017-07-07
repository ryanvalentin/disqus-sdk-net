using System;
using System.Windows.Input;

using ExampleApp.UWP.Helpers;
using ExampleApp.UWP.Models;
using ExampleApp.UWP.Services;

using Disqus.SDK.UWP;

using Windows.UI.Xaml;

namespace ExampleApp.UWP.ViewModels
{
    public class ArticleDetailViewModel : Observable
    {
        const string NarrowStateName = "NarrowState";
        const string WideStateName = "WideState";

        public ICommand StateChangedCommand { get; private set; }

        private Article _item;
        public Article Item
        {
            get { return _item; }
            set { Set(ref _item, value); }
        }

        public ArticleDetailViewModel()
        {
            StateChangedCommand = new RelayCommand<VisualStateChangedEventArgs>(OnStateChanged);
        }
        
        private void OnStateChanged(VisualStateChangedEventArgs args)
        {
            if (args.OldState.Name == NarrowStateName && args.NewState.Name == WideStateName)
            {
                NavigationService.GoBack();
            }
        }
    }
}
