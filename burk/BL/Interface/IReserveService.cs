using Burk.DAL.Entity;
using Burk.DTO;

namespace Burk.BL.Interface;

public interface IReserveService
{
	Task<List<WaitingList>> GetWaitingListAsync();
	Task<List<AcceptedUserDTO>> GetAcceptedUserAsync();
	Task<string> AcceptUser(int id, int tablenumber);
	Task<string> UnAcceptUser(int id);
	Task RemoveWaiting(int id);
	Task RemoveAccepted(int id);
	Task<string> EditAccepted(int id, EditUserDTO userDTO);
}
