using Poemify.Models.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poemify.BLL.Utility
{
    public interface IPaginationService
    {

        Task<PaginationResponse<T>> PaginateRecordsWithSynchronousModelProcessorAsync<T, W>(int page, int pageSize, IQueryable<W> records, Func<IEnumerable<W>, IEnumerable<T>> modelProcessorDelegate);
        Task<PaginationResponse<T>> PaginateRecordsAsync<T, W>(int page, int pageSize, IEnumerable<W> records, Func<IEnumerable<W>, Task<IEnumerable<T>>> modelProcessorDelegate);


        Task<PaginationResponse<T>> PaginateRecordsAsync<T, W>(int page, int pageSize, IQueryable<W> records, Func<IEnumerable<W>, Task<IEnumerable<T>>> modelProcessorDelegate);
        PaginationResponse<T> PaginateRecords<T, W>(int page, int pageSize, IEnumerable<W> records, Func<IEnumerable<W>, IEnumerable<T>> modelProcessorDelegate);
        PaginationResponse<T> PaginateRecords<T, W>(int page, int pageSize, IEnumerable<W> records);
        Task<PaginationResponse<T>> PaginateRecords<T, W>(int page, int pageSize, IQueryable<W> records);

        Task<PaginationResponse<T>> PaginateRecordsWithoutMap<T>(int page, int pageSize, IQueryable<T> records);

    }
}
