using JianShen.Swarm.Common;
using JianShen.Swarm.Common.Utility;
using JianShen.Swarm.DTO;
using JianShen.Swarm.IRepository;
using JianShen.Swarm.IService;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JianShen.Swarm.Service
{
    public class ServiceBase<T>: IService<T> where T:class
    {
        private readonly IRepository<T> _repository;
        public ServiceBase(IRepository<T> repository)
        {
            _repository = repository;
        }

        public T GetByID(int id)
        {
            var queryResult = _repository.Get(id);          

            return queryResult;
        }

        public async Task<T> GetByIDAsync(int id)
        {
            var queryResult = await _repository.GetAsync(id);          

            return queryResult;
        }


        public List<T> GetList()
        {
            var queryResult = _repository.GetList();
            return queryResult;
        }

        public async Task<List<T>> GetListAsync()
        {
            var queryResult = await _repository.GetListAsync();
            return queryResult;
        }

        public PagedList<T> GetPagedList(BaseQueryEntity queryEntity)
        {
            int total = 0;
            var result = new PagedList<T>();
            var queryResult = _repository.GetPagedList(queryEntity.PageIndex, queryEntity.PageSize, queryEntity.WhereCondition, queryEntity.OrderBy, out total,queryEntity.Parameters);
            result.Items = queryResult.Items;
            result.TotalCount = total;

            return result;
        }

        public async Task<PagedList<T>> GetPagedListAsync(BaseQueryEntity queryEntity)
        {
            int total = 0;
            var result = new PagedList<T>();
            var queryResult = await _repository.GetPagedListAsync(queryEntity.PageIndex, queryEntity.PageSize, null, null);
            result.Items = queryResult.Items;
            result.TotalCount = total;

            return result;
        }       
    }
}
