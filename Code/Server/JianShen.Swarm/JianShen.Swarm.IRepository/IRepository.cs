using JianShen.Swarm.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace JianShen.Swarm.IRepository
{
    public interface IRepository<T> where T : class
    {
        T Get(object id, IDbTransaction transaction = null, int? commandTimeout = null);

        Task<T> GetAsync(object id, IDbTransaction transaction = null, int? commandTimeout = null);

        List<T> GetList();

        Task<List<T>> GetListAsync();

        PagedList<T> GetPagedList(int pageNumber, int rowsPerPage, string conditions, string orderby, out int total, object parameters = null, IDbTransaction transaction = null, int? commandTimeout = null);

        Task<PagedList<T>> GetPagedListAsync(int pageNumber, int rowsPerPage, string conditions, string orderby, object parameters = null, IDbTransaction transaction = null, int? commandTimeout = null);
    }
}
