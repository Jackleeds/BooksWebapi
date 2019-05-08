using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Configuration;
namespace BooksWebapi.App_Start
{
    //创建json对象数据转json字符串和log日志
    public class SerializerExtendMethod
    {
        public static HttpResponseMessage toJson(Object obj)
        {
            String str;
            if (obj is String || obj is Char)
            {
                str  = obj.ToString();
            }
            else
            {
                str = JsonConvert.SerializeObject(obj);
            }
            HttpResponseMessage result =  new HttpResponseMessage { Content = new StringContent(str, Encoding.GetEncoding("UTF-8"), "application/json") };
            return result;
        }
        public static void LogWrite(string content)
        {
            FileStream fs = new FileStream(ConfigurationManager.AppSettings["LogPath"], FileMode.Append);
            StreamWriter  sw=new StreamWriter(fs);
            sw.WriteLine(DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "\t" + content);//开始写入值
            sw.Close();
            fs.Close();//释放

        }
    }
}