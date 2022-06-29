using Aiub_Hand_To_Hand_MVC.Models;
using Aiub_Hand_To_Hand_MVC.Models.AccessFactory;
using Aiub_Hand_To_Hand_MVC.Models.Data;
using Aiub_Hand_To_Hand_MVC.Models.DataModel;
using Microsoft.AspNetCore.Mvc;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

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


        [HttpGet]
        public IActionResult Create()
        {


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
                return RedirectToAction("Index");
            }

            return View("Create");
        }

        public IActionResult Delete(int id)
        {
            _db.RepositoryDataAccessFactory().Delete(id);
            return RedirectToAction("Index");
        }
    }
}
