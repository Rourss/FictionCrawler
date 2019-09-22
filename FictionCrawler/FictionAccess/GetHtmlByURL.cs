using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using FictionCrawler.MessageException;
using System.Windows.Forms;

namespace FictionCrawler.FictionAccess
{
  public class GetHtmlByURL
    {
        /// <summary>
        /// 起点小说网的方法
        /// 通过传入URL和设置页数，返回一个html
        /// </summary>
        /// <returns></returns>
        public string Html(int page,string url)
        {
            string html = "";
            //url = string.Format(url+"{0}", page);
            url = url.Replace("page=1", "page="+page);
            HttpWebRequest http = WebRequest.Create(url) as HttpWebRequest;
            http.KeepAlive = false;
            http.Timeout = 30 * 1000;
            http.Method = "GET";
            http.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3";
            http.Host = "www.qidian.com";
            //http.Referer = url;
            http.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/74.0.3729.169 Safari/537.36";
            try
            {
                HttpWebResponse https = http.GetResponse() as HttpWebResponse;
                using (StreamReader s = new StreamReader(https.GetResponseStream()))
                {
                    while (!s.EndOfStream)
                    {
                        html += s.ReadLine();
                    }
                    //html = s.ReadToEnd();
                    s.Close();
                    http.Abort();
                    https.Close();
                    http = null;
                    https = null;
                }
                return html;
            }
            catch
            {
                throw new ConnectionTimeout();
            }
        }
    }
}
