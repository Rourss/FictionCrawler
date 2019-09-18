using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FictionCrawler.MessageException
{
   public class ConnectionTimeout:Exception
    {
        public ConnectionTimeout() 
            : base("连接超时") //调用父类构造函数
        {

        }
        public ConnectionTimeout(string message)
            : base(message) //调用父类构造函数
        {

        }
    }
}
