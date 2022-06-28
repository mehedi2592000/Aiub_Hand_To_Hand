using Aiub_Hand_To_Hand_MVC.Models;
using Aiub_Hand_To_Hand_MVC.Models.AccessFactory;
using Aiub_Hand_To_Hand_MVC.Models.Data;
using Aiub_Hand_To_Hand_MVC.Models.DataModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
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
        string MailBody = "Wlcomwe to the questain";

        string mailtitle = "Givr change";
        string mailSubject = "Email with the";
        string fromEmail = "bd34017053@gmail.com";
        string password = "qiovdgghfkejmipa";


        [HttpGet]
        public IActionResult UserLogin()
        {
            
            return View();
        }
        [HttpPost]
        public IActionResult UserLogin(Login lm)
        {
           /* var err = new Login()
            {
                Username = lm.Username,
                Password = lm.Password
            };
*/            //var dat = _db.LOginCheckDataAccess().LoginPart(err);   
           //  var dat = _db.LoginDataAccessFactory().GetAll().Where(x => x.Username.Equals(err.Username) && x.Password.Equals(err.Password));
            
            
            //if(dat!=0)
           // {
                var data = _db.LoginDataAccessFactory().GetAll().Where(x => x.Username.Equals(lm.Username) && x.Password.Equals(lm.Password)).Select(w => w.Id).ToList();
                int p=0;
                

                if (data != null)
                {
                    foreach (int roleN in data)
                    {
                         p= roleN;
                    }
                }
           
            
            if (p != 0)
            {
                var er = data;

                var d = lm.Username;

                HttpContext.Session.SetString("Username", lm.Username);
                // HttpContext.Session.SetString("Username1", p);
                HttpContext.Session.SetInt32("Username1", p);
                return RedirectToAction("Index");
                // return View();
            }
            
                   
           // }
            return View(lm);
        }
        public IActionResult Logout()
        {
             HttpContext.Session.Remove("Username");
             
             HttpContext.Session.Remove("Username1");
            return RedirectToAction("Index");
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
                if (lm.Picture != null)
                {
                    var fullpath = Path.Combine(envio.ContentRootPath, @"wwwroot\Pic\", lm.Picture.FileName);
                    FileStream stream = new FileStream(fullpath, FileMode.Create);
                    lm.Picture.CopyTo(stream);

                    // string d = HttpContext.Session.GetString("Username");
                    //  var dr = HttpContext.Session.GetString("Username");
                    //int dw= HttpContext.Session.GetInt32("Username1").Value;



                    var data = new Login()
                    {
                        Name = lm.Name,
                        Gmail = lm.Gmail,
                        Instutue = lm.Instutue,
                        //Username = HttpContext.Session.GetString("Username"),
                        //Username= HttpContext.Session.GetString("Username"),
                        Username = lm.Username,
                        Password = lm.Password,
                        Picture = lm.Picture.FileName

                    };
                    _db.LoginDataAccessFactory().Add(data);
                }
                else
                {
                    var data = new Login()
                    {
                        Name = lm.Name,
                        Gmail = lm.Gmail,
                        Instutue = lm.Instutue,
                        //Username = HttpContext.Session.GetString("Username"),
                        //Username= HttpContext.Session.GetString("Username"),
                        Username = lm.Username,
                        Password = lm.Password
                        

                    };
                    _db.LoginDataAccessFactory().Add(data);

                }
               
                //EMail
               

                MailMessage message = new MailMessage(new MailAddress(fromEmail, mailtitle), new MailAddress(lm.Gmail));

                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("<div class='row'>");
                sb.AppendFormat("<div class='col-sm-4'>");
                sb.AppendFormat("<div class='card'>");
                sb.AppendFormat("<img src='{0}' class='card-img-top'>", lm.Picture);
                sb.AppendFormat("<div class='card-body'>");
                sb.AppendFormat("<h4 Card-title>{0} {1}</h4>", new object[] { lm.Username, lm.Password });
                sb.AppendFormat("</div>");
                sb.AppendFormat("</div>");
                sb.AppendFormat("</div>");
                sb.AppendFormat("</div>"); message.Subject = mailSubject;
                message.Body = sb.ToString();
                message.IsBodyHtml = true;

                //message.Attachments.Add(new Attachment(filetoAttach.OpenReadStream(), filetoAttach.FileName));

                if (lm.Picture != null) { 
                //message.Attachments.Add(new Attachment("Password", lm.Password));
                message.Attachments.Add(new Attachment(lm.Picture.OpenReadStream(), lm.Picture.FileName));
                    }


                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

                System.Net.NetworkCredential credential = new System.Net.NetworkCredential();
                credential.UserName = fromEmail;
                credential.Password = password;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = credential;
                smtp.Send(message);

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


        public IActionResult Profile()
        {
            string Uu = HttpContext.Session.GetString("Username");
            var logins = _db.LoginDataAccessFactory().GetAll().Where(x=>x.Username.Equals(Uu));
            return View(logins);

        }
    }
}

