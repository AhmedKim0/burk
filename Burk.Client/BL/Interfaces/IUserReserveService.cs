using Burk.Client.DTO;

namespace Burk.Client.BL.Interfaces
{
    public interface IUserReserveService
	{
		Task<string> AddReservaiton(WatinigListDto waiting);

	}
}
