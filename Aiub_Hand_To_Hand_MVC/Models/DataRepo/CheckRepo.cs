using Aiub_Hand_To_Hand_MVC.Models.Data;
using Aiub_Hand_To_Hand_MVC.Models.Interface;

namespace Aiub_Hand_To_Hand_MVC.Models.DataRepo
{
    public class CheckRepo: LoginIRrepository<Login>
    {
        public ApplicationDbContext _context;

        public CheckRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool LoginPart(Login login)
        {
            var data = _context.Logins.Where(x => x.Username.Equals(login.Username) && x.Password.Equals(login.Password));
            if (data != null)
                return true;

            return false;

        }
    }
}
