using AutoMapper;
using WebApplication1.Models;
using WebApplication1.DTOs;

namespace WebApplication1
{
    public class MappingProfile : Profile
    {
        /*
         * Я не уверен что это вообще необходимо, т.к. конфиг маппинга
         * мы все равно создаем перед тем как замапить в каждом
         * отдельном методе
         */
        public MappingProfile() {
            CreateMap<Customer, CustomerDTO>().ReverseMap();
            CreateMap<Movie, MovieDTO>().ReverseMap();
            CreateMap<MemberShip, MemberShipTypeDTO>().ReverseMap();
            CreateMap<Genre, GenreDTO>().ReverseMap();
        }

    }
}
