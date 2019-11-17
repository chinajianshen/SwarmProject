using JianShen.Swarm.Common;
using JianShen.Swarm.Common.Message;
using JianShen.Swarm.DTO;
using JianShen.Swarm.Model.BaseModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace JianShen.Swarm.IService
{
    public interface IDepartmentService
    {
        ReponseMessage<List<DepartmentEntity>> GetList();

        ReponseMessage<DepartmentEntity> GetByID(int id);

        ReponseMessage<PagedList<DepartmentEntity>> GetPagedList(int pageNumber, int rowsPerPage);
    }
}
