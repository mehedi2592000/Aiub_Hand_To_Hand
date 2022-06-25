using Aiub_Hand_To_Hand_MVC.Models;
using Aiub_Hand_To_Hand_MVC.Models.AccessFactory;
using Aiub_Hand_To_Hand_MVC.Models.Data;
using Microsoft.AspNetCore.Mvc;

namespace Aiub_Hand_To_Hand_MVC.Controllers
{
    public class LoginController : Controller
    {

        private ApplicationDbContext _context;
        private DataAccessFactory _db;
        public LoginController(ApplicationDbContext context, DataAccessFactory db)
        {
            _context = context;
            _db = db;
        }

        public IActionResult Index()
        {
            
            List<Login> logins = _db.LoginDataAccessFactory().GetAll();
            return View(logins);
        }
    }
}
