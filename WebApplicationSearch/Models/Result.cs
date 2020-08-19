using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationSearch.Models
{
    public class Result : IResult
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string SearchEngine { get; set; }
        public string Request { get; set; }
        public string Title { get; set; }
        public DateTime EnteredDate { get; set; }

        public override string ToString()
        {
            return $"{SearchEngine} {Request} {Title} {EnteredDate}";
        }
    }
}
