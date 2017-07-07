using System;

namespace Disqus.SDK.UWP
{
    public class DisqusCommentsEmbedConfig
    {
        public DisqusCommentsEmbedConfig(string shortname, Uri url, string title, string identifier = "")
        {
            Shortname = shortname;
            Url = url;
            Title = title;
            Identifier = identifier;
        }

        public string Shortname { get; set; }

        public string Identifier { get; set; }

        public Uri Url { get; set; }

        public string Title { get; set; }

        internal bool IsValid()
        {
            return !String.IsNullOrWhiteSpace(Shortname) && !String.IsNullOrWhiteSpace(Title) && Url?.IsAbsoluteUri == true;
        }
    }
}
