using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FictionCrawler.MessageException
{
   public class NoCreate:Exception
    {
        public NoCreate() //构造函数
            : base("未能成功创建文件夹") //调用父类构造函数
        {

        }
        public NoCreate(string message) // 构造函数
            : base(message) //调用父类构造函数
        {

        }
    }
}
