using System.Threading.Tasks;
using System.Configuration;
using Microsoft.Data.SqlClient;

namespace Investor.Core
{
    public class MainDataConnector : SqlConnector
    {

        #region Protected Members

        protected override string CnnString { get; } = ConfigurationManager.ConnectionStrings["InvestorDB"].ConnectionString;

        #endregion



    }

}
