using System.Globalization;

namespace WednesdayTalkLabs
{
    public class Program
    {
        public enum EnumLoadErrorType
        {
            InvalidInput = 0
        }
        static void Main(string[] args)
        {
            int line = 1;
            string column = "A";
            int idLoad = 10;
            string number = "2,123";

            var result = TryConvertDecimal(number, line, column, idLoad, true);
            Console.WriteLine(result);
        }


        public static decimal? TryConvertDecimal(string number, int line, string column, int idLoad, bool canBeNull)
        {
            decimal result = 0;
            if ((string.IsNullOrWhiteSpace(number) && !canBeNull) || (!decimal.TryParse(number, new CultureInfo("pt-BR"), out result)) || !ValidateDecimalPrecisionScale(result, 20,9) )
            {
                AddErrorMessage(string.Format("Linha {0} - Coluna {1}", line, column), EnumLoadErrorType.InvalidInput, idLoad);

            }

            return result;
        }



        public static bool ValidateDecimalPrecisionScale(decimal result, int precision, int scale)
        {
            var dec = new System.Data.SqlTypes.SqlDecimal(result);
            return
                dec.Precision <= precision &&
                dec.Scale <= scale &&   
                dec.Precision - dec.Scale <= (precision - scale);
        }



        private static void AddErrorMessage(string msg, EnumLoadErrorType errorType, int idLoad)
        {
            Console.WriteLine($"{msg}");
        }
    }
}