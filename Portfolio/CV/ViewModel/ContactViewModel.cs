using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CV.Models;
using CV.ViewModel;

namespace CV.ViewModel
{
    public class ContactViewModel
    {
        public Personal_info Info { get; set; }
        public SocialLink socialLink { get; set; }
    }
}