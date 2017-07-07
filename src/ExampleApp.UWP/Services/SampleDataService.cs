using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

using ExampleApp.UWP.Models;

namespace ExampleApp.UWP.Services
{
    // This class holds sample data used by some generated pages to show how they can be used.
    // TODO UWPTemplates: Delete this file once your app is using real data.
    public static class SampleDataService
    {
        private static IEnumerable<Article> AllOrders()
        {
            // The following is order summary data
            var data = new ObservableCollection<Article>
            {
                new Article
                {
                    Id = "5208262972",
                    Title = "Here’s what 973 Disqus commenters revealed about their reading habits",
                    Description = "At Disqus, we are proud and humbled to boast an expansive network of users. Since writers and publishers are always striving to learn more about their audience, we thought it was a good idea to ask our users what they read, how they read it, and why. We also asked questions around reader-publisher engagement and what content they pay for. To answer these questions, we recently conducted a survey and in just one day, we got 973 responses! Here’s a summary of the most interesting findings from our research.",
                    Link = "https://blog.disqus.com/disqus-commenters-reveal-their-reading-habits",
                    Content = "<div class=\"hs-featured-image-wrapper\"> <a href=\"https://blog.disqus.com/disqus-commenters-reveal-their-reading-habits\" title=\"\" class=\"hs-featured-image-link\"> <img src=\"https://blog.disqus.com/hubfs/disqus-reader-survey-2017.png?t=1499295289583\" alt=\"Here’s what 973 Disqus commenters revealed about their reading habits\" class=\"hs-featured-image\" style=\"width:auto !important; max-width:50%; float:left; margin:0 15px 15px 0;\"> </a> </div> <p><span style=\"font-weight: 400;\">At Disqus, we are proud and humbled to boast an </span><a href=\"http://data.disqus.com/\" style=\"background-color: transparent;\">expansive network</a><span style=\"font-weight: 400;\"> of users. Since writers and publishers are always striving to learn more about their audience, we thought it was a good idea to ask our users what they read, how they read it, and why. We also asked questions around reader-publisher engagement and what content they pay for. To answer these questions, we recently conducted a survey and in just one day, we got 973 responses! Here’s a summary of the most interesting findings from our research.</span></p> <p>&nbsp;</p> <img src=\"http://track.hubspot.com/__ptq.gif?a=429754&amp;k=14&amp;r=https%3A%2F%2Fblog.disqus.com%2Fdisqus-commenters-reveal-their-reading-habits&amp;bu=https%253A%252F%252Fblog.disqus.com&amp;bvt=rss\" alt=\"\" width=\"1\" height=\"1\" style=\"min-height:1px!important;width:1px!important;border-width:0!important;margin-top:0!important;margin-bottom:0!important;margin-right:0!important;margin-left:0!important;padding-top:0!important;padding-bottom:0!important;padding-right:0!important;padding-left:0!important; \">",
                },
                new Article
                {
                    Id = "5200630409",
                    Title = "Disqus for WordPress 3.0 Plugin Beta Now Available For Testing",
                    Description = "We recently shared news about an upcoming major release of the Disqus WordPress plugin. Today, we’re excited to announce the release of a beta version of the plugin available to download and install on your site. We've completely rewritten the plugin from the ground up using the latest WordPress APIs that will allow us to deliver more frequent improvements in the future. Here’s a rundown of the biggest updates:",
                    Link = "https://blog.disqus.com/disqus-for-wordpress-3.0-plugin-beta-now-available-for-testing",
                    Content = "<div class=\"hs-featured-image-wrapper\"> <a href=\"https://blog.disqus.com/disqus-for-wordpress-3.0-plugin-beta-now-available-for-testing\" title=\"\" class=\"hs-featured-image-link\"> <img src=\"https://blog.disqus.com/hubfs/disqus-wordpress-plugin.png?t=1499295289583\" alt=\"disqus-wordpress-plugin.png\" class=\"hs-featured-image\" style=\"width:auto !important; max-width:50%; float:left; margin:0 15px 15px 0;\"> </a> </div> <span style=\"font-weight: 400;\">We recently shared </span> <a href=\"https://blog.disqus.com/ahead-of-the-curve-the-disqus-wordpress-plugin-in-2017\"><span style=\"font-weight: 400;\">news</span></a> <span style=\"font-weight: 400;\"> about an upcoming major release of the </span> <a href=\"https://wordpress.org/plugins/disqus-comment-system/\"><span style=\"font-weight: 400;\">Disqus WordPress plugin</span></a> <span style=\"font-weight: 400;\">. Today, we’re excited to announce the release of a </span> <a href=\"https://github.com/ryanvalentin/disqus-wordpress-plugin/releases/\"><span style=\"font-weight: 400;\">beta version of the plugin</span></a> <span style=\"font-weight: 400;\"> available to download and install on your site. We've completely rewritten the plugin from the ground up using the latest WordPress APIs that will allow us to deliver more frequent improvements in the future.</span> <p><span style=\"font-weight: 400;\">Here’s a rundown of the biggest updates:</span></p>",
                },
                new Article
                {
                    Id = "5135099144",
                    Title = "Introducing Shadow Banning and Timeouts",
                    Description = "2 new tools to fight trolls, improve discussion quality, and save your team time! We're excited to announce two new moderation features for publishers using Disqus Pro - Shadow Banning and Timeouts. With these new features at their disposal, moderators now have even more flexibility when dealing with troublesome users. While the traditional user banning feature is a great way for moderators to keep spammers and trolls out of their communities, a one size fits all solution is not always flexible enough to deal with different types of users. That's where Shadow Banning and Timeouts come into play.",
                    Link = "https://blog.disqus.com/introducing-shadow-banning-and-timeouts",
                    Content = "<div class=\"hs-featured-image-wrapper\"> <a href=\"https://blog.disqus.com/introducing-shadow-banning-and-timeouts\" title=\"\" class=\"hs-featured-image-link\"> <img src=\"https://blog.disqus.com/hubfs/shadow%20ban%20image.png?t=1499295289583\" alt=\"shadow ban image.png\" class=\"hs-featured-image\" style=\"width:auto !important; max-width:50%; float:left; margin:0 15px 15px 0;\"> </a> </div> <h2><span style=\"font-weight: 400;\">2 new tools to fight trolls, improve discussion quality, and save your team time!</span></h2> <p><span style=\"font-weight: 400;\">We're excited to announce two new moderation features for publishers using <a href=\"https://about.disqus.com/pro\">Disqus Pro</a> - Shadow Banning and Timeouts. With these new features at their disposal, moderators now have even more flexibility when dealing with troublesome users. While the traditional user banning feature is a great way for moderators to keep spammers and trolls out of their communities, a one size fits all solution is not always flexible enough to deal with different types of users. That's where Shadow Banning and Timeouts come into play.</span></p> <img src=\"http://track.hubspot.com/__ptq.gif?a=429754&amp;k=14&amp;r=https%3A%2F%2Fblog.disqus.com%2Fintroducing-shadow-banning-and-timeouts&amp;bu=https%253A%252F%252Fblog.disqus.com&amp;bvt=rss\" alt=\"\" width=\"1\" height=\"1\" style=\"min-height:1px!important;width:1px!important;border-width:0!important;margin-top:0!important;margin-bottom:0!important;margin-right:0!important;margin-left:0!important;padding-top:0!important;padding-bottom:0!important;padding-right:0!important;padding-left:0!important; \">",
                },
            };

            return data;
        }

        // TODO UWPTemplates: Remove this once your MasterDetail pages are displaying real data
        public static async Task<IEnumerable<Article>> GetSampleModelDataAsync()
        {
            await Task.CompletedTask;

            return AllOrders();
        }
    }
}
