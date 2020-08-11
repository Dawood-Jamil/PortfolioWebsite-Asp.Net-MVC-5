using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CV.ViewModel
{
    public class ExperienceViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Company { get; set; }
        [Required]
        public System.DateTime Duration_From { get; set; }
        [Required]
        public System.DateTime Duration_To { get; set; }
        [Required]
        public string Position { get; set; }
        [Required]
        public string Description { get; set; }

        public string Logo { get; set; }
    }
}