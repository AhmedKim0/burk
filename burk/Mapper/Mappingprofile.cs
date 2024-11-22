using AutoMapper;
using burk.DTO;
using Burk.DAL.Entity;

namespace burk.Mapper
{
    public class Mappingprofile:Profile
    {
        public Mappingprofile()
        {
            CreateMap<Client,ClientDto>().ReverseMap();
        }
    }
}
