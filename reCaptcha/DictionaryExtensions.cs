using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace reCaptcha
{
    // TODO: move to external pack
    public static class DictionaryExtensions
    {
        public static string ToQueryString(this NameValueCollection nvc, bool prefixQuestionMark = true)
        {
            if (nvc == null)
                return string.Empty;

            if (nvc.Count == 0)
                return string.Empty;

            var array = (from key in nvc.AllKeys
                         from value in nvc.GetValues(key)
                         select string.Format("{0}={1}",
                            HttpUtility.UrlEncode(key),
                            HttpUtility.UrlEncode(value))
                         ).ToArray();

            return (prefixQuestionMark ? "?" : string.Empty) + string.Join("&", array);
        }
    }
}
