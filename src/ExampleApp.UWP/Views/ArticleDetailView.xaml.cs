using ExampleApp.UWP.Models;

using Disqus.SDK.UWP;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System;

namespace ExampleApp.UWP.Views
{
    public sealed partial class ArticleDetailView : UserControl
    {
        public Article MasterMenuItem
        {
            get { return GetValue(MasterMenuItemProperty) as Article; }
            set { SetValue(MasterMenuItemProperty, value); }
        }

        public DisqusCommentsEmbedConfig DisqusConfig
        {
            get
            {
                if (MasterMenuItem == null)
                    return null;

                return new DisqusCommentsEmbedConfig(App.DisqusForum, new Uri(MasterMenuItem.Link, UriKind.Absolute), MasterMenuItem.Title, MasterMenuItem.Id);
            }
        }

        public static DependencyProperty MasterMenuItemProperty = DependencyProperty.Register(
            "MasterMenuItem",
            typeof(Article),
            typeof(ArticleDetailView),
            new PropertyMetadata(null, MasterMenuItemProperty_Changed)
        );

        private static void MasterMenuItemProperty_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ArticleDetailView control)
            {
                control.DisqusEmbedView.EmbedConfig = control.DisqusConfig;
            }
        }

        public ArticleDetailView()
        {
            InitializeComponent();
        }
    }
}
