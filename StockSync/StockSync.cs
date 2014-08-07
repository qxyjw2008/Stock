﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.IO.Compression;
using System.Text.RegularExpressions;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Data;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Data.Common;

//using System.Windows.Forms;

using StockCommon;
using HtmlAgilityPack;

namespace StockSync
{
    public class StockDataSync
    {
        public StockDataSync()
        {
            
        }

        /// <summary>
        /// 同步所有股票信息
        /// 1.增加新股票
        /// 2.同步股票名称变动
        /// </summary>
        public static void SyncStockList()  
        {
            Configuration config = new Configuration();
            //string s = "server=localhost;uid=shenghai;pwd=123465;database=stock;"; /*System.Windows.Forms.Application.StartupPath + "\\StockService\\Setting.config";*/
            //string s = "E:\\Users\\shenghai\\Desktop\\Stock\\StockServiceUITest\\bin\\Debug\\StockService\\Setting.config";

            string strConfig = System.AppDomain.CurrentDomain.BaseDirectory + "Setting.config";
            ConfigLoader.Load(strConfig, config);
            LogManager.WriteLog(LogManager.LogFile.Trace, "Setting.config Path: " + strConfig);


            string tagUrl = Configuration.StockList;
            LogManager.WriteLog(LogManager.LogFile.Trace, "Configuration.StockList: " + tagUrl);

            CookieCollection cookies = new CookieCollection();
            HttpWebResponse response = HttpWebResponseUtility.CreateGetHttpResponse(tagUrl, null, null, cookies);
            Stream stream = response.GetResponseStream();
            stream.ReadTimeout = 15 * 1000; //读取超时
            StreamReader sr = new StreamReader(stream, Encoding.GetEncoding("gb2312"));
            string strWebData = sr.ReadToEnd();
            List<Stock> datas = new List<Stock>();//定义1个列表用于保存结果  
            HtmlDocument htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(strWebData);//加载HTML字符串，如果是文件可以用htmlDocument.Load方法加载  
            HtmlNode nodes = htmlDocument.DocumentNode;
            HtmlNodeCollection collection = nodes.SelectNodes("//body/div/div/div/ul");
            foreach (HtmlNode node in collection)
            {
                //去除\r\n以及空格，获取到相应td里面的数据  
                string[] line = node.InnerText.Split(new char[] { '\r', '\n', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string item in line)
                {
                    string tmp = item.Replace(")", "");
                    string[] st = tmp.Split('(');
                    if (st[1].StartsWith("00") || st[1].StartsWith("6") || st[1].StartsWith("3"))
                    {
                        datas.Add(new Stock() { StockName = st[0], StockCode = st[1] });
                    }
                }
            }

            SyncDatabase(ref datas);
        }

        private static void SyncDatabase(ref List<Stock> datas)
        {
            
            DbUtility util = new DbUtility(Configuration.SqlConnectStr, DbProviderType.MySql);

            foreach (Stock stock in datas)
            {
                string sql = string.Format("select * from STOCK where STOCKCODE={0}",stock.StockCode);
                DataTable table = util.ExecuteDataTable(sql, null);
                List<Stock> _table = EntityReader.GetEntities<Stock>(table);
                if (_table.Count < 1)
                {
                    string sqlInsert = string.Format("insert into STOCK values('{0}','{1}')", stock.StockName, stock.StockCode);
                    try
                    {
                        int reCount = util.ExecuteNonQuery(sqlInsert, null);
                    }
                    catch (System.Exception e)
                    {
                    	
                    }                   
                }
                else if (_table[0].StockName != stock.StockName)
                {
                    string sqlUpdate = string.Format("update STOCK set STOCKNAME='{0}' where STOCKCODE={1}", stock.StockName, stock.StockCode);
                    try
                    {
                        int reCount = util.ExecuteNonQuery(sqlUpdate, null);
                    }
                    catch (System.Exception e)
                    {

                    }  
                }
            }
        }

        /// <summary>
        /// 同步股票交易信息
        /// 新增每日交易信息
        /// 同步3个月之内所有股票日交易详细信息
        /// </summary>
        public static void SyncStockDataDetaileList()
        {
            DbUtility util = new DbUtility(Configuration.SqlConnectStr, DbProviderType.MySql);
            string sql = string.Format("select * from STOCK");
            DataTable table = util.ExecuteDataTable(sql, null);
            List<Stock> _table = EntityReader.GetEntities<Stock>(table);
            foreach ( Stock stock in _table )
            {
                string resUrl = StockLogic.GenetateStockUrl(stock.StockCode, true);
            }
        }


    }
}
