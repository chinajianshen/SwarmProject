using JianShen.Swarm.Common;
using JianShen.Swarm.Common.Message;
using JianShen.Swarm.DTO;
using JianShen.Swarm.Model.BaseModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JianShen.Swarm.IService
{
    public interface IDepartmentService: IService<DepartmentModel>
    {
        List<DepartmentEntity> GetListDto();

        Task<List<DepartmentEntity>> GetListDtoAsync();

        DepartmentEntity GetByIDDto(int id);

        Task<DepartmentEntity> GetByIDDtoAsync(int id);

        PagedList<DepartmentEntity> GetPagedListDto(BaseQueryEntity queryEntity);

        Task<PagedList<DepartmentEntity>> GetPagedListDtoAsync(BaseQueryEntity queryEntity);
    }
}
