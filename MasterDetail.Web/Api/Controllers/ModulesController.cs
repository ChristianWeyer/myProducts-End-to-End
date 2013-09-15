using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace MasterDetail.Web.Api.Controllers
{
    public class ModulesController : ApiController
    {
        private readonly List<ModuleItem> modules;

        public ModulesController()
        {
            var module0 = new ModuleItem { Module = "Articles", DisplayText = "INDEX_ARTICLES", Url = "/articles", MatchPattern = "(/|/articles.*)", Users = new List<string> { "cw", "bob" } };
            var module1 = new ModuleItem { Module = "ArticleDetails", Url = "/articles/:id", Users = new List<string> {"cw", "bob" }};
            var module2 = new ModuleItem { Module = "Admin", DisplayText = "INDEX_ADMIN", Url = "/admin", MatchPattern = "/admin", Users = new List<string> { "cw" } };
            var module3 = new ModuleItem { Module = "Log", DisplayText = "INDEX_LOGS", Url = "/log", MatchPattern = "/log", Users = new List<string> { "cw" } };
            var module4 = new ModuleItem { Module = "Statistics", DisplayText = "INDEX_STATS", Url = "/stats", MatchPattern = "/stats", Users = new List<string> { "cw", "bob" } };
            modules = new List<ModuleItem> { module0, module1, module2, module3, module4 };
        }

        [HttpGet]
        public List<ModuleItem> ListModules()
        {
            // TODO: check against real repo with current user
            return modules.Where(m => m.Users.Contains(User.Identity.Name)).ToList();
        }
    }

    public class ModuleItem
    {
        public string Module { get; set; }
        public string Url { get; set; }
        public bool OverrideRoot { get; set; }
        public string DisplayText { get; set; }
        public string MatchPattern { get; set; }
        public List<string> Users { get; set; }
    }
}