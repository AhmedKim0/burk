using Burk.DAL.Entity;
using Burk.DTO;

namespace Burk.BL.Interface;

public interface IReviewService
{
Task<Burk.DAL.Entity.Client> GetClientByPhone(string phone);


 Task<string> AddReview(int id, List<ReviewDTO> reviewDTO);




 Task<List<ReviewDTO>> GetAllReview();
}
