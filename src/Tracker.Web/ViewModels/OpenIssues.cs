namespace Tracker.Web.ViewModels
{
    using Newtonsoft.Json;
    using Octokit;
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    public class OpenIssues
    {
        public IEnumerable<Issue> Issues { get; set; }
        public IDictionary<string, IEnumerable<Issue>> Assignments { get; set; }
    }

    public class IssueMetadata
    {
        public string Low { get; set; }
        public string High { get; set; }
        public string Target { get; set; }
    }
}