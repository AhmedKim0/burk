using AutoMapper;

using Burk.BL.Interface;
using Burk.DAL.Entity;
using Burk.DAL.Entity.Enums;
using Burk.DAL.Repository.Interface;
using Burk.DTO;

using Microsoft.EntityFrameworkCore.Metadata.Internal;

using System.Collections.Generic;
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
			var addClient = new Client();

			addClient.PhoneNumber = dto.PhoneNumber;
			addClient.Name = dto.ClientName;
			addClient.Email = dto.Email;


			client = await _clientRepo.AddAsync(addClient);
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
	public async Task<List<GetAllReviewsDTO>> GetAllReview()
	{
		var all= new List < GetAllReviewsDTO >();
		List <Client> clients = await _clientRepo.ListAllAsync(false);

		foreach (Client client in clients)
		{
					var dto = new GetAllReviewsDTO
						{
							ClientName = client.Name,
							PhoneNumber = client.PhoneNumber,
							Email = client.Email,
							checkDTos = new List<CheckDTo>()  // Initialize the checkDTos list here
						};
						List<Review> reviews = await _reviewRepo.ListAsync(r => r.ClientId == client.Id, false);
					//var checks = reviews.Select(x => (x.CheckNo, x.CreatedAtUtc)).Distinct();
					var checks = reviews
					.GroupBy(x => x.CheckNo)             // Group by CheckNo
					.Select(g => g.First())              // Select the first item from each group
					.Select(x => (x.CheckNo, x.CreatedAtUtc)) // Project to the desired structure
					.ToList();


					foreach (var check in checks)
					{
							var Checkdto = new CheckDTo
							{
								CheckNo = check.CheckNo,
								CreatedAt = check.CreatedAtUtc,
								Answers = new List<Answer>()  // Initialize the Answers list here
							};






							var answers = reviews.Where(x => x.CheckNo == check.CheckNo);
						//List<Burk.DTO.Answer> getanswer = new List<Burk.DTO.Answer>();
						foreach (var ans in answers)
						{
							Burk.DTO.Answer answer = new Burk.DTO.Answer();
							answer.QuestionNumber = ans.QuestionNumber;
							answer.AnswerType = ans.AnswerType;
							answer.comment = ans.comment;
							answer.rate = ans.rate;
							answer.yesOrNO = ans.yesOrNO;
							//getanswer.Add(answer);
							Checkdto.Answers.Add(answer);

						}
						dto.checkDTos.Add(Checkdto);



					}
			all.Add(dto);

		}
				return all; 
	}


}



