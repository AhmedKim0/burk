using burk.DTO;
using Burk.DAL.Entity;

namespace burk.BL.interfaces;

public interface IAccountService
{
    public bool IsClientAvaliable(string name, string phone);
    public void SaveClient( ClientDto user);
}
