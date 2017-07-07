using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace Disqus.SDK.UWP
{
    public sealed partial class DisqusCommentsEmbedView : UserControl
    {
        private const string _embedBase = "default"; // "dev" if local, "default" for production
        private const string _embedBaseUrl = "https://disqus.com/embed/comments/?base=";
        private readonly List<string> _loginUrlEntryPaths = new List<string>
        {
            "/_ax/facebook/begin/",
            "/_ax/twitter/begin/",
            "/_ax/google/begin/",
            "/next/login/",
            "/profile/login/",
            "/next/register/",
        };
        private bool _isInLoginMode = false;
        private Dictionary<string, string> _embedConfig = new Dictionary<string, string>()
        {
            { "version", "'current'" },
            { "anchorColor", "{ red: 0, green: 0, blue: 255 }" },
            { "colorScheme", "'dark'" }, // "dark", or "light"
            { "typeface", "'serif'" }, // "serif", or "sans-serif"
            // { "remoteAuthS3", "" }, // remote auth (SSO)
            // { "apiKey", "" }, // publisher api key
            // { "impressionId", "" },
            // { "sso", "{}" },
            // { "initialPosition", "" },
            // { "parentWindowHash", "" }, // For anchoring to a specific post, e.g. #comment-123456
            // { "inthreadLeadingCommentCount", "" },
            // { "inthreadRepeatCommentCount", "" },
            // { "inthreadTrailingCommentCount", "" },
            // { "inthreadPlacementUrl", "" },
            { "startedFullyVisible", "true" },
            // { "isHostIframed", "true" },
            // { "isBehindClick", "false" },
            // { "isHeightRestricted", "false" },
            // { "discovery", "{ disable_all: false }" },
            // { "experiment", "{ experiment: '', variant: '' }" },
            { "forceMobile", "true" },
            // { "forceAutoStyles", "false" },
            { "layout", "'mobile'" },
            // { "referrer", "" },
            // { "permalink", "" },
            // { "hostReferrer", "" },
            // { "canonicalUrl", "" },
        };

        // Using a DependencyProperty as the backing store for Property1.  
        // This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EmbedConfigProperty = DependencyProperty.Register(
            "EmbedConfig",
            typeof(DisqusCommentsEmbedConfig),
            typeof(DisqusCommentsEmbedView),
            new PropertyMetadata(null, EmbedConfig_PropertyChanged)
        );

        private static void EmbedConfig_PropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is DisqusCommentsEmbedView control)
            {
                control.Load();
            }
        }

        public DisqusCommentsEmbedView()
        {
            InitializeComponent();
        }

        public DisqusCommentsEmbedConfig EmbedConfig
        {
            get { return (DisqusCommentsEmbedConfig)GetValue(EmbedConfigProperty); }
            set { SetValue(EmbedConfigProperty, value); }
        }

        public void Load()
        {
            if (EmbedConfig?.IsValid() != true)
                return;

            try
            {
                CommentsWebView.Stop();
            }
            catch (Exception)
            {
                // Likely called at unexpected time
            }

            var embedUrl = GetEmbedUri();

            CommentsWebView.Navigate(embedUrl);
        }

        private Uri GetEmbedUri()
        {
            string title = WebUtility.UrlEncode(EmbedConfig.Title);

            string url = $"{_embedBaseUrl}{_embedBase}&f={EmbedConfig.Shortname}&t_i={WebUtility.UrlEncode(EmbedConfig.Identifier ?? "")}&t_u={WebUtility.UrlEncode(EmbedConfig.Url.OriginalString)}&t_t={title}&t_d={title}";

            if (_embedBase == "default")
                url += "#version=8e4b6e0b39b37378f55ff3d3af623214"; // TODO: Provide a way to get the latest version like "latest" or "current"

            return new Uri(url);
        }

        private async void CommentsWebView_DOMContentLoaded(WebView sender, WebViewDOMContentLoadedEventArgs args)
        {
            // Update the hash to include proper config. We must do this after the DOM content 
            // loads (and before the javascript loads) because the hash must be the version initially.
            await UpdateHashConfigAsync();
        }

        private async void CommentsWebView_NavigationStarting(WebView sender, WebViewNavigationStartingEventArgs args)
        {
            LoadingProgressBar.IsIndeterminate = true;

            if (args.Uri.OriginalString.StartsWith(_embedBaseUrl))
                return;

            if (_isInLoginMode)
            {
                if (!String.IsNullOrEmpty(args.Uri.Fragment))
                {
                    switch (args.Uri.Fragment)
                    {
                        case "#!auth%3Acancel":
                        case "#!auth%3Asuccess":
                        case "#!auth%3Afail":
                            args.Cancel = true;
                            Load();
                            _isInLoginMode = false;
                            return;
                    }
                }

                // Allow anything that reaches this point to continue navigating
            }
            else
            {
                args.Cancel = true;
                LoadingProgressBar.IsIndeterminate = false;

                await Windows.System.Launcher.LaunchUriAsync(args.Uri);
            }
        }

        private async void CommentsWebView_NavigationCompleted(WebView sender, WebViewNavigationCompletedEventArgs args)
        {
            if (args.Uri.AbsoluteUri.StartsWith("https://disqus.com/embed/comments/"))
            {
                await EvalJavascriptAsync(@"document.body.style.paddingRight = '19.5px';");
                await EvalJavascriptAsync(@"window.addEventListener('message', event => { let data = event.data; data = typeof data === 'string' ? data : JSON.stringify(data); window.external.notify(data); }, false);");
            }
        }

        private void CommentsWebView_NewWindowRequested(WebView sender, WebViewNewWindowRequestedEventArgs args)
        {
            if (args.Uri.Host == "disqus.com" && _loginUrlEntryPaths.Contains(args.Uri.AbsolutePath))
            {
                // Prevent web browser from being opened
                args.Handled = true;

                // Launch the login experience here. You can either navigate to the URL in this page, or open a new WebView window
                _isInLoginMode = true;
                CommentsWebView.Navigate(args.Uri);
            }
        }

        private async void CommentsWebView_ScriptNotify(object sender, NotifyEventArgs e)
        {
            await HandlePostMessageAsync(e.Value);
        }

        private async Task<bool> EvalJavascriptAsync(string javascript)
        {
            bool success = false;

            try
            {
                var arguments = new string[] { javascript };

                await CommentsWebView.InvokeScriptAsync("eval", arguments);

                success = true;
            }
            catch (Exception)
            {

            }

            return success;
        }

        private async Task UpdateHashConfigAsync()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{");
            foreach (var configItem in _embedConfig)
            {
                sb.Append(configItem.Key);
                sb.Append(": ");
                sb.Append(configItem.Value);
                sb.Append(",");
            }
            sb.Append("}");

            await EvalJavascriptAsync(@"window.location.hash = '#' + encodeURIComponent(JSON.stringify(" + sb.ToString() + "))");
        }

        private async Task HandlePostMessageAsync(string value)
        {
            try
            {
                var message = JObject.Parse(value);

                var eventName = (string)message["name"];
                var data = message["data"];

                switch (eventName)
                {
                    // Public events
                    case "ready":
                        // Corresponds to "onReady"
                        LoadingProgressBar.IsIndeterminate = false;
                        break;
                    case "posts.create":
                        // Corresponds to "onNewComment"
                        break;
                    case "posts.paginate":
                        // Corresponds to "onPaginate"
                        break;
                    case "posts.count":
                        // Corresponds to "onCommentCountChange"
                        break;
                    case "session.identify":
                        // Corresponds to "onIdentify"
                        break;

                    // Internal events
                    case "rendered":
                        break;
                    case "fakeScroll":
                        break;

                    // Home events
                    case "home.show":
                        await Windows.System.Launcher.LaunchUriAsync(new Uri("https://disqus.com/" + (string)data["path"]));
                        break;

                    // Unnecessary events for a WebView control
                    case "resize":
                    default:
                        break;
                }
            }
            catch (Exception)
            {
                // Continue 
            }
        }
    }
}
