using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burk.DAL.Repositories.Interface
{
    public interface IRepository<T> where T : class
    {
        public  Task<IEnumerable<T>> GetAll();
        public Task<T> GetById(int id);
        

    }
}
