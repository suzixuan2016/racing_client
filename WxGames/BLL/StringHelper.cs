﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace BLL
{
    public static class StringHelper
    {
        /// <summary>
        /// 从字符串中提取数字
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetNumber(this string str)
        {
            string result = "";
            if (str != null && str != string.Empty)
            {
                // 正则表达式剔除非数字字符（不包含小数点.）
                str = Regex.Replace(str, @"[^\d.\d]", "");
                // 如果是数字，则转换为decimal类型
                if (Regex.IsMatch(str, @"^[+-]?\d*[.]?\d*$"))
                {
                    result = str;
                }
            }
            return result;
        }

        public static bool ExitHanZi(this string str)
        {
            if (str != null && str != string.Empty)
            {
                if (Regex.IsMatch(str, @"[\u4e00-\u9fa5]"))
                {
                    return true;
                }
            }
            return false;
        }

        public static string String2HanZi(this string str)
        {
            String reg = "[^\u4e00-\u9fa5]";
            str = str.Replace(reg, "");
            return str;
        }

        /// <summary>
        /// 日期转换成unix时间戳
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static long DateTimeToUnixTimestamp(this DateTime dateTime)
        {
            var start = new DateTime(1970, 1, 1, 0, 0, 0, dateTime.Kind);
            return Convert.ToInt64((dateTime - start).TotalSeconds);
        }

        /// <summary>
        /// unix时间戳转换成日期
        /// </summary>
        /// <param name="unixTimeStamp">时间戳（秒）</param>
        /// <returns></returns>
        public static DateTime UnixTimestampToDateTime(this long timestamp,DateTimeKind kind)
        {
            var start = new DateTime(1970, 1, 1, 0, 0, 0, kind);
            return start.AddSeconds(timestamp);
        }

        public static int ToInt(this string s)
        {
            try
            {
                return Convert.ToInt32(s);
            }
            catch (Exception e)
            {
                Log.WriteLog(e);
                return 0;
            }
        }

        public static double ToDouble(this string s)
        {
            try
            {
                return Convert.ToDouble(s);
            }
            catch (Exception e)
            {
                Log.WriteLog(e);
                return -1;
            }
        }

        public static bool IsNum(this string str)
        {
            //Regex reg = new Regex(@"^[-]?[0-9]{1}\d*$|^[0]{1}$");
            Regex reg = new Regex(@"^[0-9]{1}\d*$|^[0]{1}$");
            return reg.IsMatch(str);
        }

        public static string ClearHtml(this string Content)
        {
            Content = Zxj_ReplaceHtml("&#[^>]*;", "*", Content);
            Content = Zxj_ReplaceHtml("</?marquee[^>]*>", "*", Content);
            Content = Zxj_ReplaceHtml("</?object[^>]*>", "*", Content);
            Content = Zxj_ReplaceHtml("</?param[^>]*>", "*", Content);
            Content = Zxj_ReplaceHtml("</?embed[^>]*>", "*", Content);
            Content = Zxj_ReplaceHtml("</?table[^>]*>", "*", Content);
            Content = Zxj_ReplaceHtml(" ", "*", Content);
            Content = Zxj_ReplaceHtml("</?tr[^>]*>", "*", Content);
            Content = Zxj_ReplaceHtml("</?th[^>]*>", "*", Content);
            Content = Zxj_ReplaceHtml("</?p[^>]*>", "*", Content);
            Content = Zxj_ReplaceHtml("</?a[^>]*>", "*", Content);
            Content = Zxj_ReplaceHtml("</?img[^>]*>", "*", Content);
            Content = Zxj_ReplaceHtml("</?tbody[^>]*>", "*", Content);
            Content = Zxj_ReplaceHtml("</?li[^>]*>", "*", Content);
            Content = Zxj_ReplaceHtml("</?span[^>]*>", "*", Content);
            Content = Zxj_ReplaceHtml("</?div[^>]*>", "*", Content);
            Content = Zxj_ReplaceHtml("</?th[^>]*>", "*", Content);
            Content = Zxj_ReplaceHtml("</?td[^>]*>", "*", Content);
            Content = Zxj_ReplaceHtml("</?script[^>]*>", "*", Content);
            Content = Zxj_ReplaceHtml("(javascript|jscript|vbscript|vbs):", "*", Content);
            Content = Zxj_ReplaceHtml("on(mouse|exit|error|click|key)", "*", Content);
            Content = Zxj_ReplaceHtml("<\\?xml[^>]*>", "*", Content);
            Content = Zxj_ReplaceHtml("<\\/?[a-z]+:[^>]*>", "*", Content);
            Content = Zxj_ReplaceHtml("</?font[^>]*>", "*", Content);
            Content = Zxj_ReplaceHtml("</?b[^>]*>", "*", Content);
            Content = Zxj_ReplaceHtml("</?u[^>]*>", "*", Content);
            Content = Zxj_ReplaceHtml("</?i[^>]*>", "*", Content);
            Content = Zxj_ReplaceHtml("</?strong[^>]*>", "*", Content);
            Content = Zxj_ReplaceHtml(@"\p{Cs}", "*", Content);
            string clearHtml = Content;
            return clearHtml;
        }

        private static string Zxj_ReplaceHtml(string patrn, string strRep, string content)
        {
            if (string.IsNullOrEmpty(content))
            {
                content = "";
            }
            Regex rgEx = new Regex(patrn, RegexOptions.IgnoreCase);
            string strTxt = rgEx.Replace(content, strRep);
            return strTxt;
        }
    }
}
