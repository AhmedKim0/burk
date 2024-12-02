using AutoMapper;



using Burk.BL.Interface;
using Burk.DAL.Context;
using Burk.DAL.Entity;
using Burk.DAL.Entity.Enums;
using Burk.DAL.Repository.Interface;
using Burk.DAL.ResponseModel;
using Burk.DTO;

using Microsoft.EntityFrameworkCore;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Burk.BL.Imp;

public class ReserveService : IReserveService
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

	public async Task<Response<List<WaitingList>>> GetWaitingListAsync()
	{
		Response<List<WaitingList>> res = new Response<List<WaitingList>>(new List<WaitingList>());

		try
		{ 
		res.Data= await _waitngRepo.ListAllAsync(false);
		return res; 
			}
		catch (Exception ex)
		{

			res.Errors.Add(new Error { Message = ex.Message, ErrorCode = "500" });
			return res;

		}

	}
	//public async Task<List<WaitingList>> GetAcceptedUserAsync()
	//{
	//	return await _waitngRepo.ListAsync(x => x.IsAccepted == true, false);

	//}


	//public async Task<bool> AcceptUser(int id  ,int tablenumber)

	//{
	//	WaitingList waitingUser = await _waitngRepo.FirstOrDefaultAsync(x => x.Id == id, false);
 
	//	var client = await _clientRepo.FirstOrDefaultAsync(i=>i.PhoneNumber== waitingUser.PhoneNumber);
	//	if (!(waitingUser == null && client==null))
	//	{
	//		waitingUser.TableNumber=tablenumber;
	//		waitingUser.IsAccepted = true;


		
	//		await _waitngRepo.UpdateAsync(waitingUser);

	//		return "done";
	//		}
	//	return "client is not registed or client reservation not found";

	//}
	//public async Task<bool> UnAcceptUser(int id)
	//{

	//	var accepted = await _waitngRepo.FirstOrDefaultAsync(c => c.Id == id,false);
	//	if (accepted != null) { 
	//	accepted.IsAccepted=false;
	//	accepted.TableNumber=null;

	//	await _waitngRepo.UpdateAsync(accepted); 
	//		return "done"; }
			
	//	return "not found client or accepted user";
	

	//}
	public async Task<Response<bool>> ConfirmUser(int id, int tablenumber)

	{
		Response<bool> res = new Response<bool>(default(bool));



		WaitingList waitingUser = await _waitngRepo.FirstOrDefaultAsync(x => x.Id == id, false);
		try { 		if (waitingUser == null)
		{
			res.Errors.Add(new Error { Message = "not found user", ErrorCode = "400" });
			return res;

		}
		var client = await _clientRepo.FirstOrDefaultAsync(i => i.PhoneNumber == waitingUser.PhoneNumber, false);
		if (!(waitingUser == null && client == null))
		{
			waitingUser.TableNumber = tablenumber;
			waitingUser.IsAccepted = true;
			waitingUser.IsConfirmed= ClientState.Confirmed;



			await _waitngRepo.UpdateAsync(waitingUser);
			res.Data=true;

			return res;
		}
		res.Errors.Add(new Error { Message = "client is not registed or client reservation not found", ErrorCode = "400" });
		return res; 
			}
		catch (Exception ex)
		{

			res.Errors.Add(new Error { Message = ex.Message, ErrorCode = "500" });
			return res;

		}

	}
	public async Task<Response<bool>> UnConfirmUser(int id)
	{
		Response<bool> res = new Response<bool>(default(bool));
		//var accpeted = await _acceptRepo.FirstOrDefaultAsync(x => x.Id == id);	
		try{

		var Confirmed = await _waitngRepo.FirstOrDefaultAsync(c => c.Id == id,false);
		if (Confirmed.IsConfirmed== ClientState.Confirmed)
		{
			Confirmed.IsAccepted = false;
			Confirmed.TableNumber = null;
			Confirmed.IsConfirmed = 0;

			await _waitngRepo.UpdateAsync(Confirmed);
			res.Data=true;
			return res;
		}
		res.Errors.Add(new Error { Message = "not found client or not Confirmed user", ErrorCode = "400" });

		return res ; }
		catch (Exception ex)
		{

			res.Errors.Add(new Error { Message = ex.Message, ErrorCode = "500" });
			return res;

		}


	}


	public async Task<Response<bool>> CancelUserWaiting(int id,bool Isleaving=false)// make if false allways
	{
		Response<bool> res = new Response<bool>(default(bool));
		try { 

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
				res.Data= true;
				return res;
		}
		user.IsConfirmed= ClientState.Canceled;
		await _waitngRepo.UpdateAsync(user);
			res.Data= true;
			return res;
		}
		res.Data= false;
		return res; }
		catch (Exception ex)
		{

			res.Errors.Add(new Error { Message = ex.Message, ErrorCode = "500" });
			return res;

		}
	}

	public async Task<Response<bool>> EditAccepted(int id ,EditUserDTO userDTO)
	{
		Response<bool> res = new Response<bool>(default(bool));
		try { 

		var user = await _waitngRepo.FirstOrDefaultAsync(u=>u.Id==id, false);
		if (!(user == null)) { 
		user.AttendanceTime = userDTO.AttendanceTime;
		user.area=userDTO.area;
			user.Smoking = userDTO.Smoking;
		user.TableNumber=userDTO.TableNumber;
		user.Visitors=userDTO.Visitors;
		await _waitngRepo.UpdateAsync(user);
			res.Data = true;
			return res;
			}
		res.Data= false;
		return res; }
		catch (Exception ex)
		{

			res.Errors.Add(new Error { Message = ex.Message, ErrorCode = "500" });
			return res;

		}






	}



}
