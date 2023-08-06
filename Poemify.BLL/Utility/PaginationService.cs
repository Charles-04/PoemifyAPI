using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Poemify.Models.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poemify.BLL.Utility
{
   public class PaginationService : IPaginationService
    {

        private readonly IMapper _mapper;

        public PaginationService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<PaginationResponse<T>> PaginateRecordsWithSynchronousModelProcessorAsync<T, W>(int page, int pageSize, IQueryable<W> records, Func<IEnumerable<W>, IEnumerable<T>> modelProcessorDelegate)
           
        {
            int recordsToSkip = (page - 1) * pageSize;
            int totalRecords = await records.CountAsync();
            double totalPages = totalRecords / (double)pageSize;

            int pageCount = int.Parse(Math.Ceiling(totalPages).ToString());

            IEnumerable<W> paginatedRecords = await records.Skip(recordsToSkip).Take(pageSize).ToListAsync();

            IEnumerable<T> paginatedRecordsDTO = modelProcessorDelegate(paginatedRecords);

            return new PaginationResponse<T>(pageSize, page, pageCount, totalRecords, paginatedRecordsDTO);
        }


        public async Task<PaginationResponse<T>> PaginateRecordsAsync<T, W>(int page, int pageSize, IEnumerable<W> records, Func<IEnumerable<W>, Task<IEnumerable<T>>> modelProcessorDelegate)
            
        {
            int recordsToSkip = (page - 1) * pageSize;
            int totalRecords = records.Count();
            double totalPages = totalRecords / (double)pageSize;

            int pageCount = int.Parse(Math.Ceiling(totalPages).ToString());

            IEnumerable<W> paginatedRecords = records.Skip(recordsToSkip).Take(pageSize);

            IEnumerable<T> paginatedRecordsDTO = await modelProcessorDelegate(paginatedRecords.ToList());

            return new PaginationResponse<T>(pageSize, page, pageCount, totalRecords, paginatedRecordsDTO);
        }

        public async Task<PaginationResponse<T>> PaginateRecordsAsync<T, W>(int page, int pageSize, IQueryable<W> records, Func<IEnumerable<W>, Task<IEnumerable<T>>> modelProcessorDelegate)
        
        {
            int recordsToSkip = (page - 1) * pageSize;
            int totalRecords = records.Count();
            double totalPages = totalRecords / (double)pageSize;

            int pageCount = int.Parse(Math.Ceiling(totalPages).ToString());

            IEnumerable<W> paginatedRecords = await records.Skip(recordsToSkip).Take(pageSize).ToListAsync();

            IEnumerable<T> paginatedRecordsDTO = await modelProcessorDelegate(paginatedRecords);

            return new PaginationResponse<T>(pageSize, page, pageCount, totalRecords, paginatedRecordsDTO);
        }

        public PaginationResponse<T> PaginateRecords<T, W>(int page, int pageSize, IEnumerable<W> records, Func<IEnumerable<W>, IEnumerable<T>> modelProcessorDelegate)
            
        {
            int recordsToSkip = (page - 1) * pageSize;
            int totalRecords = records.Count();
            double totalPages = totalRecords / (double)pageSize;

            int pageCount = int.Parse(Math.Ceiling(totalPages).ToString());

            IEnumerable<W> paginatedRecords = records.Skip(recordsToSkip).Take(pageSize);

            IEnumerable<T> paginatedRecordsDTO = modelProcessorDelegate(paginatedRecords.ToList());

            return new PaginationResponse<T>(pageSize, page, pageCount, totalRecords, paginatedRecordsDTO);
        }

        public PaginationResponse<T> PaginateRecords<T, W>(int page, int pageSize, IEnumerable<W> records)
           
        {
            int recordsToSkip = (page - 1) * pageSize;
            int totalRecords = records.Count();
            double totalPages = totalRecords / (double)pageSize;

            int pageCount = int.Parse(Math.Ceiling(totalPages).ToString());

            IEnumerable<W> paginatedRecords = records.Skip(recordsToSkip).Take(pageSize);

            IEnumerable<T> paginatedRecordsDTO = _mapper.Map<IEnumerable<T>>(paginatedRecords);

            return new PaginationResponse<T>(pageSize, page, pageCount, totalRecords, paginatedRecordsDTO);
        }

        public async Task<PaginationResponse<T>> PaginateRecords<T, W>(int page, int pageSize, IQueryable<W> records)
         
         
        {
            int recordsToSkip = (page - 1) * pageSize;
            int totalRecords = await records.CountAsync();
            double totalPages = totalRecords / (double)pageSize;

            int pageCount = int.Parse(Math.Ceiling(totalPages).ToString());

            IEnumerable<W> paginatedRecords = await records.Skip(recordsToSkip).Take(pageSize).ToListAsync();

            IEnumerable<T> paginatedRecordsDTO = _mapper.Map<IEnumerable<T>>(paginatedRecords);

            return new PaginationResponse<T>(pageSize, page, pageCount, totalRecords, paginatedRecordsDTO);
        }

        public async Task<PaginationResponse<T>> PaginateRecordsWithoutMap<T>(int page, int pageSize, IQueryable<T> records)
       
        {
            int recordsToSkip = (page - 1) * pageSize;
            int totalRecords = await records.CountAsync();
            double totalPages = totalRecords / (double)pageSize;

            int pageCount = int.Parse(Math.Ceiling(totalPages).ToString());

            IEnumerable<T> paginatedRecords = records.Skip(recordsToSkip).Take(pageSize);


            return new PaginationResponse<T>(pageSize, page, pageCount, totalRecords, paginatedRecords);
        }
    }
}
