using FictionCrawler.CommVerify;
using FictionCrawler.FictionAccess;
using FictionCrawler.MessageException;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FictionCrawler
{
    public partial class Fiction : Form
    {
        public Dictionary<string, string> bookInfo = new Dictionary<string, string>();
        public Fiction()
        {
            InitializeComponent();
            System.GC.Collect();
            bookInfo.Clear();
            GetBookInfoByHtml.bookIDCover.Clear();
            pbFiction.WaitOnLoad = false;
            pbFiction.UseWaitCursor = true;
            string path = "D:\\GitRepository\\FictionCrawler\\FictionCrawler\\/Fiction/";
            if (Directory.Exists(path) == false)
            {
                Directory.CreateDirectory(path);
            }
            else
            {
                
            }
        }
        private void btnStart_Click(object sender, EventArgs e)
        {
            string path = "D:\\GitRepository\\FictionCrawler\\FictionCrawler\\/Fiction/";
            if (Directory.Exists(path) == false)
            {
                Directory.CreateDirectory(path);
            }
            string url = "";
            if (txtURL.Text != "")
            {
                url = txtURL.Text;
            }
            else
            {
                url = "https://www.qidian.com/all?chanId=12&subCateId=66&orderId=&style=1&pageSize=20&siteid=1&pubflag=0&hiddenField=0&page=1";
            }
            try
            {
                Thread startBook = new Thread(() => { });
                if (btnStart.Text == "开始")
                {
                    //Dispose(true);
                    GetHtmlByURL html = new GetHtmlByURL();
                    string fictionhtml = "";
                    btnStart.Text = "停止";
                    btnClear.Enabled = false;
                    int index = 1;
                    int page = 1;

                    while (index < 3)
                    {
                        fictionhtml = html.Html(index, url);

                        startBook = new Thread(() =>
                        {
                            BookInfo(fictionhtml);
                            MessageBox.Show("第" + page + "页爬取完成!");
                            page++;
                        });
                        startBook.IsBackground = true;
                        startBook.Start();
                        MessageBox.Show(Thread.CurrentThread.Name);
                        index++;
                    }
                }
                else
                {
                    MessageBox.Show("??");
                    startBook.Abort();
                    startBook.Abort();
                    //startBook.wait()
                    btnStart.Text = "开始";
                    btnClear.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void BookInfo(string html)
        {
            lock ("book")
            {
                var bookname = "";
                var intro = "";
                var id = 0;
                GetBookInfoByHtml getBookInfo = new GetBookInfoByHtml();
                var htmlDoc = new HtmlAgilityPack.HtmlDocument();
                htmlDoc.LoadHtml(html);
                try
                {
                    for (int i = 0; i < 20; i++)
                    {
                        var name = htmlDoc.DocumentNode.SelectNodes("//div[@class='book-mid-info']//h4/a")[i];
                        bookname = name.InnerText;
                        var info = htmlDoc.DocumentNode.SelectNodes("//div[@class='book-mid-info']//h4//a")[i].Attributes["href"].Value;
                        var image = htmlDoc.DocumentNode.SelectNodes("//div[@class='book-img-box']//img")[i].Attributes["src"].Value;

                        if (Examine.IsNull(bookname, info, image))
                        {
                            intro = getBookInfo.BookIntro(info);
                            id = getBookInfo.BookImage(bookname, image);
                            bookInfo.Add(bookname, intro);
                            getBookInfo.StreamFill(id, bookname, intro);
                            this.Invoke(new Action(() => { lbInfo.Items.Add(bookname); }));
                        }
                        else
                        {
                            throw new NoFound();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void btnEnd_Click(object sender, EventArgs e)
        {
            try
            {
                string path = "D:\\GitRepository\\FictionCrawler\\FictionCrawler\\/Fiction/";
                if (Directory.Exists(path) == true)
                {
                    Directory.Delete(path, true);
                    bookInfo.Clear();
                    GetBookInfoByHtml.bookIDCover.Clear();
                    lbInfo.Items.Clear();
                    txtBookName.Text = "";
                    txtBookIntro.Text = "";
                    pbFiction.Image = null;
                    if (Directory.Exists(path) == false)
                    {
                        MessageBox.Show("清除成功!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void lbInfo_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                int index = lbInfo.IndexFromPoint(e.X, e.Y);
                lbInfo.SelectedIndex = index;
                if (lbInfo.SelectedIndex != -1)
                {
                    string item = lbInfo.SelectedItem.ToString();
                    string intro = string.Empty;
                    string path = string.Empty;
                    GetBookInfoByHtml.bookIDCover.TryGetValue(item, out path);
                    if (bookInfo.ContainsKey(item) && bookInfo.TryGetValue(item, out intro) && path != "")
                    {
                        txtBookName.Text = item;
                        txtBookIntro.Text = intro;
                        pbFiction.LoadAsync(@path);
                    }
                }
            }
            catch 
            {
                throw new NoFound();
            }
        }
    }
}
