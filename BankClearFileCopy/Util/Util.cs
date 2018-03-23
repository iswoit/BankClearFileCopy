using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace BankClearFileCopy
{
    public static class Util
    {
        private static char[] arrMddConvert;  // mdd格式的字典数组
        static Util()
        {
            arrMddConvert = new char[] { '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c' };
        }

        public static string ReplaceStringWithDateFormat(string fileName, DateTime dtNow)
        {
            string strTmp = fileName;   // 返回值

            string yyyymmdd_replacement = dtNow.ToString("yyyyMMdd");
            string yymmdd_replacement = dtNow.ToString("yyMMdd");
            string mmdd_replacement = string.Format("{0}{1}", dtNow.Month.ToString().PadLeft(2, '0'), dtNow.Day.ToString().PadLeft(2, '0'));
            string mdd_replacement = string.Format("{0}{1}", arrMddConvert[dtNow.Month - 1], dtNow.Day.ToString().PadLeft(2, '0'));

            
            strTmp = Regex.Replace(strTmp, "yyyymmdd", yyyymmdd_replacement, RegexOptions.IgnoreCase);  // 1.替换yyyymmdd
            strTmp = Regex.Replace(strTmp, "yymmdd", yymmdd_replacement, RegexOptions.IgnoreCase);      // 2.替换yymmdd
            strTmp = Regex.Replace(strTmp, "mmdd", mmdd_replacement, RegexOptions.IgnoreCase);          // 3.替换mmdd
            strTmp = Regex.Replace(strTmp, "mdd", mdd_replacement, RegexOptions.IgnoreCase);            // 4.替换mdd
            return strTmp;
        }
    }
}
