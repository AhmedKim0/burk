using Burk.DAL.Entity;
using Burk.DAL.ResponseModel;
using Burk.DTO;

using System.Threading.Tasks;

namespace Burk.BL.Interface;

public interface IReviewService
{
Task<Response<Burk.DAL.Entity.Client>> GetClientByPhone(string phone);


Task<Response<bool>> AddReview(SubmitReviewDTO dto);




Task<Response<List<GetAllReviewsDTO>>> GetAllReview();
}
