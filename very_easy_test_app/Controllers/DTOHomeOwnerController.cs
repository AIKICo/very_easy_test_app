using AutoMapper;
using Microsoft.Extensions.Localization;
using very_easy_test_app.Models.DTO;
using very_easy_test_app.Models.Entities;
using very_easy_test_app.Services;

namespace very_easy_test_app.Controllers
{
    public class DTOHomeOwnerController : BaseCRUDApiController<HomeEntity, DTOHome>
    {
        public DTOHomeOwnerController(IMapper map,
            IService<HomeEntity, DTOHome> service,
            IStringLocalizer<BaseCRUDApiController<HomeEntity, DTOHome>> localizer,
            bool isReadOnly = false) : base(map, service, localizer)
        {
        }
    }
}