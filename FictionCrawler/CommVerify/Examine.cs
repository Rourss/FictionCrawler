using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FictionCrawler.CommVerify
{
   public class Examine
    {
        public static string Patt(string str)
        {
            //var s = "《.+》";
            var s = "/1.*?/";
            Regex ss = new Regex(s);
            MatchCollection ma = ss.Matches(str);
            Match math = ss.Match(str);
            //return ma[0].Value;
            return math.Value.Replace("/", "");
        }
        public static bool IsNull(string name,string info,string image)
        {
            if(name == null || info == null || image == null)
            {
                return false;
            }
            if (name == "" || info == "" || image == "")
            {
                return false;
            }
            return true;
        }
    }
}
