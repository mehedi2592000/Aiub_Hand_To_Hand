using Aiub_Hand_To_Hand_MVC.Models;
using Aiub_Hand_To_Hand_MVC.Models.AccessFactory;
using Aiub_Hand_To_Hand_MVC.Models.Data;
using Aiub_Hand_To_Hand_MVC.Models.DataModel;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using Document = iTextSharp.text.Document;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Aiub_Hand_To_Hand_MVC.Controllers
{
    public class RepositoryController : Controller
    {

        private ApplicationDbContext _context;
        private DataAccessFactory _db;
        private readonly IHostingEnvironment envio;


        public RepositoryController(ApplicationDbContext context, DataAccessFactory db, IHostingEnvironment envio)
        {
            _context = context;
            _db = db;
            this.envio = envio;
        }
        public IActionResult Index()
        {
            List<Repository> logins = _db.RepositoryDataAccessFactory().GetAll();

            return View(logins);
        }

        public IActionResult ShowIndex(string name,string title)
        {
            

            List<Repository> logins = _db.RepositoryDataAccessFactory().GetAll();
            if(name!=null ||title!=null)
            {
                logins= _db.RepositoryDataAccessFactory().GetAll().Where(x=>x.Subject.Equals(name) || x.Title.Equals(title)).ToList();
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
        public IActionResult Create(RepositoryModel lm)
        {
            if (ModelState.IsValid)
            {

                var fullpath = Path.Combine(envio.ContentRootPath, @"wwwroot\PDF\", lm.Pdf.FileName);
                FileStream stream = new FileStream(fullpath, FileMode.Create);
                lm.Pdf.CopyTo(stream);
                int dw = HttpContext.Session.GetInt32("Username1").Value;
                var data = new Repository()
                {
                    Subject = lm.Subject,
                    Status = lm.Status,
                    Title = lm.Title,
                    Pdf = lm.Pdf.FileName,
                    Date = DateTime.Now,
                    Count = 0,
                    Personal = lm.Personal,
                    LoginId=dw

                };
                _db.RepositoryDataAccessFactory().Add(data);
                return RedirectToAction("Profile","Login");
            }

            return View("Create");
        }

        public IActionResult Delete(int id)
        {
            _db.RepositoryDataAccessFactory().Delete(id);
            return RedirectToAction("Index");
        }

        public IActionResult GetPdf(string Pdf)
        {
            var lm = _db.RepositoryDataAccessFactory().GetAll().FirstOrDefault(xc => xc.Pdf.Contains(Pdf));
            lm.Count +=1;

           
            _db.RepositoryDataAccessFactory().Edit(lm);

            var memory = PdfDemo(Pdf, "wwwroot\\PDF");
            return File(memory.ToArray(), "application/pdf", Pdf);

        }
        public  MemoryStream PdfDemo(string filename,string uploadpath)
        {
            var path=Path.Combine(Directory.GetCurrentDirectory(),uploadpath, filename);
            var memory = new MemoryStream();
            if(System.IO.File.Exists(path))
            {
                var net = new System.Net.WebClient();
                var data=net.DownloadData(path);
                var content = new System.IO.MemoryStream(data);
                memory = content;

            }
            memory.Position = 0;
            return memory;
        }

        public IActionResult DeleteRepo()
        {
            int dw = HttpContext.Session.GetInt32("Username1").Value;

            List<Repository> logins = _db.RepositoryDataAccessFactory().GetAll().Where(x=>x.LoginId==dw).ToList();
            return View(logins);

        }


    }
}
