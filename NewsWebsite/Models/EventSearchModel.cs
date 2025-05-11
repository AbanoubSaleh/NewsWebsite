using System;
using System.ComponentModel.DataAnnotations;

namespace NewsWebsite.Models
{
    public class EventSearchModel
    {
        [Display(Name = "Search Term")]
        public string SearchTerm { get; set; }
    }
}