using AutoMapper;

using Burk.BL.Interface;
using Burk.DAL.Context;
using Burk.DAL.Entity;
using Burk.DAL.Entity.Enums;
using Burk.DAL.Repository.Interface;
using Burk.DTO;

using Microsoft.EntityFrameworkCore;

using System.ComponentModel.DataAnnotations;

namespace Burk.BL.Imp;

public class ReserveService: IReserveService
{
	private readonly IAsyncRepository<WaitingList> _waitngRepo;
	private readonly IAsyncRepository<RecordedVisit> _tempRepo;
	private readonly IAsyncRepository<Burk.DAL.Entity.Client> _clientRepo;
	private readonly IMapper _mapper;
	//private readonly BurkDbContext _burkDbContext;

	public ReserveService(IAsyncRepository<WaitingList> waitngRepo, IAsyncRepository<RecordedVisit> tempRepo, IMapper mapper
		, IAsyncRepository<Burk.DAL.Entity.Client> clientRepo)/*, BurkDbContext burkDbContext)*/
	{
		_waitngRepo = waitngRepo ?? throw new ArgumentNullException(nameof(waitngRepo));
		_tempRepo = tempRepo ?? throw new ArgumentNullException(nameof(tempRepo));
		_mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
		_clientRepo=clientRepo ?? throw new ArgumentNullException(nameof(clientRepo));
		//_burkDbContext = burkDbContext;
	}

	public async Task<List<WaitingList>> GetWaitingListAsync()
		{
		return await _waitngRepo.ListAllAsync(false);

	}
	public async Task<List<WaitingList>> GetAcceptedUserAsync()
	{
		return await _waitngRepo.ListAsync(x => x.IsAccepted == true, false);

	}


	public async Task<string> AcceptUser(int id  ,int tablenumber)

	{
		WaitingList waitingUser = await _waitngRepo.FirstOrDefaultAsync(x => x.Id == id, false);
 
		var client = await _clientRepo.FirstOrDefaultAsync(i=>i.PhoneNumber== waitingUser.PhoneNumber);
		if (!(waitingUser == null && client==null))
		{
			waitingUser.TableNumber=tablenumber;
			waitingUser.IsAccepted = true;


		
			await _waitngRepo.UpdateAsync(waitingUser);

			return "done";
			}
		return "client is not registed or client reservation not found";

	}
	public async Task<string> UnAcceptUser(int id)
	{

		var accepted = await _waitngRepo.FirstOrDefaultAsync(c => c.Id == id,false);
		if (accepted != null) { 
		accepted.IsAccepted=false;
		accepted.TableNumber=null;

		await _waitngRepo.UpdateAsync(accepted); 
			return "done"; }
			
		return "not found client or accepted user";
	

	}
	public async Task<string> ConfirmUser(int id, int tablenumber)

	{
		WaitingList waitingUser = await _waitngRepo.FirstOrDefaultAsync(x => x.Id == id, false);
		if (waitingUser == null) { return "invalid waitinglist";}
		var client = await _clientRepo.FirstOrDefaultAsync(i => i.PhoneNumber == waitingUser.PhoneNumber, false);
		if (!(waitingUser == null && client == null))
		{
			waitingUser.TableNumber = tablenumber;
			waitingUser.IsAccepted = true;
			waitingUser.IsConfirmed= ClientState.Confirmed;



			await _waitngRepo.UpdateAsync(waitingUser);

			return "done";
		}
		return "client is not registed or client reservation not found";

	}
	public async Task<string> UnConfirmUser(int id)
	{
		//var accpeted = await _acceptRepo.FirstOrDefaultAsync(x => x.Id == id);
		var Confirmed = await _waitngRepo.FirstOrDefaultAsync(c => c.Id == id,false);
		if (Confirmed.IsConfirmed== ClientState.Confirmed)
		{
			Confirmed.IsAccepted = false;
			Confirmed.TableNumber = null;
			Confirmed.IsConfirmed = 0;

			await _waitngRepo.UpdateAsync(Confirmed);
			return "done";
		}

		return "not found client or Confirmed user";


	}


	public async Task<string> CancelUserWaiting(int id,bool Isleaving=false)// make if false allways
	{
		var user = await _waitngRepo.FirstOrDefaultAsync(w => w.Id == id, false);
		if (user != null) {
		if(Isleaving )
		{
			RecordedVisit temp = new RecordedVisit()
			{
				AttendanceTime = user.AttendanceTime,
				ReservationTime = user.ReservationTime,
				Smoking = user.Smoking,
				area = user.area,
				TableNumber = (int)user.TableNumber,
				ClientId = user.ClientId,
				Visitors = user.Visitors,




			};
			await _tempRepo.AddAsync(temp);
			await _waitngRepo.DeleteAsync(user);
				return "done";
		}
		user.IsConfirmed= ClientState.Canceled;
		await _waitngRepo.UpdateAsync(user);
			return "done";
		}
		return "failed";
	}

	public async Task<string> EditAccepted(int id ,EditUserDTO userDTO)
	{
		
		var user = await _waitngRepo.FirstOrDefaultAsync(u=>u.Id==id, false);
		if (!(user == null)) { 
		user.AttendanceTime = userDTO.AttendanceTime;
		user.area=userDTO.area;
			user.Smoking = userDTO.Smoking;
		user.TableNumber=userDTO.TableNumber;
		user.Visitors=userDTO.Visitors;
		await _waitngRepo.UpdateAsync(user);
			return"done";}
		return "user not found";



		


	}



}
