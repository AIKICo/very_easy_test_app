using AutoMapper;
using AutoMapper.Internal;
using Microsoft.Extensions.Localization;
using very_easy_test_app.Models.DTO;
using very_easy_test_app.Models.Entities;
using very_easy_test_app.Services;

namespace very_easy_test_app.Controllers
{
    public sealed class DTOHomeController:BaseCRUDApiController<HomeEntity, DTOHome>
    {
        public DTOHomeController(IMapper map,
            IService<HomeEntity, DTOHome> service,
            bool isReadOnly = false):base(map, service) {}
    }
}