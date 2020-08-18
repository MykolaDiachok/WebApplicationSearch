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
    [ApiController]
    [Route("api/[controller]")]
    public class GoogleController : Controller
    {
        private readonly ILogger<GoogleController> _logger;
        private readonly DBContext _context;

        public GoogleController(ILogger<GoogleController> logger, DBContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public IEnumerable<IResult> Get(string search="anglersharp", int topPage=5)
        {
            
            var config = Configuration.Default.WithDefaultLoader();
            var document = BrowsingContext.New(config).OpenAsync($"https://www.google.com/search?num={topPage}").Result;
            var form = document.QuerySelector<IHtmlFormElement>("form[action='/search']");
            var result = form.SubmitAsync(new { q = $"{search}" }).Result;
            var t = result.QuerySelectorAll("div.search");
            var links = result.QuerySelectorAll<IHtmlAnchorElement>("a"); // CSS
            foreach (var link in links)
            {
                var url = link.Attributes["href"].Value; // HTML DOM
                var newResult = new Result
                    {EnteredDate = DateTime.Now, Request = search, SearchEngine = "Google", Title = url};
                _context.Results.Add(newResult);
                _context.SaveChanges();
                yield return newResult;
            }
        }
    }
}
