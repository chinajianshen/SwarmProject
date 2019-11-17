using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace JianShen.Swarm.Common.DataBase
{
    public interface IRepository<T> where T : class
    {
        T Get(object id, IDbTransaction transaction = null, int? commandTimeout = null);

        List<T> GetList();

        PagedList<T> GetPagedList(int pageNumber, int rowsPerPage, string conditions, string orderby, out int total, object parameters = null, IDbTransaction transaction = null, int? commandTimeout = null);
    }
}
