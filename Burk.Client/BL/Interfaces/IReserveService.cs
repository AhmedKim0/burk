using Burk.Client.DTO;

namespace Burk.Client.BL.Interfaces
{
    public interface IReserveService
    {
		Task<string> AddReservaiton(WatinigListDto waiting);

	}
}
