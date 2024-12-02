using Burk.DAL.Entity;
using Burk.DAL.ResponseModel;
using Burk.DTO;

namespace Burk.BL.Interface;

public interface IReserveService
{
	Task<Response<List<WaitingList>>> GetWaitingListAsync();
	//Task<List<WaitingList>> GetAcceptedUserAsync();
	//Task<bool> AcceptUser(int id, int tablenumber);
	//Task<bool> UnAcceptUser(int id);
	Task<Response<bool>> ConfirmUser(int id, int tablenumber);
	Task<Response<bool>> UnConfirmUser(int id);

	Task<Response<bool>> CancelUserWaiting(int id, bool Isleaving);

	Task<Response<bool>> EditAccepted(int id, EditUserDTO userDTO);
}
