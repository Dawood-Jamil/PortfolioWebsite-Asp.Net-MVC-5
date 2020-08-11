using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CV.ViewModel
{
    public class ProjectsViewModel
    {

        public int Id { get; set; }
        [Required]
        
        [Display(Name ="Project Name")]
        public string Project_Name { get; set; }
        [Required]
        [Display(Name ="Technology Used")]
        public string Tech_Used { get; set; }
        [Required]
        public bool Status { get; set; }
        [Display(Name ="Link(if project is online)")]
        public string Link { get; set; }
        public string Image { get; set; }
    }
}