using Burk.DAL.Entity;
using Burk.DAL.Repository.Interface;
using Burk.DTO;

using Microsoft.EntityFrameworkCore.Metadata.Internal;

using System.Web.Http.ModelBinding;

namespace Burk.BL.Imp;

public class ReviewService
{
	private readonly IAsyncRepository<Review> _reviewRepo;
	private readonly IAsyncRepository<Burk.DAL.Entity.Client> _clientRepo;


	public ReviewService(IAsyncRepository<Review> reviewRepo)
	{
		_reviewRepo = reviewRepo ?? throw new ArgumentNullException(nameof(reviewRepo));
	}
	public async Task<int> GetClientByPhone(string phone)
	{
		var client = await _clientRepo.FirstOrDefaultAsync(c => c.PhoneNumber == phone);
		if(client!=null)
		return client.Id;
		else return 0;


	} 
	//public Task<string> AddReview(int id , ReviewDTO reviewDTO)
	//{
	//	if(reviewDTO.CheckNo!=null)
	//	{ 
	//		Review review = new Review()
	//		{
	//			CheckNo= reviewDTO.CheckNo,


	//		};
	//	}

}

