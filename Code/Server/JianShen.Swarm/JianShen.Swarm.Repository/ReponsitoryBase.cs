using Dapper;
using JianShen.Swarm.Common;
using JianShen.Swarm.Common.DataBase;
using JianShen.Swarm.IRepository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace JianShen.Swarm.Repository
{
    public class ReponsitoryBase<T> : IRepository<T> where T : class
    {
        private IDbConnection m_Connection;

        public ReponsitoryBase(IDapperContext dapperContext)
        {
            m_Connection = new SqlConnection(dapperContext.GetConnectionString());
        }

        public IDbConnection GetReadingConnection()
        {
            return m_Connection;
        }

        public T Get(object id, IDbTransaction transaction, int? commandTimeout)
        {
            return m_Connection.Get<T>(id, transaction, commandTimeout);
        }

        public async Task<T> GetAsync(object id, IDbTransaction transaction, int? commandTimeout)
        {
            return await m_Connection.GetAsync<T>(id, transaction, commandTimeout);
        }

        public List<T> GetList()
        {
            return m_Connection.GetList<T>().AsList();
        }

        public async Task<List<T>> GetListAsync()
        {
            return (await m_Connection.GetListAsync<T>()).AsList();
        }

        public PagedList<T> GetPagedList(int pageNumber, int rowsPerPage, string conditions, string orderby, out int total, object parameters, IDbTransaction transaction, int? commandTimeout)
        {
            var resultList = m_Connection.GetListPaged<T>(pageNumber, rowsPerPage, conditions, orderby, out total, parameters, transaction, commandTimeout);

            PagedList<T> pagedList = new PagedList<T>();

            pagedList.Items = resultList as List<T>;

            pagedList.TotalCount = total;

            return pagedList;
        }

        public async Task<PagedList<T>> GetPagedListAsync(int pageNumber, int rowsPerPage, string conditions, string orderby, object parameters, IDbTransaction transaction, int? commandTimeout)
        {
            var resultList = await m_Connection.GetListPagedAsync<T>(pageNumber, rowsPerPage, conditions, orderby, parameters, transaction, commandTimeout);

            PagedList<T> pagedList = new PagedList<T>();

            pagedList.Items = resultList.Item1 as List<T>;

            pagedList.TotalCount = resultList.Item2;

            return pagedList;
        }
    }
}
