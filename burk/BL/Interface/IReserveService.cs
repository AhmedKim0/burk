using Burk.DAL.Entity;
using Burk.DTO;

namespace Burk.BL.Interface;

public interface IReserveService
{
	Task<List<WaitingList>> GetWaitingListAsync();
	Task<List<WaitingList>> GetAcceptedUserAsync();
	Task<string> AcceptUser(int id, int tablenumber);
	Task<string> UnAcceptUser(int id);
	Task<string> ConfirmUser(int id, int tablenumber);
	Task<string> UnConfirmUser(int id);

	Task<string> CancelUserWaiting(int id, bool Isleaving);

	Task<string> EditAccepted(int id, EditUserDTO userDTO);
}
