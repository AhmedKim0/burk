using Burk.DAL.Entity;
using Burk.DAL.Repository.Interface;

namespace Burk.BL.Imp;

public class DashBoardServices
{
	private readonly IAsyncRepository<Review> _reviewRepo;
	private readonly IAsyncRepository<Burk.DAL.Entity.Client> _clientRepo;

	public DashBoardServices(IAsyncRepository<Review> reviewRepo, IAsyncRepository<Client> clientRepo)
	{
		_reviewRepo = reviewRepo ?? throw new ArgumentNullException(nameof(reviewRepo));
		_clientRepo = clientRepo ?? throw new ArgumentNullException(nameof(clientRepo));
	}
	public async Task<List<Review>> GetAllUserReviewByPhone(string phone)
	{
		var client = await _clientRepo.FirstOrDefaultAsync(c=>c.PhoneNumber==phone);
		if(client != null)
		{
			List<Review> reviews = await _reviewRepo.ListAsync(r => r.ClientId == client.Id);
			reviews.OrderByDescending(x=>x.CreatedAtUtc).ThenBy(x=>x.QuestionNumber);
			
			return reviews; }
		return null;
	}
	public async Task<int> NumberOfVistors()
	{ return 0;}




	}

