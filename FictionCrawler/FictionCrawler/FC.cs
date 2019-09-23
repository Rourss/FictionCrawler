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
            DriveInfo[] dri = DriveInfo.GetDrives();
            foreach (var item in dri)
            {
                if (item.Name =="D:\\")
                {
                    DirectoryInfo info = Directory.CreateDirectory(item.Name + "\\Fiction");
                }
            }
            //string path = "D:\\GitRepository\\FictionCrawler\\FictionCrawler\\/Fiction/";
            //if (Directory.Exists(path) == false)
            //{
            //    Directory.CreateDirectory(path);
            //}
        }
        private void btnStart_Click(object sender, EventArgs e)
        {
            DriveInfo[] dri = DriveInfo.GetDrives();
            foreach (var item in dri)
            {
                if (item.Name == "D:\\")
                {
                    DirectoryInfo info = Directory.CreateDirectory(item.Name + "\\Fiction");
                }
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
                GetHtmlByURL html = new GetHtmlByURL();
                string fictionhtml = "";
                int page = 1;
                Thread startBook = new Thread(() =>
                {
                    try
                    {
                        while (page < 6)
                        {
                            fictionhtml = html.Html(page, url);
                            BookInfo(fictionhtml);
                            MessageBox.Show("第" + page + "页爬取完成!");
                            page++;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("出现：" + ex.Message + " 错误" + "\r\n爬取已取消!");
                    }
                });
                if (btnStart.Text == "开始")
                {
                    btnStart.Text = "停止";
                    btnClear.Enabled = false;
                    startBook.Start();
                }
                else
                {
                    startBook.Abort();
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
                        }else
                        {
                            MessageBox.Show("未找到书籍!");
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
                string path = "D:\\/Fiction/";
                if (Directory.Exists(path) == true)
                {
                    if (MessageBox.Show("确定要删除吗？", "删除保存的书籍", MessageBoxButtons.YesNo) == DialogResult.Yes)
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
