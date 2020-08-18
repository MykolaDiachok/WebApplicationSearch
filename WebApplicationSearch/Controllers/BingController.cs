﻿using System;
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
    public class BingController : Controller
    {
        private readonly ILogger<BingController> _logger;
        private readonly DBContext _context;
        private const string eng = "Bing";

        public BingController(ILogger<BingController> logger, DBContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public async IAsyncEnumerable<Result> Get(string search = "anglersharp", int topPage = 5)
        {

            var config = Configuration.Default.WithDefaultLoader();
            var document =  await BrowsingContext.New(config).OpenAsync($"https://www.bing.com/search");
            var form = document.QuerySelector<IHtmlFormElement>("form[action='/search']");
            var result =  await form.SubmitAsync(new { q = $"{search}" });
            var resultCollection = result.QuerySelectorAll<IHtmlAnchorElement>("ol li.b_algo h2 a").Take(topPage);
            
            foreach (var link in resultCollection)
            {
                var newResult = new Result
                { EnteredDate = DateTime.Now, Request = search, SearchEngine = eng, Title = link.Text };
                _context.Results.Add(newResult);
                _context.SaveChanges();
                yield return newResult;
            }
        }
    }
}
