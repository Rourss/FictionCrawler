using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.IO;
using System.Net;
using FictionCrawler.CommVerify;
using FictionCrawler.MessageException;
using System.Threading;
using System.Windows.Forms;

namespace FictionCrawler.FictionAccess
{
    public class GetBookInfoByHtml
    {
        public static Dictionary<string, string> bookInfo = new Dictionary<string, string>();
        public static Dictionary<string, string> bookIDCover = new Dictionary<string, string>();
        /// <summary>
        /// 通过传入html获取书籍信息的方法
        /// </summary>
        /// <param name="html"></param>
        public void BookInfo(string html)
        {
            var htmlDoc = new HtmlAgilityPack.HtmlDocument();
            htmlDoc.LoadHtml(html);
            try
            {
                for (int i = 0; i < 3; i++)
                {
                    var name = htmlDoc.DocumentNode.SelectNodes("//div[@class='book-mid-info']//h4/a")[i];
                    //var info = htmlDoc.DocumentNode.SelectNodes("//div[@class='book-mid-info']//p [@class='intro']")[i];
                    var info = htmlDoc.DocumentNode.SelectNodes("//div[@class='book-mid-info']//h4//a")[i].Attributes["href"].Value;
                    var image = htmlDoc.DocumentNode.SelectNodes("//div[@class='book-img-box']//img")[i].Attributes["src"].Value;
                    if (Examine.IsNull(name.InnerText, BookIntro(info), image))
                    {
                        bookInfo.Add(name.InnerText, BookIntro(info));
                        StreamFill(BookImage(name.InnerText,image), name.InnerText, BookIntro(info));
                        
                    }else
                    {
                        throw new NoFound();
                    }
                }
            }
            catch
            {
                throw new NoFound();
            }
        }
        /// <summary>
        /// 创建文件，用于存放书籍信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="bookname"></param>
        /// <param name="bookinfo"></param>
        public void StreamFill(int id, string bookname, string bookinfo)
        {
            try
            {
                string path = "D:\\Fiction";
                if (Directory.Exists(path))
                {
                    DirectoryInfo info = Directory.CreateDirectory(path + "\\" + id);
                    StreamWriter sw = new StreamWriter(info.FullName + "\\" + id + ".txt", true);
                    sw.WriteLine(bookname + "\r\n" + bookinfo + "\r\n");
                    sw.Close();
                }
            }
            catch
            {
                throw new NoCreate();
            }
        }
        /// <summary>
        /// 获取书籍封面的方法
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public int BookImage(string bookname,string url)
        {
            try
            {
                url = String.Format("{0}" + url, "https:");
                string id = Examine.Patt(url);
                HttpWebRequest http = WebRequest.Create(url) as HttpWebRequest;
                http.Timeout = 30 * 1000;
                HttpWebResponse https = http.GetResponse() as HttpWebResponse;

                string path = "D:\\Fiction";
                if (Directory.Exists(path))
                {
                    DirectoryInfo info = Directory.CreateDirectory(path + "\\" + id);
                    FileStream fs = new FileStream(info.FullName + "\\" + id + ".jpg", FileMode.Create);
                    https.GetResponseStream().CopyTo(fs);
                    fs.Close();
                    http.Abort();
                    http = null;
                    https.Close();
                    https = null;
                    bookIDCover.Add(bookname, info.FullName + "\\" + id + ".jpg");
                }
                return Convert.ToInt32(id);
            }
            catch
            {
                throw new NoFound();
            }
        }
        /// <summary>
        /// 获取全部简介的方法
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public string BookIntro(string url)
        {
            try
            {
                string html="";
                url = String.Format("{0}" + url, "https:");
                HttpWebRequest http = WebRequest.Create(url) as HttpWebRequest;
                http.Timeout = 30 * 1000;
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
                var htmlDoc = new HtmlAgilityPack.HtmlDocument();
                htmlDoc.LoadHtml(html);
                var info = htmlDoc.DocumentNode.SelectNodes("//div[@class='book-intro']/p").First();
                return info.InnerText.Trim();
            }
            catch
            {
                throw new NoFound();
            }
        }
    }
}
