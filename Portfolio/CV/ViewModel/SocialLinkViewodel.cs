using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CV.ViewModel
{
    public class SocialLinkViewodel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Link { get; set; }
        [Required]
        [Display(Name="Icon Class")]
        public string Icon_Class { get; set; }
    }
}
