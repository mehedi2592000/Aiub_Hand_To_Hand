namespace Aiub_Hand_To_Hand_MVC.Models.Interface
{
    public interface IRrepository<T,id>
    {
        List<T> GetAll();

        T GetId(id id);
        void Add(T item);
        void Edit(T item);

        void Delete(id id);

    }
}
