using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Query;
using very_easy_test_app.Models.DTO;
using very_easy_test_app.Models.Entities;

namespace very_easy_test_app.Services
{
    public interface IService<T, V>
        where T : EntityBase
        where V : DTOBase
    {
        Task<V> GetById(Guid id);
        Task<IEnumerable<V>> GetAll();

        Task<IEnumerable<V>> GetAll(
            Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool disableTracking = true,
            bool ignoreQueryFilters = false);

        Task<IList<V>> GetPagedList(Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            int pageIndex = 0,
            int pageSize = 20,
            bool disableTracking = true,
            CancellationToken cancellationToken = default,
            bool ignoreQueryFilters = false);

        Task<V> GetSingle(Expression<Func<T, bool>> predicate, bool ignoreQueryFilters = false);
        Task<K> GetSingle<K>(Expression<Func<K, bool>> predicate, bool ignoreQueryFilters = false) where K : EntityBase;

        Task<IList<SR>> GetAnotherTableRecords<S, SR>(Expression<Func<S, bool>> predicate = null,
            Func<IQueryable<S>, IOrderedQueryable<S>> orderBy = null,
            Func<IQueryable<S>, IIncludableQueryable<S, object>> include = null, bool disableTracking = true,
            bool ignoreQueryFilters = false) where S : EntityBase where SR : DTOBase;

        Task<bool> isExists(Expression<Func<T, bool>> predicate, bool ignoreQueryFilters = false);
        Task<int> AddRecord(V request, Guid? companyId = null);
        Task<V> AddRecordWithReturnRequest(V request);
        Task<int> UpdateRecord(V request);
        Task<int> PartialUpdateRecord(V request);
        Task<int> DeleteRecord(Guid id);
    }
}