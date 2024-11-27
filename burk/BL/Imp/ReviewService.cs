using AutoMapper;

using Burk.BL.Interface;
using Burk.DAL.Entity;
using Burk.DAL.Entity.Enums;
using Burk.DAL.Repository.Interface;
using Burk.DTO;

using Microsoft.EntityFrameworkCore.Metadata.Internal;

using System.Numerics;
using System.Web.Http.ModelBinding;

namespace Burk.BL.Imp;

public class ReviewService: IReviewService
{
	private readonly IAsyncRepository<Review> _reviewRepo;
	private readonly IAsyncRepository<Burk.DAL.Entity.Client> _clientRepo;
	private readonly IAsyncRepository<WaitingList> _waitingListRepo;
	private readonly IAsyncRepository<RecordedVisit> _recordedVisitRepo;

	private readonly IMapper _mapper;


	public ReviewService(
		IAsyncRepository<Review> reviewRepo,
		IMapper mapper,
		IAsyncRepository<WaitingList> waitingListRepo,
		IAsyncRepository<RecordedVisit> recordedVisit,
		IAsyncRepository<Burk.DAL.Entity.Client> clientRepo
		)
	{
		_reviewRepo = reviewRepo ?? throw new ArgumentNullException(nameof(reviewRepo));
		_mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
		_waitingListRepo = waitingListRepo ?? throw new ArgumentNullException(nameof(waitingListRepo));
		_recordedVisitRepo = recordedVisit ?? throw new ArgumentNullException(nameof(recordedVisit));
		_clientRepo= clientRepo?? throw new ArgumentNullException(nameof(clientRepo));
		
	}
	public async Task<Burk.DAL.Entity.Client> GetClientByPhone(string phone)
	{
		Client client = await _clientRepo.FirstOrDefaultAsync(c => c.PhoneNumber == phone,false);
		if (client != null)
			return client;
		else return new Burk.DAL.Entity.Client();

	}
	public async Task<string> AddReview(SubmitReviewDTO dto)
	{ Client client = await _clientRepo.FirstOrDefaultAsync(client => client.PhoneNumber == dto.PhoneNumber,false);
		if (client==null)
		{

			client.PhoneNumber = dto.PhoneNumber;
			client.Name = dto.ClientName;
			client.Email = dto.Email;


			client = await _clientRepo.AddAsync(client);
		}
		if (client.Name != dto.ClientName)
		{
				client.Name = dto.ClientName;
			await _clientRepo.UpdateAsync(client);
		}

	






		var visit = await _waitingListRepo.LastOrDefaultAsync(c => c.ClientId ==client.Id&&c.IsConfirmed== ClientState.Confirmed, false);
		foreach (var item in dto.Answers)
		{
			if (dto.CheckNo == null)
			{
				return "CheckNo is empty";
			}
			Review review = new Review()
			{
				CheckNo = dto.CheckNo,
				comment = item.comment,
				yesOrNO = item.yesOrNO,
				rate = item.rate,
				AnswerType = item.AnswerType,
				QuestionNumber = item.QuestionNumber,
				ClientId=client.Id,

			};
			await _reviewRepo.AddAsync(review, false);

		}
		if(visit != null)
			{
		var recordvisit= new RecordedVisit()
		{
			TableNumber=(int)visit.TableNumber,
			Visitors= visit.Visitors,
			ReservationTime= visit.ReservationTime,
			AttendanceTime= visit.AttendanceTime,
			area=visit.area,
			Smoking=visit.Smoking,
			ClientId= visit.ClientId,

		};
		
		await _recordedVisitRepo.AddAsync(recordvisit);
		await _waitingListRepo.DeleteAsync(visit);
		}
		await _reviewRepo.SaveChangesAsync();
		
		return "done";

	}
	public async Task<List<Review>> GetAllReview()
	{
		//List<AnswerDTO> list = new List<AnswerDTO>();
		List<Review> reviews = await _reviewRepo.ListAllAsync();
		//reviews.OrderByDescending(x=>x.ClientId).ThenBy(c=>c.CreatedBy)
		//.ThenBy(n=>n.QuestionNumber);
		//foreach (var review in reviews)
		//{

		//}
		
		return reviews;
							

	}



}


