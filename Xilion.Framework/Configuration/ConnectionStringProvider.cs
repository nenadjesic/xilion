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
            var connection = "Data Source=DESKTOP-16EGH6R\\SQLEXPRESS; UID=sa; Password=sa;Database=Xilion";
            return connection;
        }
    }
}