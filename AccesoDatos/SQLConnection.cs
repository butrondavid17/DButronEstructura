using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos
{
    public class SQLConnection
    {
        public static string GetConnection()
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["DButronEstructura"].ToString();
        }
    }
}
