using Aiub_Hand_To_Hand_MVC.Models.Data;
using Aiub_Hand_To_Hand_MVC.Models.DataRepo;
using Aiub_Hand_To_Hand_MVC.Models.Interface;

namespace Aiub_Hand_To_Hand_MVC.Models.AccessFactory
{
    public class DataAccessFactory
    {
        public readonly ApplicationDbContext _context;

        public DataAccessFactory(ApplicationDbContext context)
        {
            _context = context;
        }

        public  IRrepository<Login,int> LoginDataAccessFactory()
        {
            return new LoginRepo(_context);
        }

    }
}
