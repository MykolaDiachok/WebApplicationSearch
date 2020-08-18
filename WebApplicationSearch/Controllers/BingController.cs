using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApplicationSearch.Models;

namespace WebApplicationSearch.Controllers
{
    public class BingController : Controller
    {
        private readonly ILogger<BingController> _logger;
        private readonly DBContext _context;

        public BingController(ILogger<BingController> logger, DBContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public async IAsyncEnumerable<Result> Get(string search = "anglersharp", int topPage = 20)
        {

            var config = Configuration.Default.WithDefaultLoader();
            var document = await BrowsingContext.New(config).OpenAsync($"https://www.google.com/search?num={topPage}");
            var form = document.QuerySelector<IHtmlFormElement>("form[action='/search']");
            var result = await form.SubmitAsync(new { q = $"{search}" });
            var t = result.QuerySelectorAll("*").Where(m => m.LocalName == "div" && m.HasAttribute("id") && m.GetAttribute("id") == "main");
            var classname = result.QuerySelectorAll<IHtmlAnchorElement>("a")
                .Select(x => new { x.ParentElement.ClassName }).GroupBy(x => x.ClassName)
                .Select(x => new { key = x.Key, count = x.Count() }).OrderByDescending(x => x.count).First();
            var links = result.QuerySelectorAll<IHtmlAnchorElement>("a").Where(x => x.ParentElement.ClassName == classname.key).Take(topPage); // CSS
            foreach (var link in links)
            {
                var url = link.Attributes["href"].Value; // HTML DOM
                var newResult = new Result
                    { EnteredDate = DateTime.Now, Request = search, SearchEngine = "Google", Title = link.Text };
                _context.Results.Add(newResult);
                _context.SaveChanges();
                yield return newResult;
            }
        }
    }
}
