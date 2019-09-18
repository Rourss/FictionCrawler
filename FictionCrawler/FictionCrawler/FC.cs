using FictionCrawler.FictionAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FictionCrawler
{
    public partial class Fiction : Form
    {
        public Fiction()
        {
            InitializeComponent();
            Test();
        }
        public void Test()
        {
            GetHtmlByURL html = new GetHtmlByURL();
            txtBookIntro.Text = html.Html(1, "https://www.qidian.com/all?chanId=12&subCateId=66&orderId=&style=1&pageSize=20&siteid=1&pubflag=0&hiddenField=0&page=");
        }
    }
}
