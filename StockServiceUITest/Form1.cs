﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ServiceProcess;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
//using System.DirectoryServices.Protocols;
//using System.ServiceModel.Security;
using System.Net;
using System.IO;
using System.IO.Compression;
using System.Text.RegularExpressions;

using System.Data.SqlClient;
using System.Data.Sql;

using System.Data.Common;
using MySql.Data.MySqlClient;
using StockCommon;
using StockServiceUITest.TestCode;

namespace StockServiceUITest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //---------------------------------------------------------------------------
           /*MySql.Data.MySqlClient.MySqlConnection conn;
            MySql.Data.MySqlClient.MySqlCommand cmd;
            conn = new MySql.Data.MySqlClient.MySqlConnection();
            cmd = new MySql.Data.MySqlClient.MySqlCommand();
            conn.ConnectionString = "server=localhost;uid=shenghai;" + "pwd=123465;database=stock;";
            conn.Open();
            cmd.Connection = conn;
            DataSet dataSet1 = new DataSet();
            MySql.Data.MySqlClient.MySqlDataAdapter dataAdapter = new MySqlDataAdapter("SELECT * FROM stockcode;", conn);//读数据库  
            cmd.CommandText = "SELECT * FROM stockcode";
            dataAdapter.SelectCommand = cmd;
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds);
            dataAdapter.FillSchema(dataSet1, SchemaType.Source, "identify");
            dataAdapter.Fill(dataSet1, "IDENTIFY");//填充DataSet控件  
            dataGridView1.DataSource = dataSet1.Tables["identify"];//注意，DataSet中的数据表依次为Table, Table1, Table2...  
            dataGridView1.Update();*/
            //----------------------------------------------------------------------------------------------
            // All Test
            //---------------------------------------------------------------------------------------
            //LogManagerTest.testLogManager();
            //HttpTest.testHttpGet();
            //DbUtilityTest.testSql();
            //ServiceProcessTest.testServe();
            //-------------------------------------------------------------------------------------------
        }
    }

    public class stock
    {
        public stock(){}
        private string name;
        public string Name
        {
            get {return name;}
            set { name = value; }
        }

        private string code;
        public string Code
        {
            get { return code; }
            set { code = value; }
        }
    }
}
