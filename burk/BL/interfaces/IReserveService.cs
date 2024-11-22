using Burk.DAL.Entity;

namespace burk.BL.interfaces
{
    public interface IReserveService
    {
        public void reserve(int TableNo ,Client client);
    }
}
