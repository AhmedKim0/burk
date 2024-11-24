using AutoMapper;

using Burk.BL.Interface;
using Burk.DAL.Entity;
using Burk.DAL.Repository.Interface;
using Burk.DTO;

using Microsoft.EntityFrameworkCore.Metadata.Internal;

using System.Web.Http.ModelBinding;

namespace Burk.BL.Imp;

public class ReviewService: IReviewService
{
	private readonly IAsyncRepository<Review> _reviewRepo;
	private readonly IAsyncRepository<Burk.DAL.Entity.Client> _clientRepo;
	private readonly IMapper _mapper;


	public ReviewService(IAsyncRepository<Review> reviewRepo,IMapper mapper)
	{
		_reviewRepo = reviewRepo ?? throw new ArgumentNullException(nameof(reviewRepo));
		_mapper = mapper ?? throw new ArgumentNullException(nameof(_mapper));
	}
	public async Task<Burk.DAL.Entity.Client> GetClientByPhone(string phone)
	{
		var client = await _clientRepo.FirstOrDefaultAsync(c => c.PhoneNumber == phone);
		if (client != null)
			return client;
		else return new Burk.DAL.Entity.Client();


	}
	public async Task<string> AddReview(int id, List<ReviewDTO> reviewDTO)
	{
		foreach (var item in reviewDTO)
		{
			if (item.CheckNo == null)
			{
				return "CheckNo is empty";
			}
			Review review = new Review()
			{
				CheckNo = item.CheckNo,
				comment = item.comment,
				yesOrNO = item.yesOrNO,
				rate = item.rate,
				AnswerType = item.AnswerType,
				QuestionNumber = item.QuestionNumber,

			};
			await _reviewRepo.AddAsync(review, false);



		}
		await _reviewRepo.SaveChangesAsync();
		return "done";

	}
	public async Task<List<ReviewDTO>> GetAllReview()
	{
		List<Review> reviews = await _reviewRepo.ListAllAsync();
		reviews.OrderByDescending(x=>x.CreatedBy).ThenBy(c=>c.ClientId).ThenBy(n=>n.QuestionNumber);
		List<ReviewDTO> DTO=_mapper.Map<List<ReviewDTO>>(reviews);
		return DTO;
							

	}



}


