using System;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using very_easy_test_app.Extensions;
using very_easy_test_app.Models.DTO;
using very_easy_test_app.Models.Entities;
using very_easy_test_app.Services;

namespace very_easy_test_app.Controllers
{
    [ApiController]
    public abstract class BaseCRUDApiController<T, V> : ControllerBase
        where T : EntityBase
        where V : DTOBase
    {
        private readonly IMapper _map;
        protected readonly IService<T, V> _service;
        protected readonly bool _isReadOnly;
        protected readonly IStringLocalizer<BaseCRUDApiController<T, V>> _localizer;

        protected BaseCRUDApiController(
            IMapper map,
            IService<T, V> service,
            IStringLocalizer<BaseCRUDApiController<T, V>> localizer,
            bool isReadOnly = false)
        {
            _map = map ?? throw new NullReferenceException(nameof(map));
            _service = service ?? throw new NullReferenceException(nameof(service));
            _isReadOnly = isReadOnly;
            _localizer = localizer;
        }

        [HttpGet]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        public virtual async Task<IActionResult> Get()
            => Ok(await _service.GetAll());

        [HttpGet("{pageSize:int}/{pageIndex:int}")]
        public virtual async Task<IActionResult> Get([FromRoute] int pageSize, [FromRoute] int pageIndex)
            => Ok(await _service.GetPagedList(pageSize: pageSize, pageIndex: pageIndex));

        [HttpGet("{id:guid}")]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        public virtual async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var result = await _service.GetSingle(q => q.id == id);
            if (result != null)
                return Ok(result);
            return NotFound();
        }

        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        [ModelValidation]
        public virtual async Task<IActionResult> Post([FromBody] V request)
        {
            if (_isReadOnly) return BadRequest(new {message = "اطلاعات قابل ویرایش نیستند"});
            var result = await _service.AddRecord(request);
            if (result > 0)
                return Ok(request);
            return BadRequest(new {model = ModelState, message = "خطا در ویرایش اطلاعات"});
        }

        [HttpPut]
        [Produces("application/json")]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [ModelValidation]
        public virtual async Task<IActionResult> Put([FromBody] V request)
        {
            if (_isReadOnly) return BadRequest(new {message = "اطلاعات قابل ویرایش نیستند"});
            var existsRecord = await _service.GetSingle(q => q.id == request.id);
            if (existsRecord == null) return NotFound();
            var result = await _service.UpdateRecord(request);
            if (result > 0) return Ok(request);
            return BadRequest(new {model = ModelState, message = "خطا در ویرایش اطلاعات"});
        }

        [HttpPatch("{id:guid}")]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [ModelValidation]
        public virtual async Task<IActionResult> Patch([FromRoute] Guid id, [FromBody] JsonPatchDocument<V> patchDoc)
        {
            if (_isReadOnly) return BadRequest(new {message = "اطلاعات قابل ویرایش نیستند"});
            if (patchDoc == null) return BadRequest(new {model = ModelState, message = "خطا در ویرایش اطلاعات"});
            var founded = await _service.GetSingle(q => q.id == id);
            if (founded == null) return NotFound();
            var foundedToPatch = _map.Map<V>(founded);
            patchDoc.ApplyTo(foundedToPatch, ModelState);
            TryValidateModel(foundedToPatch);
            var result = await _service.PartialUpdateRecord(_map.Map(foundedToPatch, founded));
            if (result > 0)
                return Ok(_map.Map(foundedToPatch, founded));
            return BadRequest(new {model = ModelState, message = "خطا در ویرایش اطلاعات"});
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        public virtual async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            if (_isReadOnly) return BadRequest(new {model = ModelState, message = "خطا در ویرایش اطلاعات"});
            var exists = await _service.isExists(q => q.id == id);
            if (!exists) return NotFound();
            var result = await _service.DeleteRecord(id);
            if (result > 0)
                return Ok(id);
            return BadRequest(new {model = ModelState, message = "خطا در حذف اطلاعات"});
        }
    }
}