using AutoMapper;


using Burk.DAL.Entity;
using Burk.DTO;

namespace Burk.Mapper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
			CreateMap<ReviewDTO, Review>().ReverseMap();

		}
} }
