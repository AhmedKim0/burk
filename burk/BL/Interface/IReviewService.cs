using Burk.DAL.Entity;
using Burk.DTO;

using System.Threading.Tasks;

namespace Burk.BL.Interface;

public interface IReviewService
{
Task<Burk.DAL.Entity.Client> GetClientByPhone(string phone);


Task<string> AddReview(BeforeReviewDTO dto, List<ReviewDTO> reviewDTO);




 Task<List<ReviewDTO>> GetAllReview();
}
