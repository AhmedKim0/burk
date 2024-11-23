using AutoMapper;

using Burk.DAL.Context;
using Burk.DAL.Entity;
using Burk.DAL.Repository.Interface;
using Burk.DTO;

using Microsoft.EntityFrameworkCore;

using System.ComponentModel.DataAnnotations;

namespace Burk.BL.Imp;

public class ReserveService
{
	private readonly IAsyncRepository<WaitingList> _waitngRepo;
	private readonly IAsyncRepository<AcceptedUser> _acceptRepo;
	private readonly IAsyncRepository<Burk.DAL.Entity.Client> _clientRepo;
	private readonly IMapper _mapper;
	//private readonly BurkDbContext _burkDbContext;

	public ReserveService(IAsyncRepository<WaitingList> waitngRepo, IAsyncRepository<AcceptedUser> acceptRepo, IMapper mapper
		, IAsyncRepository<Burk.DAL.Entity.Client> clientRepo)/*, BurkDbContext burkDbContext)*/
	{
		_waitngRepo = waitngRepo ?? throw new ArgumentNullException(nameof(waitngRepo));
		_acceptRepo = acceptRepo ?? throw new ArgumentNullException(nameof(acceptRepo));
		_mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
		_clientRepo=clientRepo ?? throw new ArgumentNullException(nameof(clientRepo));
		//_burkDbContext = burkDbContext;
	}

	public async Task<List<WaitingList>> GetWaitingListAsync()
		{
		return await _waitngRepo.ListAllAsync(false);

	}
	public async Task<List<AcceptedUserDTO>> GetAcceptedUserAsync()
	{
		List<AcceptedUserDTO> DTOs = new List<AcceptedUserDTO>();
		List<AcceptedUser> accpetedUsers = await _acceptRepo.ListAllAsync(false);
		foreach (var user in accpetedUsers)
		{
			AcceptedUserDTO dto = new AcceptedUserDTO()
			{
				Id = user.Id,
				Clientname = user.Client.Name,
				TableNumber = user.TableNumber,
				Visitors = user.Visitors,
				ReservationTime = user.ReservationTime,
				AttendanceTime = user.AttendanceTime,

				area = user.area,
				Smoking = user.Smoking
			};

			DTOs.Add(dto);

		}
		return DTOs;
	}


	public async Task<string> AcceptUser(int id  ,int tablenumber)

	{
		WaitingList waitingUser = await _waitngRepo.FirstOrDefaultAsync(x => x.Id == id);
 
		var client = await _clientRepo.FirstOrDefaultAsync(i=>i.PhoneNumber== waitingUser.PhoneNumber);
		if (!(waitingUser == null && client==null))
		{
			//var user= new()
			//{
			//	id=client.Id,
			//	tablenumber=tablenumber,
			//};
			AcceptedUser user = new AcceptedUser
		{
			ClientId=client.Id,
			TableNumber=tablenumber,
			Visitors=waitingUser.Visitors,
			WaitingListId=id,
				ReservationTime=waitingUser.ReservationTime,
				AttendanceTime=waitingUser.AttendanceTime,
				area=waitingUser.area,
				Smoking=waitingUser.Smoking,

			};
		
			await _acceptRepo.AddAsync(user);
			await _waitngRepo.DeleteAsync(waitingUser); 
			return "done";
			}
		return "client is not registed or client reservation not found";

	}
	public async Task<string> UnAcceptUser(int id)
	{
		var accpeted = await _acceptRepo.FirstOrDefaultAsync(x => x.Id == id);
		var client = await _clientRepo.FirstOrDefaultAsync(c => c.Id == accpeted.ClientId);
		if (!(accpeted == null && client == null)) { 
			var ToWaiting = new WaitingList()
		{
			ClientName= client.Name,
			PhoneNumber=client.PhoneNumber,
			Email=client.Email,
			Visitors=accpeted.Visitors,
			ReservationTime = accpeted.ReservationTime,
			AttendanceTime= accpeted.AttendanceTime,
			area=accpeted.area,
			Smoking = accpeted.Smoking,

		};
		await _waitngRepo.AddAsync(ToWaiting); 
			return "done";
			}
		return"not found client or accepted user";

	}
	 public async Task RemoveWaiting(int id)
	{
		var user=await _waitngRepo.FirstOrDefaultAsync(w=>w.Id==id);
		await _waitngRepo.DeleteAsync(user);
	}
	public async Task RemoveAccepted(int id)
	{
		var user = await _acceptRepo.FirstOrDefaultAsync(w => w.Id == id);
		await _acceptRepo.DeleteAsync(user);

	}
	public async Task<string> EditAccepted(int id ,EditUserDTO userDTO)
	{
		
		var user = await _acceptRepo.FirstOrDefaultAsync(u=>u.Id==id);
		if (!(user == null)) { 
		user.AttendanceTime = userDTO.AttendanceTime;
		user.area=userDTO.area;
			user.Smoking = userDTO.Smoking;
		user.TableNumber=userDTO.TableNumber;
		user.Visitors=userDTO.Visitors;
		await _acceptRepo.UpdateAsync(user);
			return"done";}
		return "user not found";



		


	}



}
