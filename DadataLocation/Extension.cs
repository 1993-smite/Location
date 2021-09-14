namespace DadataLocation
{
    public static class Extensions
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
