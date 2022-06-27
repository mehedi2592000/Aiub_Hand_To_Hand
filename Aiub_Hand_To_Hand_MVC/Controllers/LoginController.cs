using Aiub_Hand_To_Hand_MVC.Models;
using Aiub_Hand_To_Hand_MVC.Models.AccessFactory;
using Aiub_Hand_To_Hand_MVC.Models.Data;
using Aiub_Hand_To_Hand_MVC.Models.DataModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace Aiub_Hand_To_Hand_MVC.Controllers
{
    public class LoginController : Controller
    {

        private ApplicationDbContext _context;
        private DataAccessFactory _db;
        private readonly IHostingEnvironment envio;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoginController(ApplicationDbContext context, IHttpContextAccessor _httpContextAccessor, DataAccessFactory db,IHostingEnvironment envio)
        {
            _context = context;
            _db = db;
            this.envio = envio;
            this._httpContextAccessor=_httpContextAccessor;
        }


        [HttpGet]
        public IActionResult UserLogin()
        {
            
            return View();
        }
        [HttpPost]
        public IActionResult UserLogin(LoginModel lm)
        {
            var data = _db.LoginDataAccessFactory().GetAll().Where(x => x.Username.Equals(lm.Username) && x.Password.Equals(lm.Password)).Select(w=>w.Id).ToList();
                

            if(data!=null)
            {

                int p=0;
                ViewBag.userId = data;

                if (data != null)
                {
                    foreach (int roleN in data)
                    {
                         p= roleN;
                    }
                }
                // int t = Convert.ToInt32(data);
                var er = data;
                
                var d = lm.Username;

                HttpContext.Session.SetString("Username",lm.Username);
               // HttpContext.Session.SetString("Username1", p);
                HttpContext.Session.SetInt32("Username1", p);
                return RedirectToAction("Index");
               // return View();
                   
            }
            return View(lm);
        }

        public IActionResult Index()
        {
            
            List<Login> logins = _db.LoginDataAccessFactory().GetAll();
            var use = HttpContext.Session.GetString("Username");
            var use1 = HttpContext.Session.GetString("Username1");
            return View(logins);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(LoginModel lm)
        {
            if (ModelState.IsValid)
            {
                
                    var fullpath = Path.Combine(envio.ContentRootPath, @"wwwroot\Pic\", lm.Picture.FileName);
                    FileStream stream = new FileStream(fullpath, FileMode.Create);
                    lm.Picture.CopyTo(stream);

                string d = HttpContext.Session.GetString("Username");
              //  var dr = HttpContext.Session.GetString("Username");
               int dw= HttpContext.Session.GetInt32("Username1").Value;

                

                var data = new Login()
                {
                    Name = lm.Name,
                    Gmail = lm.Gmail,
                    Instutue = lm.Instutue,
                    //Username = HttpContext.Session.GetString("Username"),
                    //Username= HttpContext.Session.GetString("Username"),
                   Username=d,
                    Password = lm.Password,
                    Picture = lm.Picture.FileName

                };
                _db.LoginDataAccessFactory().Add(data);
                return RedirectToAction("Index");
            }
            else
            {

                return View(lm);

            }
        }


        //Picture Save 

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var lm=_db.LoginDataAccessFactory().GetId(id);
  
           
                var data = new LoginModel()
                {
                    Id = lm.Id,
                    Name = lm.Name,
                    Gmail = lm.Gmail,
                    Instutue = lm.Instutue,
                    Username = lm.Username,
                    Password = lm.Password,
            
                   // Picture = new FormFile(stream, 0, stream.Length, null, lm.Picture)
                    // Picture = new FormFile(s, 0, y, null, Path.GetFileName(s.Name))

                };
                return View(data);
            
        }
        [HttpPost]
        public IActionResult Edit(LoginModel lm)
        {
            if (ModelState.IsValid)
            {

                var fullpath = Path.Combine(envio.ContentRootPath, @"wwwroot\Pic\", lm.Picture.FileName);
                FileStream stream = new FileStream(fullpath, FileMode.Create);
                lm.Picture.CopyTo(stream);

                var data = new Login()
                {
                    Id = lm.Id,
                    Name = lm.Name,
                    Gmail = lm.Gmail,
                    Instutue = lm.Instutue,
                    Username = lm.Username,
                    Password = lm.Password,
                    Picture = lm.Picture.FileName
                };
                // _context.Entry(data).State = EntityState.Modified;
                //_context.SaveChanges();
                _db.LoginDataAccessFactory().Edit(data);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Edit");
        }

        [HttpGet]
        public IActionResult EditSetting(int id)
        {
            var lm = _db.LoginDataAccessFactory().GetId(id);
            return View(lm);
        }

        [HttpPost]
        public IActionResult EditSetting(Login lm)
        {
            
            _db.LoginDataAccessFactory().Edit(lm);
            return RedirectToAction("Index");
        }


        public IActionResult Delete(int id)
        {
            _db.LoginDataAccessFactory().Delete(id);
            return RedirectToAction("Index");
        }
    }
}

