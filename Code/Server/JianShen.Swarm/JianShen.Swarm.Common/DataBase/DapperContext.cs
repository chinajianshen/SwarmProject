using JianShen.Swarm.Common.Config;
using System;
using System.Collections.Generic;
using System.Text;

namespace JianShen.Swarm.Common.DataBase
{
    public class DapperContext : IDapperContext
    {
        private readonly WebApiOption _Config;

        public DapperContext(WebApiOption config)
        {
            _Config = config;
        }
        public string GetConnectionString() {
            return _Config.DB.ConnectionString;
        }
    }
}
