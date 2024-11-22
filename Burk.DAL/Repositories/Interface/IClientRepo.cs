using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Burk.DAL.Entity;

namespace Burk.DAL.Repositories.Interface
{
    public interface IClientRepo
    {
        public Task<Client> GetClientByName(string Name);
        public Task <Client> GetClientByPhone(string phoneNumber);
        public void AddClient(Client client);
    }
}
