using BooksWebapi.App_Start;
using BooksWebapi.DataBase;
using BooksWebapi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.Results;

namespace BooksWebapi.Controllers
{
    [RoutePrefix("api/BooksInfo")]
    public class BooksInfoController : ApiController
    {
        /// <summary>
        /// 查询所有数据
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>

        [Route("")]
        [HttpGet]
        public HttpResponseMessage GetAll(dynamic obj)
        {   //初始化实例
            SqlConnection conn = new SqlConnection(DB.getConnection);
            //打开数据库
            conn.Open();
            string sql = "select * from Books;";
            try
            {
                DataSet ds = new DataSet();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sql, conn);
                sqlDataAdapter.Fill(ds);
                var result = SerializerExtendMethod.toJson(ConvertJson.ToJson(ds.Tables[0]));
                conn.Close();
                return result;
            }
            catch (Exception ex)
            {
                HttpResponseMessage httpResponseMessage = Request.CreateResponse("value");
                SerializerExtendMethod.LogWrite(ex.Message);
                return httpResponseMessage;
            }
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="books"></param>
        /// <returns></returns>
        [Route("addBooks")]
        [HttpPost]
        public bool AddBooks(Books books)
        {
            SqlConnection conn = new SqlConnection(DB.getConnection);
            conn.Open();
            string sql = "insert into [BooksInfo].[dbo].[Books](bookName,Author,Publication_date,Remark)values(";
            Type t = books.GetType();
            foreach (PropertyInfo pi in t.GetProperties())
            {
                if (pi.Name == "ID")
                {
                    continue;
                }
                //返回对象指定的属性值
                if (pi.GetValue(books, null) != null)
                {
                    string value = pi.GetValue(books, null).ToString().Trim();
                    if (value != "")
                    {
                        sql += "'" + value + "',";
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            sql = sql.Substring(0, sql.Length - 1);
            sql += ")";
            try
            {
                DataSet ds = new DataSet();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sql, conn);
                sqlDataAdapter.Fill(ds);
                conn.Close();
                return true;
            }
            catch (Exception ex)
            {
                SerializerExtendMethod.LogWrite(ex.Message);
                return false;
            }
        }

        [Route("delete")]
        [HttpPost]
        public bool deleteBooksById(string[] ids)
        {

            SqlConnection conn = new SqlConnection(DB.getConnection);
            conn.Open();
            bool b = false;
            if (ids.Length == 0)
            {
                SerializerExtendMethod.LogWrite("id未获取到");
                return false;
            }
            else
            {
                for (int i = 0; i < ids.Length; i++)
                {

                string sql = "delete from [BooksInfo].[dbo].[Books] where ID =" + ids[i];
                try
                {
                    DataSet dataSet = new DataSet();
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sql, conn);
                    sqlDataAdapter.Fill(dataSet);
                }
                catch (Exception ex)
                {
                    SerializerExtendMethod.LogWrite(ex.Message);
                    b = false;
                }
                }
            }

            conn.Close();
            return b;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="limit">页面大小（几行）</param>
        /// <param name="offset">页码</param>
        /// <param name="BookName"></param>
        /// <param name="Author"></param>
        /// <returns></returns>
        [Route("GetInfoBybookNameAndAuthor")]
        [HttpPost]
        public HttpResponseMessage GetInfoBybookNameAndAuthor(dynamic obj)
        {
            //初始化实例
            SqlConnection conn = new SqlConnection(DB.getConnection);
            //打开数据库
            conn.Open();
            try
            {
                string sql = "select * from [BooksInfo].[dbo].[Books]  where 1=1 ";
                var BookName = obj.BookName;
                var Author = obj.Author;
                if (BookName != "" && BookName != null)
                {
                    sql += "and BookName='" + BookName + "'";
                }
                if (Author != "" && Author != null)
                {
                    sql += "and Author='" + Author + "'";
                }
                sql += "order by ID  offset 0 rows fetch next 5 rows only";
                DataSet ds = new DataSet();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sql, conn);
                sqlDataAdapter.Fill(ds);
                var result = SerializerExtendMethod.toJson(ConvertJson.ToJson(ds.Tables[0]));
                conn.Close();
                return result;
            }
            catch (Exception ex)
            {
                HttpResponseMessage httpResponseMessage = Request.CreateResponse("value");
                SerializerExtendMethod.LogWrite(ex.Message);
                return httpResponseMessage;
            }

        }

    }
}
