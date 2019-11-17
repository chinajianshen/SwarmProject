using System;
using System.Collections.Generic;
using System.Text;

namespace JianShen.Swarm.Common.DataBase
{
    /// <summary>
    /// Dapper上下文
    /// </summary>
    public interface IDapperContext
    {
        string GetConnectionString();
    }
}

