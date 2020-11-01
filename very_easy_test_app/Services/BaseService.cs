using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Arch.EntityFrameworkCore.UnitOfWork;
using AutoMapper;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using very_easy_test_app.Models.DTO;
using very_easy_test_app.Models.Entities;
using very_easy_test_app.Models.Enum;

namespace very_easy_test_app.Services
{
    public class BaseService<T, V> : IService<T, V>
        where T : EntityBase
        where V : DTOBase
    {
        protected readonly Guid _companyId;
        private readonly IHttpContextAccessor _context;

        public BaseService(
            IMapper map,
            IUnitOfWork unitofwork,
            IHttpContextAccessor context,
            IDataProtectionProvider provider)
        {
            _map = map ?? throw new NullReferenceException(nameof(map));
            _unitofwork = unitofwork;
            _repository = _unitofwork.GetRepository<T>();
            _context = context;
        }

        protected IUnitOfWork _unitofwork { get; }
        protected IRepository<T> _repository { get; }
        protected IMapper _map { get; }

        public virtual async Task<IEnumerable<V>> GetAll()
        {
            return _map.Map<IEnumerable<V>>(await _repository.GetAllAsync());
        }

        public virtual async Task<IEnumerable<V>> GetAll(
            Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool disableTracking = true,
            bool ignoreQueryFilters = false)
        {
            return _map.Map<IEnumerable<V>>(await _repository.GetAllAsync(predicate, orderBy, include, disableTracking,
                ignoreQueryFilters));
        }

        public virtual async Task<IList<V>> GetPagedList(Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            int pageIndex = 0,
            int pageSize = 20,
            bool disableTracking = true,
            CancellationToken cancellationToken = default,
            bool ignoreQueryFilters = false)
        {
            return _map.Map<IList<V>>((await _repository.GetPagedListAsync(predicate, orderBy,
                include, pageIndex, pageSize, disableTracking, cancellationToken, ignoreQueryFilters)).Items);
        }

        public virtual async Task<V> GetById(Guid id)
        {
            return _map.Map<V>(await _repository.FindAsync(id));
        }

        public virtual async Task<IList<SR>> GetAnotherTableRecords<S, SR>(
            Expression<Func<S, bool>> predicate = null,
            Func<IQueryable<S>, IOrderedQueryable<S>> orderBy = null,
            Func<IQueryable<S>, IIncludableQueryable<S, object>> include = null, bool disableTracking = true,
            bool ignoreQueryFilters = false) where S : EntityBase where SR : DTOBase
        {
            return _map.Map<IList<SR>>(await _unitofwork.GetRepository<S>()
                .GetAllAsync(predicate, orderBy, include, disableTracking, ignoreQueryFilters));
        }

        public virtual async Task<bool> isExists(Expression<Func<T, bool>> predicate, bool ignoreQueryFilters = false)
        {
            return await _repository.ExistsAsync(predicate, ignoreQueryFilters);
        }

        public virtual async Task<int> AddRecord(V request, Guid? companyId = null)
        {
            request.id = Guid.NewGuid();
            request.tracking = new Tracking()
            {
                CRUDType = CRUDType.INSERT,
                CRUDActionDate = DateTime.Now,
                IPAddress = _context.HttpContext.Connection.RemoteIpAddress.ToString()
            };
            var record = _map.Map<T>(request);            
            record.allowDelete = true;
            await _repository.InsertAsync(record);
            return await _unitofwork.SaveChangesAsync();
        }

        public virtual async Task<V> AddRecordWithReturnRequest(V request)
        {
            request.id = Guid.NewGuid();
            request.tracking = new Tracking()
            {
                CRUDType = CRUDType.INSERT,
                CRUDActionDate = DateTime.Now,
                IPAddress = _context.HttpContext.Connection.RemoteIpAddress.ToString()
            };
            var record = _map.Map<T>(request);
            record.allowDelete = true;
            await _repository.InsertAsync(record);
            var g= await _unitofwork.SaveChangesAsync();
            return request;
        }

        public virtual async Task<int> UpdateRecord(V request)
        {
            var record = _map.Map<T>(request);
            request.tracking = new Tracking()
            {
                CRUDType = CRUDType.UPDATE,
                CRUDActionDate = DateTime.Now,
                IPAddress = _context.HttpContext.Connection.RemoteIpAddress.ToString()
            };
            _repository.Update(record);
            return await _unitofwork.SaveChangesAsync();
        }

        public virtual async Task<int> DeleteRecord(Guid id)
        {
            var founded = _map.Map<T>(await GetSingle(q => q.id == id));
            if (founded == null) return 0;
            founded.allowDelete = true;
            return await _unitofwork.SaveChangesAsync();
        }

        public virtual async Task<int> PartialUpdateRecord(V request)
        {
            request.tracking = new Tracking()
            {
                CRUDType = CRUDType.UPDATE,
                CRUDActionDate = DateTime.Now,
                IPAddress = _context.HttpContext.Connection.RemoteIpAddress.ToString()
            };
            var record = _map.Map<T>(request);
            _repository.ChangeEntityState(record, EntityState.Modified);
            return await _unitofwork.SaveChangesAsync();
        }

        public virtual async Task<V> GetSingle(Expression<Func<T, bool>> predicate, bool ignoreQueryFilters = false)
        {
            return _map.Map<V>(await _repository.GetFirstOrDefaultAsync(predicate: predicate,
                ignoreQueryFilters: ignoreQueryFilters));
        }

        public virtual async Task<K> GetSingle<K>(Expression<Func<K, bool>> predicate, bool ignoreQueryFilters = false)
            where K : EntityBase
        {
            return _map.Map<K>(await _unitofwork.GetRepository<K>()
                .GetFirstOrDefaultAsync(predicate: predicate, ignoreQueryFilters: ignoreQueryFilters));
        }
    }
}