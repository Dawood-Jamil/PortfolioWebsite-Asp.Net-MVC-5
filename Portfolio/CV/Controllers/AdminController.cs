using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using CV.Models;
using System.Data.Entity;
using CV.ViewModel;
using System.Net; 

namespace CV.Controllers
{
   
    public class AdminController : Controller
    {
        private ResumeEntities _context = new ResumeEntities();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        

        [HttpPost]
        public ActionResult Index(Login user)
        {
            bool isvalid = _context.Admin_Users.Any(u => u.User_Name == user.UserName && u.Password == user.Password);
            if (isvalid)
            {
                FormsAuthentication.SetAuthCookie(user.UserName, false);
                return RedirectToAction("Dashboard", "Admin");
            }
            ModelState.AddModelError("", "Invalid user name or password");
            return View("Index");
        }

        [Authorize]
        public ActionResult Dashboard()
        {
           
            return View();
        }

        [Authorize]
        public ActionResult About()
        {

            var infoinDb = _context.Personal_info.Single(m => m.Id == 0);
            return View(infoinDb);
        }

        [HttpPost]
        public ActionResult About(Personal_info Info,HttpPostedFileBase file)
        { 
            if (ModelState.IsValid)
            {
                try
                {
                    if (file != null)
                    {
                        string path = System.IO.Path.Combine("../Content/PortfolioImages/" + file.FileName);
                        file.SaveAs(Server.MapPath(path));
                    }
                   
                    var infoinDB = _context.Personal_info.Single(i => i.Id == 0);
                    infoinDB.Name = Info.Name;
                    infoinDB.Father_Name = Info.Father_Name;
                    infoinDB.Phone = Info.Phone;
                    infoinDB.Address = Info.Address;
                    infoinDB.Email = Info.Email;
                    infoinDB.Field = Info.Field;
                    if(file != null)
                    {
                        infoinDB.image = file.FileName.ToString();
                    }
                   
                    infoinDB.DOB = Info.DOB;
                    infoinDB.Projects_done = Info.Projects_done;
                    infoinDB.Projects_online = Info.Projects_online;
                    infoinDB.Nationality = Info.Nationality;
                    infoinDB.BirthPlace= Info.BirthPlace;
                    infoinDB.Gender = Info.Gender;
                    infoinDB.Description = Info.Description;
                    _context.SaveChanges();
                    return RedirectToAction("Dashboard");
                }
                catch (InvalidOperationException ex)
                {

                    Console.WriteLine(ex);
                }  

            }
            return View("About");
            
          
        }

        [Authorize]
        public ActionResult Education()
        {
            var models= _context.Educations.ToList();
            return View(models);
        }

        [Authorize]
        public ActionResult EducationForm()
        { 
            return View();
        }

        [HttpPost]
        public ActionResult AddEducation(EducationViewModel model, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    string path = System.IO.Path.Combine("../Content/PortfolioImages/" + file.FileName);
                    file.SaveAs(Server.MapPath(path));
                }
                Education obj = new Education()
                {
                    Institution_Name = model.Institution_Name,
                    Degree_Name = model.Degree_Name,
                    Duration_From = model.Duration_From,
                    Duration_TO = model.Duration_To,
                    Description = model.Description,
                };
                if (file != null)
                    obj.Image = file.FileName.ToString();
                _context.Educations.Add(obj);
                _context.SaveChanges();
                return RedirectToAction("Education");
            }
            else
                return View("EducationForm", model);
        }

        public ActionResult EditEducation(int? id)
        {
            if (id == null)
            {
              return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var model = _context.Educations.Single(e => e.Id == id);
            if (model == null)
            {
                return HttpNotFound();
            }
            
            EducationViewModel obj = new EducationViewModel()
            {
                Institution_Name = model.Institution_Name,
                Degree_Name = model.Degree_Name,
                Duration_From = model.Duration_From,
                Duration_To = model.Duration_TO,
                Description=model.Description,
                Image = model.Image
            };
            return View(obj);
        }
        [Authorize(Roles ="Admin")]
        [HttpPost]
        public ActionResult Edit(EducationViewModel model, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    string path = System.IO.Path.Combine("../Content/PortfolioImages/" + file.FileName);
                    file.SaveAs(Server.MapPath(path));
                }
               
                var eduInDb = _context.Educations.Single(e => e.Id == model.Id);
                eduInDb.Institution_Name = model.Institution_Name;
                eduInDb.Degree_Name = model.Degree_Name;
                eduInDb.Duration_From = model.Duration_From;
                eduInDb.Duration_TO = model.Duration_To;
                eduInDb.Description = model.Description;
                if (file != null)
                    eduInDb.Image = file.FileName.ToString();
                _context.SaveChanges();
            }
           
            return RedirectToAction("Education");
        }
        [Authorize(Roles = "Admin")]
        [Authorize]
        public ActionResult DeleteEdu(int id)
        {
            var model = _context.Educations.Single(e => e.Id == id);
            _context.Educations.Remove(model);
            _context.SaveChanges();
            return RedirectToAction("Education");
        }

        [Authorize]
        public ActionResult Experience()
        {
            var exp = _context.Experiences.ToList();
            return View(exp);

        }


        public ActionResult ExperienceForm()
        {
            return View();

        }

       

        [HttpPost]
        public ActionResult AddExperience(ExperienceViewModel model, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    string path = System.IO.Path.Combine("../Content/PortfolioImages/" + file.FileName);
                    file.SaveAs(Server.MapPath(path));
                }
               
                if (model.Id == 0)
                {
                    Experience exp = new Experience()
                    {
                        Company = model.Company,
                        Position = model.Position,
                        Duration_From = model.Duration_From,
                        Duration_To = model.Duration_To,
                        Description = model.Description
                    };
                    if (file != null)
                        exp.Logo = file.FileName.ToString();
                    _context.Experiences.Add(exp);
                    _context.SaveChanges();
                    return RedirectToAction("Experience");
                }
                else
                {
                    var expInDb = _context.Experiences.Single(m => m.Id == model.Id);
                    expInDb.Company = model.Company;
                    expInDb.Position = model.Position;
                    expInDb.Duration_From = model.Duration_From;
                    expInDb.Duration_To = model.Duration_To;
                    expInDb.Description = model.Description;
                    if (file != null)
                        expInDb.Logo = file.FileName.ToString();
                    _context.SaveChanges();

                    return RedirectToAction("Experience");
                }
               
               

            }
            return RedirectToAction("Experience");
        }

        [Authorize]
        public ActionResult EditExp(int id)
        {
            var expInDb = _context.Experiences.Single(m => m.Id == id);
            ExperienceViewModel model = new ExperienceViewModel()
            {
                Company = expInDb.Company,
                Position = expInDb.Position,
                Duration_From = expInDb.Duration_From,
                Duration_To = expInDb.Duration_To,
                Description = expInDb.Description,
                Logo = expInDb.Logo

            };


            return View(model);
        }



        [Authorize]
        public ActionResult DeleteExp(int id)
        {
            var model = _context.Experiences.Single(exp => exp.Id == id);
            _context.Experiences.Remove(model);
            _context.SaveChanges();
            return RedirectToAction("Experience");
        }

        [Authorize]
        public ActionResult Projects()
        {
            var models = _context.Projects.ToList();
            return View(models);
        }

        [Authorize]
        public ActionResult ProjectForm()
        {
            return View();
        }

        public ActionResult EditProject(int id)
        {
            var obj = _context.Projects.SingleOrDefault(m => m.Id == id);
            var status = obj.Status;
            ProjectsViewModel viewModel = new ProjectsViewModel()
            {
                Project_Name = obj.Project_Name,
                Tech_Used = obj.Tech_Used,
                Link = obj.Link,
                Image = obj.Image,
                Status = bool.Parse(obj.Status)
            };

            return View(viewModel);

        }

        [HttpPost]
        public ActionResult SaveProject(ProjectsViewModel model,HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if(file!=null)
                {
                    string path = System.IO.Path.Combine("~/Content/Images/PortfolioImages/" + file.FileName);
                    file.SaveAs(Server.MapPath(path));

                }
                if(model.Id==0)
                {
                    Project obj = new Project()
                    {
                        Project_Name=model.Project_Name,
                        Tech_Used=model.Tech_Used,
                        Status=model.Status.ToString(),
                        Link=model.Link
                    };
                    if (file != null)
                        obj.Image = file.FileName.ToString();
                    _context.Projects.Add(obj);
                    _context.SaveChanges();
                    return RedirectToAction("Projects");
                }
                else
                {
                    var projectInDb = _context.Projects.Single(p => p.Id == model.Id);
                    projectInDb.Project_Name = model.Project_Name;
                    projectInDb.Tech_Used = model.Tech_Used;
                    projectInDb.Status = model.Status.ToString();

                    if (file != null)
                        projectInDb.Image = file.FileName.ToString();
                    _context.SaveChanges();
                    return RedirectToAction("Projects");
                }
                
            }
            return View("ProjectForm",model);
        }

        public ActionResult DeleteProject(int id)
        {
           
            var model = _context.Projects.Single(m => m.Id == id);
            var fullpath = Request.MapPath("~/Content/Images/PortfolioImages/" + model.Image);

            if (System.IO.File.Exists(fullpath))
            {
                System.IO.File.Delete(fullpath);
            }
            _context.Projects.Remove(model);
            _context.SaveChanges();
            return RedirectToAction("Projects");
        }

        [Authorize]
        public ActionResult SocialMedia()
        {
            var models = _context.SocialLinks.ToList();
            return View(models);
        }

        [Authorize]
        public ActionResult CreateSocialMedia()
        {
            return View();
        }

        public ActionResult EditSocialMedia(int id)
        {
            var modelInDB = _context.SocialLinks.Single(m => m.Id == id);
            SocialLinkViewodel viewModel = new SocialLinkViewodel()
            {
                Id=modelInDB.Id,
                Name = modelInDB.Name,
                Link = modelInDB.Link,
                Icon_Class = modelInDB.Icon_Class
            };
            return View("EditSocialMedia", viewModel);

        }

        [HttpPost]
        public ActionResult SaveSocialMedia(SocialLinkViewodel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id==0)
                {
                    var obj = new SocialLink()
                    {
                        Name = model.Name,
                        Link = model.Link,
                        Icon_Class = model.Icon_Class
                    };
                    _context.SocialLinks.Add(obj);
                    _context.SaveChanges();
                    return RedirectToAction("SocialMedia");
                }
                else
                {
                    var modelInDB = _context.SocialLinks.Single(m => m.Id == model.Id);
                    modelInDB.Name = model.Name;
                    modelInDB.Link = model.Link;
                    modelInDB.Icon_Class = model.Icon_Class;
                    _context.SaveChanges();
                    return RedirectToAction("SocialMedia");
                }

            }
            return View("CreateSocialMedia", model);


        }
        
       

        public ActionResult DeleteSocialMedia(int id)
        {
            var model = _context.SocialLinks.Single(m => m.Id == id);
            _context.SocialLinks.Remove(model);
            _context.SaveChanges();
            return RedirectToAction("SocialMedia");
        }

        [Authorize]
        public ActionResult Portfolio()
        {
            var models = _context.Portfolios.ToList();
            return View(models);
        }



        public ActionResult CreatePortfolio()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreatePortfolio(PortfolioViewModel viewModel)
        {
            Portfolio obj = new Portfolio()
            {
                Type = viewModel.Type,
                Link = viewModel.Link
            };
            _context.Portfolios.Add(obj);
            _context.SaveChanges();
            return RedirectToAction("Portfolio");
        }

        public ActionResult DeletePortfolio(int id)
        {
            var model = _context.Portfolios.Single(m => m.Id == id);
            _context.Portfolios.Remove(model);
            _context.SaveChanges();
            return RedirectToAction("Portfolio");
        }

        public ActionResult Resume()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Resume(HttpPostedFileBase file)
        {
            if (file != null)
            {
                string path = System.IO.Path.Combine("/Content/CV/" + file.FileName);
                file.SaveAs(Server.MapPath(path));
                return View("Dashboard");
            }
            else
                return View();
           
        }

        public FileResult DownloadCV()
        {
            return File("~/Content/CV/Resume.pdf", "pdf", "Dawood_Resume");
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return View("Index");
        }


    }
}