using System.Collections.Generic;
using System.Linq;
using System.Globalization;

namespace Investor.Core
{
    public static class GlobalInfo
    {

        public static List<string> GetCurrencies()
        {
            IEnumerable<string> Result = from Crn in CultureInfo.GetCultures(CultureTypes.SpecificCultures)
                                         let reg = new RegionInfo(Crn.LCID)
                                         orderby reg.ISOCurrencySymbol
                                         select $"{reg.ISOCurrencySymbol} - {reg.CurrencyEnglishName}";

            return Result.Distinct().ToList();
        }


    }
}
