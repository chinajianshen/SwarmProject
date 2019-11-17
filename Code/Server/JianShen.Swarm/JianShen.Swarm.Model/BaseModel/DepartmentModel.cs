using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace JianShen.Swarm.Model.BaseModel
{
    [Table("JS_Departments")]
    public class DepartmentModel
    {
        [Key]
        public int DepartmentID { get; set; }

        public string DepartmentName { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
