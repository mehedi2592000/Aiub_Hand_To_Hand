using Aiub_Hand_To_Hand_MVC.Models;
using Aiub_Hand_To_Hand_MVC.Models.AccessFactory;

namespace Aiub_Hand_To_Hand_MVC.Data_Logic_LAyer
{
    public class LoginLogic
    {

        public  DataAccessFactory _db;

        public LoginLogic(DataAccessFactory db)
        {
            _db = db;
        }

        public  List<Login> GetAllLogin()
        {
            List<Login> logins = _db.LoginDataAccessFactory().GetAll();
            return logins;  
        }

        public  List<Repository> GetAllRepoSItory()
        {
            List<Repository>rep= _db.RepositoryDataAccessFactory().GetAll();
            return rep;
        }
    }
}
