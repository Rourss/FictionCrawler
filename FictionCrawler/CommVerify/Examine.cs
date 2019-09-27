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
        public static string PattBookInfo(int index, string str)
        {
            if (index == 1)
            {
                var s = "书名：(.*?)[封$]";
                Regex ss = new Regex(s);
                Match math = ss.Match(str);
                return math.Groups[1].Value;
            }
            else if (index == 2)
            {
                var s = "封面：(.*?)[简$]";
                Regex ss = new Regex(s);
                Match math = ss.Match(str);
                return math.Groups[1].Value;
            }
            else if (index == 3)
            {
                var s = "简介：(.*?)$";
                Regex ss = new Regex(s);
                Match math = ss.Match(str);
                return math.Groups[1].Value;
            }
            else
            {
                return "";
            }
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
