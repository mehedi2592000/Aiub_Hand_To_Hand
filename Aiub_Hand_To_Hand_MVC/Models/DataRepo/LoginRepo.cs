using Aiub_Hand_To_Hand_MVC.Models.Data;
using Aiub_Hand_To_Hand_MVC.Models.Interface;
using Microsoft.EntityFrameworkCore;

namespace Aiub_Hand_To_Hand_MVC.Models.DataRepo
{
    public class LoginRepo:IRrepository<Login,int>
    {
        public ApplicationDbContext _context;

        public LoginRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(Login item)
        {
            _context.Logins.Add(item);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var data = _context.Logins.Where(x => x.Id == id).FirstOrDefault();
            _context.Logins.Remove(data);
            _context.SaveChanges();
        }

        public void Edit(Login item)
        {
            //var data = _context.Logins.Where(x => x.Id == item.Id).FirstOrDefault();
            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public List<Login> GetAll()
        {
            return _context.Logins.ToList();
        }

        public Login GetId(int id)
        {
            var data = _context.Logins.Where(x => x.Id == id).FirstOrDefault();
            return data;
        }

    }
}
