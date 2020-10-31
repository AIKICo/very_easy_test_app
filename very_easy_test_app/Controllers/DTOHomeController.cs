using AutoMapper;
using AutoMapper.Internal;
using Microsoft.Extensions.Localization;
using very_easy_test_app.Models.DTO;
using very_easy_test_app.Models.Entities;
using very_easy_test_app.Services;

namespace very_easy_test_app.Controllers
{
    public sealed class DTOHomeController:BaseCRUDApiController<HomeOwenerEntity, DTOHomeOwener>
    {
        public DTOHomeController(IMapper map,
            IService<HomeOwenerEntity, DTOHomeOwener> service,
            bool isReadOnly = false):base(map, service) {}
    }
}