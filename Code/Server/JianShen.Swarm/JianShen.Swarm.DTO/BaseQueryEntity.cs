using JianShen.Swarm.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace JianShen.Swarm.DTO
{
   public class BaseQueryEntity: DTOBase
    {
        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        /// <summary>
        /// Where条件  如：where id=1 and name='li'
        /// </summary>
        public string WhereCondition { get; set; }

        /// <summary>
        /// 排序条件(不要加order by 关键字) 如 id desc 
        /// </summary>
        public string OrderBy { get; set; }

        /// <summary>
        /// 查询条件占位符参数
        /// 如果查询条件：：where id=@id and name=@name
        /// 查询参数设置成： new { id =1,name="li"}
        /// </summary>
        public object Parameters { get; set; }

    }
}
