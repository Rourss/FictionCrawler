using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FictionCrawler.MessageException
{
    public class NoFound:Exception
    {
        public NoFound() //构造函数
            : base("未找到符合的书籍") //调用父类构造函数
        {

        }
        public NoFound(string message) // 构造函数
            : base(message) //调用父类构造函数
        {

        }
    }
}
