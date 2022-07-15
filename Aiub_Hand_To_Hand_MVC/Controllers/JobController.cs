using Aiub_Hand_To_Hand_MVC.Models;
using Aiub_Hand_To_Hand_MVC.Models.AccessFactory;
using Aiub_Hand_To_Hand_MVC.Models.Data;
using Aiub_Hand_To_Hand_MVC.Models.DataModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace Aiub_Hand_To_Hand_MVC.Controllers
{
    public class JobController : Controller
    {
        private ApplicationDbContext _context;
        private DataAccessFactory _db;
        private readonly IHostingEnvironment envio;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public JobController(ApplicationDbContext context, IHttpContextAccessor _httpContextAccessor, DataAccessFactory db, IHostingEnvironment envio)
        {
            _context = context;
            _db = db;
            this.envio = envio;
            this._httpContextAccessor = _httpContextAccessor;
        }

        public IActionResult Index()
        {
            List<Job> logins = _db.JobDataAccess().GetAll();
            return View(logins);
        }

        public IActionResult ShowJob(string Depertment, string DateNow)
        {


            List<Job> logins = _db.JobDataAccess().GetAll();
            if (Depertment != null || DateNow != null)
            {
                logins = _db.JobDataAccess().GetAll().Where(x => x.Depertment.Equals(Depertment) || x.DateNow.Equals(DateNow)).ToList();
            }
            return View(logins);
        }



        [HttpGet]
        public IActionResult Create()
        {
            List<SelectListItem> cities = new()
            {
                new SelectListItem { Value = "", Text = "None" },
                new SelectListItem { Value = "COA", Text = "COA" },
                new SelectListItem { Value = "DLC", Text = "DLC" },
                new SelectListItem { Value = "MAth 5", Text = "MAth 5" },
                new SelectListItem { Value = "ENG", Text = "ENG" }
            };
            ViewBag.cities = cities;

            return View();
        }

        [HttpPost]
        public IActionResult Create(JobModel j)
        {
            if (ModelState.IsValid)
            {

                int dw = HttpContext.Session.GetInt32("Username1").Value;
                var data = new Job()
                {
                    Depertment = j.Depertment,
                    Jobtitle = j.Jobtitle,
                    Message = j.Message,
                    DateNow = DateTime.Now,
                    Exparied = j.Exparied,
                    LoginId = dw


                };

                _db.JobDataAccess().Add(data);
                return RedirectToAction("Profile","Login");
            }

            return View(j);
        }

        public IActionResult DateJobList()
        {
            List<Job> logins = _db.JobDataAccess().GetAll();
            return View(logins);
        }

        public IActionResult JobListDelete()
        {
            int dw = HttpContext.Session.GetInt32("Username1").Value;
            List<Job> job = _db.JobDataAccess().GetAll().Where(x=>x.LoginId==dw).ToList();
            return View(job);
        }

    }

}
