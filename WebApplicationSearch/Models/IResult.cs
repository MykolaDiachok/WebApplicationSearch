using System;

namespace WebApplicationSearch.Models
{
    public interface IResult
    {
        int Id { get; set; }
        string SearchEngine { get; set; }
        string Request { get; set; }
        string Title { get; set; }
        DateTime EnteredDate { get; set; }
    }
}