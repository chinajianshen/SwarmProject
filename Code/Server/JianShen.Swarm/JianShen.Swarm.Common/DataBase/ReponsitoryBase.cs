using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace JianShen.Swarm.Common.DataBase
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

        T IRepository<T>.Get(object id, IDbTransaction transaction, int? commandTimeout)
        {
            return m_Connection.Get<T>(id, transaction, commandTimeout);
        }

        List<T> IRepository<T>.GetList()
        {
            return m_Connection.GetList<T>().AsList();
        }

        PagedList<T> IRepository<T>.GetPagedList(int pageNumber, int rowsPerPage, string conditions, string orderby, out int total, object parameters, IDbTransaction transaction, int? commandTimeout)
        {
            var resultList = m_Connection.GetListPaged<T>(pageNumber, rowsPerPage, conditions, orderby, out total, parameters, transaction, commandTimeout);

            PagedList<T> pagedList = new PagedList<T>();

            pagedList.Items = resultList as List<T>;

            pagedList.TotalCount = total;

            return pagedList;
        }
    }
}
