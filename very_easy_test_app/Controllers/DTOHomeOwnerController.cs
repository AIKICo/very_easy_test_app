using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using very_easy_test_app.Models.DTO;
using very_easy_test_app.Models.Entities;
using very_easy_test_app.Services;

namespace very_easy_test_app.Controllers
{
    public class DTOHomeOwnerController : BaseCRUDApiController<HomeOwenerEntity, DTOHomeOwener>
    {
        public DTOHomeOwnerController(IMapper map,
            IService<HomeOwenerEntity, DTOHomeOwener> service,
            bool isReadOnly = false) : base(map, service)
        {
        }
    }
}