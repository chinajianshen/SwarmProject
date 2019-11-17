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

namespace JianShen.Swarm.Service
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRep m_DepartmentRep;

        public DepartmentService(IDepartmentRep departmentRep)
        {
            m_DepartmentRep = departmentRep;
        }

        ReponseMessage<DepartmentEntity> IDepartmentService.GetByID(int id)
        {
            ReponseMessage<DepartmentEntity> result = new ReponseMessage<DepartmentEntity>();           
            var queryResult = m_DepartmentRep.Get(id);
            result.Data = queryResult.JTransformTo<DepartmentEntity>();
            result.IsSuccess = true;

            return result;
        }

        ReponseMessage<List<DepartmentEntity>> IDepartmentService.GetList()
        {
            ReponseMessage<List<DepartmentEntity>> result = new ReponseMessage<List<DepartmentEntity>>()
            {
                Data = m_DepartmentRep.GetList().JTransformTo<DepartmentEntity>(),
                IsSuccess = true
            };
            return result;
        }

        ReponseMessage<PagedList<DepartmentEntity>> IDepartmentService.GetPagedList(int pageNumber, int rowsPerPage)
        {
            int total = 0;
            ReponseMessage<PagedList<DepartmentEntity>> result = new ReponseMessage<PagedList<DepartmentEntity>>() {
                Data = new PagedList<DepartmentEntity>()
            };

            var queryResult = m_DepartmentRep.GetPagedList(pageNumber, rowsPerPage, null, null, out total);
            result.Data.Items = queryResult.Items.JTransformTo<DepartmentEntity>();
            result.Data.TotalCount = total;
            result.IsSuccess = true;

            return result;
        }
    }
}
