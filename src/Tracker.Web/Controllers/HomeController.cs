using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Octokit;
using Octokit.Helpers;
using System.Threading.Tasks;
using System.Web.Configuration;
using Tracker.Web.ViewModels;

namespace Tracker.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly string accessToken;
        private readonly string owner;
        private readonly string repository;

        public HomeController()
        {
            accessToken = WebConfigurationManager.AppSettings["github:AccessToken"];
            owner = WebConfigurationManager.AppSettings["github:Owner"];
            repository = WebConfigurationManager.AppSettings["github:Repository"];
        }

        public async Task<ActionResult> Index()
        {
            // Log in and get list of private repo issues
            var client = new GitHubClient(new ProductHeaderValue("github-issue-priority-tracker"))
            {
                Credentials = new Credentials(accessToken)
            };

            var issues = await client.Issue.GetForRepository(owner, repository, new RepositoryIssueRequest { State = ItemState.Open });
            var assignments = issues.GroupBy(x => x.Assignee == null ? "Unassigned" : x.Assignee.Login).ToDictionary(x => x.Key, x => x.AsEnumerable());

            return View(new OpenIssues()
            {
                Issues = issues,
                Assignments = assignments                
            });
        }

        [HttpPost]
        public async Task<ActionResult> Transfer(int issueNumber, string to)
        {
            var client = new GitHubClient(new ProductHeaderValue("github-issue-priority-tracker"))
            {
                Credentials = new Credentials(accessToken)
            };

            if (to == "Unassigned")
            {
                to = "none";
            }

            var issue = await client.Issue.Update(owner, repository, issueNumber, new IssueUpdate() { Assignee = to });
            return new JsonResult() { Data = issue };
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}