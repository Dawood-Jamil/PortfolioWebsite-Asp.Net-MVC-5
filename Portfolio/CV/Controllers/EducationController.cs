using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace CV.Controllers
{
    public class EducationController : ApiController
    {
        ResumeEntities _context = new ResumeEntities();

        public HttpResponseMessage Get()
        {
            var models = _context.Educations.ToList();
            if (models == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Empty List");
            }
            else
                return Request.CreateResponse(HttpStatusCode.OK, models);
        }

        public HttpResponseMessage Post(Education edu)
        {
            HttpResponseMessage result = null;
            var httpRequest = HttpContext.Current.Request;
            if (httpRequest.Files.Count > 0 && edu != null)
            {
                var docfiles = new List<string>();
                foreach (string file in httpRequest.Files)
                {
                    var postedFile = httpRequest.Files[file];
                    var filePath = HttpContext.Current.Server.MapPath("~/Content/PortfolioImages/" + postedFile.FileName);
                    postedFile.SaveAs(filePath);
                    docfiles.Add(filePath);
                    Education model = new Education()
                    {
                        Institution_Name = edu.Institution_Name,
                        Degree_Name = edu.Degree_Name,
                        Duration_From = edu.Duration_From,
                        Duration_TO = edu.Duration_TO,
                        Description = edu.Description,
                        Image = postedFile.FileName.ToString()
                    };
                    _context.Educations.Add(model);
                    _context.SaveChanges();
                    result = Request.CreateResponse(HttpStatusCode.Created, model);
                    result.Headers.Location = new Uri(Request.RequestUri + model.Id.ToString());
                }
               
                return result;
            }
            else
            {
                result = Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid Request");
                return result;
            }
          
        }




    }
}
