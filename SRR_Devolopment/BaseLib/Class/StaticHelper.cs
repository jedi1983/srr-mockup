using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace SRR_Devolopment.BaseLib.Class
{
    public static class StaticHelper
    {
        public static string HighlightKeyWords(this string text, string keywords, string cssClass, bool fullMatch)
        {
            if (text == String.Empty || keywords == String.Empty || cssClass == String.Empty)
                return text;
            var words = keywords.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            if (!fullMatch)
                return words.Select(word => word.Trim()).Aggregate(text,
                             (current, pattern) =>
                             Regex.Replace(current, pattern, string.Format("<span style=\"background-color:{0}\">{1}</span>", cssClass, "$0"), RegexOptions.IgnoreCase));
            //return words.Select(word => word.Trim()).Aggregate(text,(current, pattern) =>Regex.Replace(current, pattern,pattern.ToUpper(), RegexOptions.IgnoreCase));
            return words.Select(word => "\\b" + word.Trim() + "\\b")
                        .Aggregate(text, (current, pattern) =>
                                  Regex.Replace(current,
                                  pattern,
                                    string.Format("<span style=\"background-color:{0}\">{1}</span>",
                                    cssClass,
                                    "$0"),
                                    RegexOptions.IgnoreCase));
            

        }
    }
}
