using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Web1.Utils
{
    public static class XString
    {
        public static string Encode(this String s)
        {
            var bytes = Encoding.Unicode.GetBytes(s);
            String ss = Convert.ToBase64String(bytes);
            return ss;
        }
        public static String Decode(this String s)
        {
            var bytes = Convert.FromBase64String(s);
            String ss = Encoding.Unicode.GetString(bytes);
            return ss;
        }

        public static String Md5(this String s)
        {
            var bytes = Encoding.Unicode.GetBytes(s);
            var md5Bytes = MD5.Create().ComputeHash(bytes);
            String ss = Convert.ToBase64String(md5Bytes);
            return ss;
        }
    }
}