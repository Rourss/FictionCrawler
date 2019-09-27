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
        private bool stop;
        private bool isTrigger;
        public Fiction()
        {
            InitializeComponent();
            isTrigger = true;
            stop = false;
            pbFiction.WaitOnLoad = false;
            pbFiction.UseWaitCursor = true;
            DriveInfo[] dri = DriveInfo.GetDrives();
            string path = "D:\\Fiction";
            foreach (var item in dri)
            {
                if (item.Name == "D:\\")
                {
                    if (Directory.Exists(path))
                    {
                        if (MessageBox.Show("是否加载上次爬到的内容?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            //执行加载方法
                            Loading();
                        }
                    }
                    DirectoryInfo info = Directory.CreateDirectory(item.Name + "\\Fiction");
                }
            }
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
                            if (stop) { break; }
                            fictionhtml = html.Html(page, url);
                            BookInfo(fictionhtml);
                            if (stop) { MessageBox.Show("已停止!"); } else { MessageBox.Show("第" + page + "页爬取完成!"); }
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
                    stop = false;
                    startBook.IsBackground = true;
                    startBook.Start();
                }
                else
                {
                    stop = true;
                    btnStart.Text = "开始";
                    btnClear.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void ShowException()
        {
            throw new Exception("已停止");
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
                        if (stop) { break; }
                        var name = htmlDoc.DocumentNode.SelectNodes("//div[@class='book-mid-info']//h4/a")[i];
                        bookname = name.InnerText;
                        var info = htmlDoc.DocumentNode.SelectNodes("//div[@class='book-mid-info']//h4//a")[i].Attributes["href"].Value;
                        var image = htmlDoc.DocumentNode.SelectNodes("//div[@class='book-img-box']//img")[i].Attributes["src"].Value;

                        if (Examine.IsNull(bookname, info, image))
                        {
                            string path = "";
                            intro = getBookInfo.BookIntro(info);
                            id = getBookInfo.BookImage(bookname, image);
                            bookInfo.Add(bookname, intro);
                            getBookInfo.StreamFill(id, bookname, intro);
                            GetBookInfoByHtml.bookIDCover.TryGetValue(bookname, out path);
                            getBookInfo.StreamAll(bookname, intro, path);
                            this.Invoke(new Action(() => { lbInfo.Items.Add(bookname); }));
                        }
                        else
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
                        bookInfo.Clear();
                        GetBookInfoByHtml.bookIDCover.Clear();
                        lbInfo.Items.Clear();
                        txtBookName.Text = "";
                        txtBookIntro.Text = "";
                        pbFiction.Image = null;
                        Directory.Delete(path, true);
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
        private void Loading()
        {
            try
            {
                string path = "D:\\Fiction";
                DirectoryInfo fileInfo = new DirectoryInfo(path);
                FileInfo[] file = fileInfo.GetFiles("All.txt");
                StreamReader sr = new StreamReader(path + "\\All.txt", Encoding.UTF8);
                while (!sr.EndOfStream)
                {
                    string bookInfo = sr.ReadLine();
                    lbInfo.Items.Add(Examine.PattBookInfo(1, bookInfo));
                }
                isTrigger = false;
                btnStart.Enabled = false;
                btnClear.Enabled = false;
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
                if (isTrigger)
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
                else
                {
                    int index = lbInfo.IndexFromPoint(e.X, e.Y);
                    lbInfo.SelectedIndex = index;
                    if (lbInfo.SelectedIndex != -1)
                    {
                        string item = lbInfo.SelectedItem.ToString();
                        SelectBookInfo(item);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void SelectBookInfo(string bookname)
        {
            try
            {
                string path = "D:\\Fiction";
                if (Directory.Exists(path))
                {
                    StreamReader sr = new StreamReader(path + "\\All.txt", Encoding.UTF8);
                    while (!sr.EndOfStream)
                    {
                        string bookInfo = sr.ReadLine();
                        if (bookInfo.Contains(bookname))
                        {
                            txtBookName.Text = Examine.PattBookInfo(1, bookInfo);
                            pbFiction.LoadAsync(Examine.PattBookInfo(2, bookInfo));
                            txtBookIntro.Text = Examine.PattBookInfo(3, bookInfo);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
