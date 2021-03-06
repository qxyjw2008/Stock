﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net;
using System.IO;
using System.IO.Compression;
using System.Text.RegularExpressions;

using System.Data;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Data.Common;
using MySql.Data.MySqlClient;

using System.ServiceProcess;

using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

using StockCommon;
using HtmlAgilityPack;
using CsvHelper;
using StockSync;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
//using Newtonsoft.Json.JsonConvert;


namespace StockServiceUITest.TestCode
{
    public class LogManagerTest
    {
        public static void testLogManager()
        {
            LogManager.LogPath = "E://";
            LogManager.WriteLog(LogManager.LogFile.Trace, "shenghai");
        }
    }

    public class ServiceProcessTest
    {
        public static void testServe()
        {
            ServiceController controller = new ServiceController("StockService");
            controller.Stop();
        }
    }

    public class HttpTest
    {
        public static string testHttpGet()
        {
            string userName = "15528358573";

            string url = "http://quotes.money.163.com/service/chddata.html?code=1000686&start=20140725&end=20140801&fields=TCLOSE;HIGH;LOW;TOPEN;LCLOSE;CHG;PCHG;TURNOVER;VOTURNOVER;VATURNOVER;TCAP;MCAP";

            string tagUrl = Configuration.StockList; //"http://www.sina.com/";
            CookieCollection cookies = new CookieCollection();//如何从response.Headers["Set-Cookie"];中获取并设置CookieCollection的代码略  
            HttpWebResponse response = HttpWebResponseUtility.CreateGetHttpResponse(url, null, null, cookies);
            Stream stream = response.GetResponseStream();

            stream.ReadTimeout = 15 * 1000; //读取超时
            StreamReader sr = new StreamReader(stream, Encoding.GetEncoding("gb2312"));
            string strWebData = sr.ReadToEnd();
            return strWebData;
        }

        public static void testHttpPost()
        {
            //string loginUrl = "http://192.168.22.205:8089/DataDispacher.asmx/TestService";
            string loginUrl = "http://www.cuit.edu.cn/PassPort/Login.asp";
            //string loginUrl = "https://passport.baidu.com/v2/?login&fr=old";
            Encoding encoding = Encoding.GetEncoding("gb2312");

            IDictionary<string, string> parameters = new Dictionary<string, string>();

            parameters.Add("txtId", "2008081208");
            parameters.Add("txtMM", "Aa123456789");
            parameters.Add("x", "16");
            parameters.Add("y", "15");
            parameters.Add("WinW", "1280");
            parameters.Add("WinH", "984");
            parameters.Add("Login", "Check"); 
            //parameters.Add("Login", "Check&txtId=2008081208&txtMM=Aa123456789&WinW=1280&WinH=984&x=14&y=17");
           // parameters.Add("Name", "aaa");

            /*parameters.Add("tpl", "fa");
            parameters.Add("tpl_reg", "fa");
            parameters.Add("u", "https://passport.baidu.com/");
            parameters.Add("psp_tt", "0");
            parameters.Add("username", "15528358573");
            parameters.Add("password", "jWe10Yrbw9FBVNHFvo8GwAEqf4i1jt8DEWeVPaHJDH3Qp9atFcffLvnQid1HuJA36SsFuT47q/B5kWduv41VRpI3PUWfb1A9H1vmgg9s3jNxOKTE6Xh8+dZIL8G905ryVzF4Ara2r/sCVhmr/hk4pyKsgdSj8mv9yLlC0fnXjHQ=");
            parameters.Add("mem_pass", "1");*/

           

            //parameters.Add("password", password);
            //parameters.Add("mem_pass", "1");
            HttpWebResponse response = HttpWebResponseUtility.CreatePostHttpResponse(loginUrl, parameters, null, null, encoding, null);
            string cookieString = response.Headers["Set-Cookie"];

            Stream stream = response.GetResponseStream();

            stream.ReadTimeout = 15 * 1000; //读取超时
            StreamReader sr = new StreamReader(stream, Encoding.GetEncoding("gb2312"));
            string strWebData = sr.ReadToEnd();
            //return strWebData;
        }
    }

    public class DbUtilityTest
    {
        public static void testSql()
        {
            string conStr = "server=localhost;uid=shenghai;" + "pwd=123465;database=stock;";
            DbUtility util = new DbUtility(conStr, DbProviderType.MySql);
            DbDataReader reader = util.ExecuteReader("SELECT * FROM stock", null);
            DataTable table = util.ExecuteDataTable("SELECT * FROM stock", null);
            //List<stock> _reader = EntityReader.GetEntities<stock>(reader);
            List<stock> _table = EntityReader.GetEntities<stock>(table);
        }
    }

    public class ConfigTest
    {
        public static void testConfig()
        {
            var config = new Configuration();
            string s = System.Windows.Forms.Application.StartupPath + "\\StockService\\Setting.config";
            ConfigLoader.Load(s, config);
        }
    }

    public class MyLambda
    {
        public void disPlay()
        {
            string mid = ",middle part,";
            Func<string, string> lambda = param =>
            {
                param += mid; param += "and this was added to the string";
                return param;
            }; 
            Console.WriteLine(lambda("Start of string"));
        }
    } 

    public class SyncTest
    {
        public static void SyncStockDataDetaileList()
        {
            StockDataSync.SyncStockDataDetaileList();
        }

        public static void testQuery()
        {
            DateTime da = new DateTime(2008,12,26);
            //StockDataSync.IsStockItemExits(da);
        }

        public static void ComputeStockSide()
        {
            StockDataSync.ComputeStockSide();
        }

        public static void SyncLastUpdate()
        {
            StockDataSync.SyncLastUpdate();
        }

        public static void SyncTradeDate()
        {
            StockDataSync.SyncTradeAllDate();
        }

        public static void test()
        {
            StockDataSync.test();
        }

        public static void SyncStockDataDetaileListExt()
        {
            StockDataSync.SyncStockDataDetaileListExt();
        }
    }

    public class Test
    {
        public static void test()
        {
            string strWebContent = @"<table><thead>  
    <tr>  
      <th>时间</th>  
      <th>类型</th>  
      <th>名称</th>  
      <th>单位</th>  
      <th>金额</th>  
    </tr>  
    </thead>  
    <tbody>" +
    @"<tr>  
      <td>2013-12-29</td>  
      <td>发票1</td>  
      <td>采购物资发票1</td>  
      <td>某某公司1</td>  
      <td>123元</td>  
    </tr>" +
    @"<tr>  
      <td>2013-12-29</td>  
      <td>发票2</td>  
      <td>采购物资发票2</td>  
      <td>某某公司2</td>  
      <td>321元</td>  
    </tr>  
    </tbody>  
  </table>  
";


            List<Data> datas = new List<Data>();//定义1个列表用于保存结果  

            HtmlDocument htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(strWebContent);//加载HTML字符串，如果是文件可以用htmlDocument.Load方法加载  

            HtmlNodeCollection collection = htmlDocument.DocumentNode.SelectSingleNode("table/tbody").ChildNodes;//跟Xpath一样，轻松的定位到相应节点下  
            foreach (HtmlNode node in collection)
            {
                //去除\r\n以及空格，获取到相应td里面的数据  
                string[] line = node.InnerText.Split(new char[] { '\r', '\n', ' ' }, StringSplitOptions.RemoveEmptyEntries);

                //如果符合条件，就加载到对象列表里面  
                if (line.Length == 5)
                    datas.Add(new Data() { 时间 = line[0], 类型 = line[1], 名称 = line[2], 单位 = line[3], 金额 = line[4] });
            }

            //循环输出查看结果是否正确  
            foreach (var v in datas)
            {
                Console.WriteLine(string.Join(",", v.时间, v.类型, v.名称, v.单位, v.金额));
            }  

        }

        public static void testEqual()
        {
            StockListTmp a = new StockListTmp();
            StockListTmp b = new StockListTmp();
            a.stockcode = "123";
            b.stockcode = "123";

            a.stockname = "456";
            b.stockname = "456";
           // b = a;
            if (a.Equals(b))
            {
                int m = 0;
            }

        }

        public static void testlist()
        {
            string tagUrl = Configuration.StockList;//"http://www.sina.com/";
            CookieCollection cookies = new CookieCollection();//如何从response.Headers["Set-Cookie"];中获取并设置CookieCollection的代码略  
            HttpWebResponse response = HttpWebResponseUtility.CreateGetHttpResponse(tagUrl, null, null, cookies);
            Stream stream = response.GetResponseStream();

            stream.ReadTimeout = 15 * 1000; //读取超时
            StreamReader sr = new StreamReader(stream, Encoding.GetEncoding("gb2312"));
            string strWebData = sr.ReadToEnd();




            List<StockListTmp> datas = new List<StockListTmp>();//定义1个列表用于保存结果  

            HtmlDocument htmlDocument = new HtmlDocument();
            
            htmlDocument.LoadHtml(strWebData);//加载HTML字符串，如果是文件可以用htmlDocument.Load方法加载  


            HtmlNode nodes = htmlDocument.DocumentNode;
            HtmlNodeCollection collection = nodes.SelectNodes("//body/div/div/div/ul");
            foreach (HtmlNode node in collection)
            {
                //去除\r\n以及空格，获取到相应td里面的数据  
                string[] line = node.InnerText.Split(new char[] { '\r', '\n', ' ' }, StringSplitOptions.RemoveEmptyEntries);

                foreach(string item in line)
                {
                    string tmp = item.Replace(")", "");
                    string[] s = tmp.Split('(');
                    if (s[1].StartsWith("00") || s[1].StartsWith("6") || s[1].StartsWith("3"))
                    {
                        datas.Add(new StockListTmp() { stockname = s[0], stockcode = s[1]});
                    }
                }
                
            }
        }

        public static void kk()
        {
            //string resUrl = "http://quotes.money.163.com/1002560.html";
            //string resUrl = "http://quote.cfi.cn/quote.aspx?actcode=&actstockid=11601&searchcode=002560&x=0&y=0";
            string resUrl = "http://api.money.126.net/data/feed/1002560,money.api";
            CookieCollection cookies = new CookieCollection();
            HttpWebResponse response = HttpWebResponseUtility.CreateGetHttpResponse(resUrl, null, null, cookies);
            Stream stream = response.GetResponseStream();
            stream.ReadTimeout = 15 * 1000; //读取超时
            StreamReader sr = new StreamReader(stream, Encoding.GetEncoding("utf-8"));//gb2312
            string strWebData = sr.ReadToEnd();
            strWebData = strWebData.Replace("_ntes_quote_callback(", "");
            strWebData = strWebData.Replace(");", "");
           string kk = "{\"1002560\":{\"code\": \"1002560\", \"percent\": -0.000502, \"price\": 19.91}}";

            Newtonsoft.Json.Linq.JObject p = JsonConvert.DeserializeObject(strWebData, typeof(Newtonsoft.Json.Linq.JObject)) as Newtonsoft.Json.Linq.JObject;
            string y = p.Path;
            string t = p.First.First.ToString();
            JToken ee = p.First.First;
            string aa = ee["code"].ToString();
            double bb = Convert.ToDouble(ee["percent"].ToString());
            
            //Dictionary<string, string> hh = p.First.ToDictionary<string, string>();
           // string kk =  "{\"employees\": [{\"firstName\": \"Bill\",\"lastName\": \"Gates\"},{\"firstName\": \"George\",\"lastName\": \"Bush\"}]}";
            JsonReader reader = new JsonTextReader(new StringReader(kk));

            while (reader.Read())
            {
                Console.WriteLine(reader.TokenType + "\t\t" + reader.ValueType + "\t\t" + reader.Value);
            }


            HtmlDocument htmlDocument = new HtmlAgilityPack.HtmlDocument();
            htmlDocument.LoadHtml(strWebData);//加载HTML字符串，如果是文件可以用htmlDocument.Load方法加载  
            HtmlNode nodes = htmlDocument.DocumentNode;
            string CategoryListXPath = "//body/div[2]/div[1]/div[3]/table[1]";///div[0]/div[2]/table/tbody/tr/td[1]
            HtmlNodeCollection collection = nodes.SelectNodes(CategoryListXPath); ///"//body/div/div/div/table/tbody/tr/td/div"td/div/table/tbody
            HtmlNode temp = null;  
            foreach (HtmlNode node in collection)
            {
                string innertext = node.InnerHtml;
                temp = HtmlNode.CreateNode(node.InnerHtml);
                string CategoryNameXPath = "//table/tbody[1]";
                HtmlNodeCollection cc = temp.SelectNodes(CategoryNameXPath);
                foreach (HtmlNode n in cc)
                {

                }
            }
        }

        public static void SyncStockList()
        {
            StockDataSync.SyncStockList();
        }

        public static void cc()
        {
            string filePath = @"E:\同花顺软件\同花顺\history\sznse\day\002560.day";//同花顺目录下history目录是历史日线数据，我例子打开的是平安银行000001的
            byte[] stockFileBytes = System.IO.File.ReadAllBytes(filePath);
            int recordStartPos = readByteToInt(stockFileBytes, 10, 2);//记录开始位置
            int recordLength = readByteToInt(stockFileBytes, 12, 2);//记录长度
            int recordCount = readByteToInt(stockFileBytes, 14, 2);//文件中记录条数
            int fileBytesLength = stockFileBytes.Length;
            int pos = recordStartPos;
            List<StockDay> stockDayList = new List<StockDay>();//日线数据暂时读到List中
            do
            {
                StockDay sd = new StockDay();
                sd.DateInt = readByteToInt(stockFileBytes, pos, 4);//时间，整形表示的，可转为日期型
                sd.OpenPrice = readByteToInt(stockFileBytes, pos + 4, 2) * 0.001f;//开盘价
                sd.HighPrice = readByteToInt(stockFileBytes, pos + 8, 2) * 0.001f;//最高价
                sd.LowPrice = readByteToInt(stockFileBytes, pos + 12, 2) * 0.001f;//最低价
                sd.ClosePrice = readByteToInt(stockFileBytes, pos + 16, 2) * 0.001f;//收盘价
                sd.VolumeValue = readByteToInt(stockFileBytes, pos + 20, 4);//成交额
                sd.Volume = readByteToInt(stockFileBytes, pos + 24, 4);//成交量
                stockDayList.Add(sd);
                pos = pos + recordLength;
            } while (pos < fileBytesLength);
        }

        ///  读取某位置开始的byte转换为16进制字符串
        ///
        ///
        ///
        ///
        private static string readByteToHex(byte[] stockFileBytes, int startPos, int length)
        {
            string r = "";
            for (int i = startPos + length - 1; i >= startPos; i--)
            {
                r += stockFileBytes[i].ToString("X2");
            }
            return r;
        }
        ///
        ///  读取某位置开始的byte转换为16进制字符串
        ///
        ///
        ///
        ///
        private static int readByteToInt(byte[] stockFileBytes, int startPos, int length)
        {
            string r = readByteToHex(stockFileBytes, startPos, length);
            int v = Convert.ToInt32(r, 16);
            return v;
        }
    }

    public class StockDay
    {
        int dateInt;
        public int DateInt
        {
            get { return dateInt; }
            set { dateInt = value; }
        }

        DateTime date;//日期
        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }
        float openPrice;//开盘价
        public float OpenPrice
        {
            get { return openPrice; }
            set { openPrice = value; }
        }
        float closePrice;//收盘价
        public float ClosePrice
        {
            get { return closePrice; }
            set { closePrice = value; }
        }
        float highPrice;//最高价
        public float HighPrice
        {
            get { return highPrice; }
            set { highPrice = value; }
        }
        float lowPrice;//最低价
        public float LowPrice
        {
            get { return lowPrice; }
            set { lowPrice = value; }
        }
        float volume;//成交量
        public float Volume
        {
            get { return volume; }
            set { volume = value; }
        }
        float volumeValue;//成交额
        public float VolumeValue
        {
            get { return volumeValue; }
            set { volumeValue = value; }
        }
    }

    public class weekTest
    {
        public static string GetRecentDay()
        {
            return TransactionDate.GetRecentDay();
        }
    }

    public class CSVTest
    {
        public static void ReadAllRecords()
        {
            string str = HttpTest.testHttpGet();
            int m =  str.IndexOf("\r\n");
            string a = str.Remove(0, m);
            string b = Configuration.StockCSVHeader + a;
            TextReader rea = new StreamReader(/*"E:\\Users\\shenghai\\Desktop\\600023.csv"*/StringToStream(b), System.Text.Encoding.UTF8);
            using (var reader = new CsvReader(rea))
            {
                var records = reader.GetRecords<StockItem>();

                foreach (StockItem s in records)//var record
                {
                    int k = 0;
                    //Console.WriteLine( record );
                }
            }
            Console.WriteLine();

             
        }

        public static Stream GenerateStreamFromString(string s)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

        public static Stream StringToStream(string src)
        {
            byte[] byteArray = Encoding.UTF8.GetBytes(src);
            return new MemoryStream(byteArray);
        }
    }

    public class StockObject
    {
        public string Date { get; set; }
        public string StockCode { get; set; }
        public string StockName { get; set; }
        public string EndPrice { get; set; }
        public string HighPrice { get; set; }
        public string LowPrice { get; set; }
        public string StartPrice { get; set; }
        public string BeforeEndPrice { get; set; }
        public string UpPrice { get; set; }
        public string UpFuPrice { get; set; }
        public string Change { get; set; }
        public string Chengjiaoliang { get; set; }
        public string Chengjiaojin { get; set; }
        public string shizhi { get; set; }
        public string Zshizhi { get; set; }
    }


    public class StockListTmp
    {
        public string stockname { get; set; }
        public string stockcode { get; set; }

        public bool Equals(StockListTmp o) 
        {
            bool ret = false;
            if (stockname == o.stockname && stockcode == o.stockcode)
            {
                ret = true;
            }
            return ret;
        }
    }

    public class Data
    {
        public string 时间 { get; set; }
        public string 类型 { get; set; }
        public string 名称 { get; set; }
        public string 单位 { get; set; }
        public string 金额 { get; set; }
    } 
}
