using JianShen.Swarm.Common.DataBase;
using JianShen.Swarm.IRepository;
using JianShen.Swarm.Model.BaseModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace JianShen.Swarm.Repository
{
    public class DepartmentRep: ReponsitoryBase<DepartmentModel>, IDepartmentRep
    {
        public DepartmentRep(IDapperContext context) : base(context)
        {

        }
    }
}
