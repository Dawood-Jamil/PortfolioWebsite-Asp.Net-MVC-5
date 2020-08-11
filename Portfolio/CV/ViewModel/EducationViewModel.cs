using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CV.ViewModel
{
    public class EducationViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Degree_Name { get; set; }
        [Required]
        public System.DateTime Duration_From { get; set; }
        [Required]
        public System.DateTime Duration_To { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Institution_Name { get; set; }

        public string Image { get; set; }
    }
}