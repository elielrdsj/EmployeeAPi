using NovoProjeto.Models;

namespace NovoProjeto.Interfaces
{
    public interface IRepository
    {
        public void Insert(List<Employee> list);
        public int GetLastId();
        public List<Employee> Get(int page, int maxResults);
    }
}
