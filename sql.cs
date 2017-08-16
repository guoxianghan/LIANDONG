using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;


namespace OPCDA
{
    class sql
    {
        //public static string M_str_sqlcon = "server=(local);database=aftersale;uid=sa;pwd=zaq!@#456";
        //public static string M_str_sqlcon = "server=(local);database=db_shengtong;uid=sa;pwd=zaq!@#456";
        //public static string M_str_sqlcon = "server=192.168.120.40;database=db_shengtong;uid=sa;pwd=zaq!@#456";
        //public static string M_str_sqlcon = "server=192.168.8.223;database=db_shengtong;uid=sa;pwd=!@#123qwe";  // Being used in Shengtong.
        //public static string M_str_sqlcon = "Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=db_shengtong;Data Source=192.168.8.223";
        //public static string M_str_sqlcon = "server=(local);database=sql_shengtong;integrated security=SSPI";
        private static object lockObj = new object();
        private static SqlConnection sqlConnection;

        #region  建立数据库连接
        /// <summa ry>
        /// 建立数据库连接.
        /// </summary>
        /// <returns>返回SqlConnection对象</returns>
        public static SqlConnection GetSqlConnection()
        {
            String server_string = "server = (local); database = doublestar; integrated security = SSPI";
            sqlConnection = new SqlConnection(server_string);   //用SqlConnection对象与指定的数据库相连接
            sqlConnection.Open();  //打开数据库连接

            return sqlConnection;  //返回SqlConnection对象的信息
        }
        #endregion

        #region  关闭数据库连接
        /// <summary>
        /// 关闭于数据库的连接.
        /// </summary>
        public static void CloseSqlConnection()
        {
            if (sqlConnection.State == ConnectionState.Open)  // 判断是否打开与数据库的连接
            {
                sqlConnection.Close();  // 关闭数据库的连接
                sqlConnection.Dispose();  // 释放My_con变量的所有空间
            }
        }
        #endregion

        #region  读取指定表中的信息
        /// <summary>
        /// 读取指定表中的信息.
        /// </summary>
        /// <param name="SQLstr">SQL语句</param>
        /// <returns>返回bool型</returns>
        public static SqlDataReader GetSqlDataReader(string SQLstr)
        {
            GetSqlConnection();   //打开与数据库的连接
            SqlCommand My_com = sqlConnection.CreateCommand(); //创建一个SqlCommand对象，用于执行SQL语句
            My_com.CommandText = SQLstr;    //获取指定的SQL语句
            SqlDataReader My_read = My_com.ExecuteReader(); //执行SQL语名句，生成一个SqlDataReader对象
            return My_read;
        }
        #endregion

        #region 执行SqlCommand命令
        /// <summary>
        /// 执行SqlCommand
        /// </summary>
        /// <param name="M_str_sqlstr">SQL语句</param>hj
        public static void ExecuteSqlCommand(string sqlCommand)
        {
            lock (lockObj)
            {
                for (int i = 0; i < 3; i++)
                {
                    try
                    {
                        GetSqlConnection();   //打开与数据库的连接
                        SqlCommand SQLcom = new SqlCommand(sqlCommand, sqlConnection); //创建一个SqlCommand对象，用于执行SQL语句
                        int result = SQLcom.ExecuteNonQuery();   //执行SQL语句
                        SQLcom.Dispose();   //释放所有空间
                        CloseSqlConnection();    //调用con_close()方法，关闭与数据库的连接
                        break;
                    }
                    catch (Exception ex)
                    {
                        CloseSqlConnection();
                        ex.ToString();
                    }
                }
            }

        }
        #endregion

        #region  创建DataSet对象
        /// <summary>
        /// 创建一个DataSet对象
        /// </summary>
        /// <param name="M_str_sqlstr">SQL语句</param>
        /// <param name="M_str_table">表名</param>
        /// <returns>返回DataSet对象</returns>
        public static DataSet GetDataSet(string SQLstr, string tableName)
        {
            lock (lockObj)
            {
                DataSet My_DataSet = new DataSet(); //创建DataSet对象
                for (int i = 0; i < 3; i++)
                {
                    try
                    {
                        GetSqlConnection();   //打开与数据库的连接
                        SqlDataAdapter SQLda = new SqlDataAdapter(SQLstr, sqlConnection);  //创建一个SqlDataAdapter对象，并获取指定数据表的信息

                        SQLda.Fill(My_DataSet, tableName);  //通过SqlDataAdapter对象的Fill()方法，将数据表信息添加到DataSet对象中
                        CloseSqlConnection();    //关闭数据库的连接
                        return My_DataSet;  //返回DataSet对象的信息
                    }
                    catch (Exception ex)
                    {
                        CloseSqlConnection();
                        ex.ToString();
                        return null;
                    }
                }
                return My_DataSet;
            }
        }
        #endregion

        /*#region 将工字轮信息保存到数据库中
        /// <summary>
        /// 将刚刚出库的工字轮部分属性写入到数据库中
        /// </summary>
        /// <param name="myHwheel"></param>
        public static void InsertHWheelToDb(Data.HWheel myHwheel)
        {
            string myStr;
            try
            {
                myStr = "insert into hwheel (qrcode,hwheelmodels,steelcord,outtime,hwheelstatus,weldcount,lr,machcode,sampleyn," +
                    "EndWrkDate,structurecode,prodcode,length,enduser,sampleno,inshixiaokutime) values ('"
                   + myHwheel.QRcode + "','" + myHwheel.models + "','" + myHwheel.steelcord + "','" + DateTime.Now.ToString() + "'," +
                   myHwheel.hwheelstatus.ToString() + ",'" + myHwheel.weldcount + "','" + myHwheel.lr + "','" + myHwheel.machcode +
                   "','" + myHwheel.sampleYN + "','" + myHwheel.EndWrkdate + "','" + myHwheel.structurecode + "','" + myHwheel.ProdCode +
                   "'," + myHwheel.length + ",'" + myHwheel.EndUser + "','" + myHwheel.SampleNo + "','" + myHwheel.inshixiaokutime + "')";

                ExecuteSqlCommand(myStr);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }
        #endregion*/
    }
}
