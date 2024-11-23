using Burk.DAL.Entity;
using Burk.DTO;

namespace Burk.BL.Interface;

public interface IReserveService
{
	Task<List<WaitingList>> GetWaitingListAsync();
	Task<List<AcceptedUserDTO>> GetAcceptedUserAsync();
	Task<string> AcceptUser(int id, int tablenumber);
	Task<string> UnAcceptUser(int id);
	Task RemoveUserWaiting(int id, bool Isleaving);

	Task<string> EditAccepted(int id, EditUserDTO userDTO);
}
