using Aiub_Hand_To_Hand_MVC.Models.Data;
using Aiub_Hand_To_Hand_MVC.Models.Interface;

namespace Aiub_Hand_To_Hand_MVC.Models.DataRepo
{
    public class JobRepo : IRrepository<Job, int>

    {
        public ApplicationDbContext _context;

        public JobRepo(ApplicationDbContext context)
        {
            _context = context;
        }


        public void Add(Job item)
        {
            _context.Jobs.Add(item);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var data = _context.Jobs.Where(x => x.Id == id).FirstOrDefault();
            _context.Jobs.Remove(data);
            _context.SaveChanges();
        }

        public void Edit(Job item)
        {
            throw new NotImplementedException();
        }

        public List<Job> GetAll()
        {
            return _context.Jobs.ToList();
        }

        public Job GetId(int id)
        {
            throw new NotImplementedException();
        }
    }
}
