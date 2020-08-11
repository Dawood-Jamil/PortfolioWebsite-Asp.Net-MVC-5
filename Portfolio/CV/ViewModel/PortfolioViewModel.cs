using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace CV.ViewModel
{
    public class PortfolioViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public string Link { get; set; }

    }
}