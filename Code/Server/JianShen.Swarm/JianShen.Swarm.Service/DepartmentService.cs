using JianShen.Swarm.Common;
using JianShen.Swarm.Common.Message;
using JianShen.Swarm.Common.Utility;
using JianShen.Swarm.DTO;
using JianShen.Swarm.IRepository;
using JianShen.Swarm.IService;
using JianShen.Swarm.Model.BaseModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JianShen.Swarm.Service
{
    public class DepartmentService : ServiceBase<DepartmentModel>,IDepartmentService
    {
        private readonly IDepartmentRep m_DepartmentRep;

        public DepartmentService(IDepartmentRep departmentRep):base(departmentRep)
        {
            m_DepartmentRep = departmentRep;            
        }

        public DepartmentEntity GetByIDDto(int id)
        {
            var queryResult = m_DepartmentRep.Get(id);
            var result = queryResult.JTransformTo<DepartmentEntity>();

            return result;
        }

        public async Task<DepartmentEntity> GetByIDDtoAsync(int id)
        {
            var queryResult = await m_DepartmentRep.GetAsync(id);
            var result = queryResult.JTransformTo<DepartmentEntity>();

            return result;
        }


        public List<DepartmentEntity> GetListDto()
        {
            var queryResult = m_DepartmentRep.GetList().JTransformTo<DepartmentEntity>();
            return queryResult;
        }

        public async Task<List<DepartmentEntity>> GetListDtoAsync()
        {
            var queryResult = (await m_DepartmentRep.GetListAsync()).JTransformTo<DepartmentEntity>();
            return queryResult;
        }

        public PagedList<DepartmentEntity> GetPagedListDto(BaseQueryEntity queryEntity)
        {
            int total = 0;
            var result = new PagedList<DepartmentEntity>();
            var queryResult = m_DepartmentRep.GetPagedList(queryEntity.PageIndex, queryEntity.PageSize, queryEntity.WhereCondition, queryEntity.OrderBy, out total,queryEntity.Parameters);
            result.Items = queryResult.Items.JTransformTo<DepartmentEntity>();
            result.TotalCount = total;

            return result;
        }

        public async Task<PagedList<DepartmentEntity>> GetPagedListDtoAsync(BaseQueryEntity queryEntity)
        {            
            var result = new PagedList<DepartmentEntity>();
            var queryResult = await m_DepartmentRep.GetPagedListAsync(queryEntity.PageIndex, queryEntity.PageSize, queryEntity.WhereCondition, queryEntity.OrderBy,queryEntity.Parameters);
            result.Items = queryResult.Items.JTransformTo<DepartmentEntity>();
            result.TotalCount = queryResult.TotalCount;

            return result;
        }
    }
}
