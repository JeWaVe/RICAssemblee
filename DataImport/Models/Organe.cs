using System;
using System.Collections.Generic;
using System.Text;

namespace RICAssemblee.DataImport.Models
{
    public class Organe
    {
        public static bool TryParse(RawData.Organe rawOrgane, out Organe result)
        {
            result = new Organe();
            return false;
        }
    }
}
