using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Configuration;
namespace BooksWebapi.DataBase
{
    public class DB
    {
        public static string getConnection
        {
            get
            {
                //获取和连接字符串（ConnectionStrings配置节点）
                return ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;
               
            }
        }
    }
}