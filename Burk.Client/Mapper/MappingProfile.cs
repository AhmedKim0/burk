using AutoMapper;
using Burk.Client.DTO;
using Burk.DAL.Entity;

namespace Burk.Client.Mapper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<WatinigListDto,WaitingList>().ReverseMap();
        }
    }
}
