using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using FictionCrawler.MessageException;

namespace FictionCrawler.FictionAccess
{
  public class GetHtmlByURL
    {
        /// <summary>
        /// 通过传入URL和设置页数，返回一个html
        /// </summary>
        /// <returns></returns>
        public string Html(int page,string url)
        {
            string html = "";
            url = string.Format(url+"{0}", page);
            HttpWebRequest http = WebRequest.Create(url) as HttpWebRequest;
            http.KeepAlive = true;
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
                    html = s.ReadToEnd();
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
