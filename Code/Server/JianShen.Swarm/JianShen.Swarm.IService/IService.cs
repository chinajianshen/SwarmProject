using JianShen.Swarm.Common;
using JianShen.Swarm.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JianShen.Swarm.IService
{
   public interface IService<T> where T:class
    {
        List<T> GetList();

        Task<List<T>> GetListAsync();

        T GetByID(int id);

        Task<T> GetByIDAsync(int id);       

        PagedList<T> GetPagedList(BaseQueryEntity queryEntity);

        Task<PagedList<T>> GetPagedListAsync(BaseQueryEntity queryEntity);
    }
}
