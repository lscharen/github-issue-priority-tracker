namespace Tracker.Web.Helpers
{
    using Newtonsoft.Json;
    using Octokit;
    using System.Text.RegularExpressions;
    using Tracker.Web.ViewModels;

    public static class OctokitHelpers
    {
        public static IssueMetadata Metadata(this Issue issue)
        {
            // Look for a 'ticket-v1:<json>' payload within a comment in the first line of the issue body
            var regex = new Regex(@"<!--\s*tracker-v1:(.*)\s*-->");
            var match = regex.Match(issue.Body ?? "");

            if (!match.Success || match.Groups.Count < 1)
            {
                return new IssueMetadata();
            }

            return JsonConvert.DeserializeObject<IssueMetadata>(match.Groups[1].Value);
        }
    }
}