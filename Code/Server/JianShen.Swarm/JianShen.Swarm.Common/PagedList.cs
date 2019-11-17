using System;
using System.Collections.Generic;
using System.Text;

namespace JianShen.Swarm.Common
{
    /// <summary>
    /// 分页实体类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PagedList<T> where T : class
    {
        public List<T> Items { get; set; }

        public int TotalCount { get; set; }
    }
}
