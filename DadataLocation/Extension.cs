using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace DadataLocation
{
    public static class Extension
    {
        public static double ToDouble(this string value)
        {
            var val = value.Replace('.',',');
            double res;

            double.TryParse(val, out res);

            return res;
        }
    }
}
