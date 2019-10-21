using Microsoft.Extensions.Configuration;
using System;


namespace Xilion.Framework.Configuration
{
    public static class ConnectionStringProvider
    {
        private static  IConfiguration Configuration { get; }

        public static string GetConnectionString()
        {
            string machineName = Environment.MachineName;
            var connection = "Server=ACTKPTP210; User Id=sa; Password=sa;Initial Catalog=Xilion";
            return connection;
        }
    }
}