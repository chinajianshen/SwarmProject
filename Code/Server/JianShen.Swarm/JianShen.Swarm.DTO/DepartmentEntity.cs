using System;
using System.Collections.Generic;
using System.Text;

namespace JianShen.Swarm.DTO
{
    public class DepartmentEntity
    {       
        public int DepartmentID { get; set; }

        public string DepartmentName { get; set; }

        public DateTime CreateTime { get; set; }

        public string DepartDesc { get; set; }
    }
}
