using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Burk.DAL.Context;
using Burk.DAL.Entity;
using Burk.DAL.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace Burk.DAL.Repositories.imp
{
    public class ClientRepository:Repository<Client>,IClientRepo,IRepository<Client>
    {
        private readonly BurkDbContext _context;
        public ClientRepository(BurkDbContext context):base(context) 
        {
            _context = context;
        }

        public async Task<Client> GetClientByName(string name)
        {
            return await _context.Clients.FirstOrDefaultAsync(c=>c.Name==name);
        }

        public async Task<Client> GetClientByPhone(string phoneNumber)
        {
            return await _context.Clients.FirstOrDefaultAsync(c => c.PhoneNumber == phoneNumber);
        }
        public void AddClient(Client client)
        {
            _context.Clients.Add(client);
            _context.SaveChanges();
        }
    }
}
