using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;

namespace InvestorCore
{
    public static class SqlConnector
    {
        public static void TZ()
        {
            string CnnString = ConfigurationManager.ConnectionStrings["ServerCnn"].ConnectionString;

            using (var connection = new SqlConnection(CnnString))
            {
                //int counter = 0;
                //foreach (string currency in currencySymbols)
                //{
                //    var Vals = new DynamicParameters();
                //    Vals.Add("@Sym", currencySymbols.ToArray()[counter]);
                //    Vals.Add("@Name", currencyNames.ToArray()[counter]);

                //    connection.Execute("INSERT [System].[Currencies] VALUES (@Sym, @Name)", Vals);

                //    counter++;
                //}
            }

        }


    }
}
