using Aiub_Hand_To_Hand_MVC.Models.Data;
using Aiub_Hand_To_Hand_MVC.Models.Interface;
using Microsoft.EntityFrameworkCore;

namespace Aiub_Hand_To_Hand_MVC.Models.DataRepo
{
    public class RepositoryRepo : IRrepository<Repository, int>
    {

        public ApplicationDbContext _context;

        public RepositoryRepo(ApplicationDbContext context)
        {
            _context = context; 
        }

        public void Add(Repository item)
        {
            _context.Repositories.Add(item);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var data = _context.Repositories.Where(x => x.ID == id).FirstOrDefault();
            _context.Repositories.Remove(data);
            _context.SaveChanges();
        }

        public void Edit(Repository item)
        {
            //var data = _context.Repositories.Where(x => x.ID == item.ID).FirstOrDefault();
            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();
            

        }

        public List<Repository> GetAll()
        {
            return _context.Repositories.ToList();
        }

        public Repository GetId(int id)
        {
            return _context.Repositories.Where(x => x.ID == id).FirstOrDefault();
        }
    }
}
