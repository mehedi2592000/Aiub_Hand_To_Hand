using Aiub_Hand_To_Hand_MVC.Models;
using Aiub_Hand_To_Hand_MVC.Models.AccessFactory;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Aiub_Hand_To_Hand_MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        
        private DataAccessFactory _db;
        

        public HomeController(ILogger<HomeController> logger, DataAccessFactory db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {
            


            return View();
        }

        [HttpPost]
        public List<object> Indexee()
        {
            //List<Repository> data = new List<Repository>();
            List<object> data = new List<object>();
           List<string>  sub = _db.RepositoryDataAccessFactory().GetAll().Select(x => x.Subject).ToList();
            data.Add(sub);
            var pp = _db.RepositoryDataAccessFactory().GetAll().Select(x => x.Count).ToList();
            data.Add(pp);

            return data; 
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}