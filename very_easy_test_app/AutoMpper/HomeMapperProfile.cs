using AutoMapper;
using Newtonsoft.Json;
using very_easy_test_app.Models.DTO;
using very_easy_test_app.Models.Entities;

namespace very_easy_test_app.AutoMpper
{
    public sealed class HomeMapperProfile : Profile
    {
        public HomeMapperProfile()
        {
            AllowNullCollections = true;
            AllowNullDestinationValues = true;

            CreateMap<string, string>().ConvertUsing(s => string.IsNullOrWhiteSpace(s) ? null : s);
            CreateMap<HomeEntity, DTOHome>().ReverseMap()
                .ForMember(c => c.tracking,
                    opt => opt.MapFrom(s => JsonConvert.SerializeObject(s.tracking)));
            CreateMap<HomeOwenerEntity, DTOHomeOwener>().ReverseMap().ForMember(c => c.tracking,
                opt => opt.MapFrom(s => JsonConvert.SerializeObject(s.tracking)));
        }
    }
}