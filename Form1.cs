using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OPCAutomation;
using System.Threading;
using System.Timers;
using System.Diagnostics;

namespace OPCDA
{
    /// <summary>
    /// 连接OPC的客户端
    /// </summary>
    public partial class Form1 : Form
    {
        OPCServer KepServer;
        OPCGroups KepGroups;
        OPCGroup KepGroup;
        OPCItems KepItems;
        OPCItem KepItem;
        OPCItem[] MyItem;
        OPCGroup KepGroup2;
        OPCItems KepItems2;
        OPCItem KepItem2;
        OPCItem[] MyItem2;
        OPCGroup KepGroup3;
        OPCItems KepItems3;
        OPCItem KepItem3;
        OPCItem[] MyItem3;
        OPCGroup KepGroup4;
        OPCItems KepItems4;
        OPCItem KepItem4;
        OPCItem[] MyItem4;
        int bbb=9999;
        int bbb1 =0;
        int aaa = 1;
        int aaa1 = 19;
        int high = 200;
        int high1 = 200;
        int ordernum = 0;
        //private NiThread t1,t2;
        int lie1 = 6;
        int lie11 = 2;
        int maduo = 1;
        Thread t,t2;
        /// <summary>
        /// 初始话窗口
        /// </summary>
        public Form1()
        {
            InitializeComponent();

        }
        /// <summary>
        /// 连接按钮的单机事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        bool tyreflag = true;
        int id;
        public static int tyrenumber1;
        //当数据改变时触发的事件
        public void KepGroup_DataChange(int TransactionID, int NumItems, ref Array ClientHandles, ref Array ItemValues, ref Array Qualities, ref Array TimeStamps)
        {
            if (MyItem[9].Value == false)
            {
                return;
            }
            listBox1.Items.Add("动均3入库");
            for (int i = 1; i <= NumItems; i++)
                listBox1.Items.Add(ItemValues.GetValue(i));//取到改变的值

            DataSet myds4 = new DataSet();
            string dstr4 = "select * from dongjunqiankuwei3 where status = '满' order by id asc;";
            myds4 = sql.GetDataSet(dstr4, "dongjunqiankuwei3");
            if (myds4.Tables[0].Rows.Count == 0)
            {
                notyre1 = true;
            }
            else
            {
                notyre1 = false;
            }
            if (MyItem2[9].Value == 1 && !notyre1)
            {
                return;
            }
            //Thread.Sleep(1000);
            MyItem2[2].Write(0);
            if (MyItem[0].Value == 3)
            {

                MyItem[3].Write(0);
                Thread.Sleep(1000);
                tyreflag = true;
            }
            if (/*aaa1 > fullnumber &&*/ aaa % fullnumber == 1 && !cd)
            {
                //DataSet myds1 = new DataSet();
                DataSet Myds = new DataSet();
                string b1 = "select * from dongjunqiankuwei3 where status = '空' order by id asc;";
                Myds = sql.GetDataSet(b1, "dongjunqiankuwei33");
                if (Myds.Tables[0].Rows.Count == 0)
                {
                    return;
                }
                id = int.Parse(Myds.Tables[0].Rows[0]["id"].ToString());
                hang = id / 100;
                lie1 = id % 100;
                high = higher1;
                tyrenumber1 = 1;
                string b2 = "update dongjunqiankuwei3 set status = '有'where id = " + id + " ;";
                sql.ExecuteSqlCommand(b2);


                cd = true;
                /* if (lie11 == 8)
                 {
                     aaa1 = 1;
                     lie11 = 4;
                     hang1 = hang1 + 1;
                     if (hang1 == 6)
                     {
                         hang1 = 4;
                     }
                     cd1 = false;
                     return;
                 }*/
            }

            if (MyItem[9].Value == true && MyItem[0].Value == 1 && MyItem2[1].Value == 0 && tyreflag)
            {
                MyItem[3].Write(0);
                Thread.Sleep(500);


                if (aaa % 4 == 1)
                {
                    high = higher1;
                }
                else if (aaa % 4 == 2)
                {
                    high = higher2;
                }
                else if (aaa % 4 == 3)
                {
                    high = higher3;
                }
                else if (aaa % 4 == 0)
                {
                    high = higher4;
                }
                int[] temp = new int[8]{ 0, MyItem[1].ServerHandle,MyItem[2].ServerHandle, MyItem[4].ServerHandle,
                MyItem[5].ServerHandle, MyItem[6].ServerHandle,
                MyItem[7].ServerHandle, MyItem[8].ServerHandle};
                Array serverHandles = temp;
                object[] valueTemp = new object[8] { "", 200, aaa, zikou, hang, lie1, high, 1 };
                Array values = (Array)valueTemp;
                Array Errors;
                Object cancel;
                // Object Qualities;
                int cancelID;
                KepGroup.AsyncWrite(7, ref serverHandles, ref values, out Errors, 1, out cancelID);//第一参数为item数量
                DataSet myds = new DataSet();
                string dstr = "update ruku3 set lie=" + lie1 + ",hang = " + hang + " where id = 1;";
                sql.ExecuteSqlCommand(dstr);

                if (tyrenumber1 >= fullnumber)
                {
                    string b3 = "update dongjunqiankuwei3 set number = " + fullnumber + ",status = '好' where id =" + id + " ;";
                    sql.ExecuteSqlCommand(b3);
                }
                else
                {
                    string b4 = "update dongjunqiankuwei3 set number = " + tyrenumber1 + "  where id = " + id + " ;";
                    sql.ExecuteSqlCommand(b4);
                }
                aaa = aaa + 1;
                string DStr = "update ruku3 set aaa = " + aaa + ";";
                sql.ExecuteSqlCommand(DStr);
                tyrenumber1 = aaa % fullnumber;
                if (tyrenumber1 == 0)
                {
                    tyrenumber1= fullnumber;
                }

                // high = high + 200;
                tyreflag = false;
                cd = false;
                //bbb = aaa;

            }

        }


           

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                String serIp = "localhost";//服务器的IP地址
                String serverName = "OPC.IwSCP";//OPC服务器名称
                KepServer = new OPCServer();
                //连接OPC服务器,opc服务名,ip
                    KepServer.Connect(serverName, serIp);
                //判断连接状态
                if (KepServer.ServerState == (int)OPCServerState.OPCRunning)
                {
                    textBox1.Text = "已连接到-" + KepServer.ServerName + "   ";
                }
                else
                {
                    //这里你可以根据返回的状态来自定义显示信息，请查看自动化接口API文档
                    textBox1.Text = "状态：" + KepServer.ServerState.ToString() + "   ";
                    return;
                }
                /*time.Elapsed += new System.Timers.ElapsedEventHandler(timer_Elapsed);
                time.Interval = 1000;
                time.Start();*/

                /*t1 = new NiThread(outtyre, null, "outtyre", 500);
                t1.Start();*/
                object ItemValues, Qualities, TimeStamps;
                KepGroups = KepServer.OPCGroups;
                KepGroup = KepGroups.Add("Group0");
                KepGroup.UpdateRate = 250;
                KepGroup.IsActive = true;
                KepGroup.IsSubscribed = true;
               // object ItemValues, Qualities, TimeStamps;
                //KepGroups = KepServer.OPCGroups;
                KepGroup2 = KepGroups.Add("Group2");
                KepGroup2.UpdateRate = 250;
                KepGroup2.IsActive = true;
                KepGroup2.IsSubscribed = true;
                KepGroup3 = KepGroups.Add("Group3");
                KepGroup3.UpdateRate = 250;
                KepGroup3.IsActive = true;
                KepGroup3.IsSubscribed = true;
                KepGroup4 = KepGroups.Add("Group4");
                KepGroup4.UpdateRate = 250;
                KepGroup4.IsActive = true;
                KepGroup4.IsSubscribed = true;
                KepItems2 = KepGroup2.OPCItems;
                //当KepGroup中数据发生改变的触发事件
                // KepGroup.DataChange += new DIOPCGroupEvent_DataChangeEventHandler(KepGroup_DataChange);
                KepItems = KepGroup.OPCItems;
                KepItems3 = KepGroup3.OPCItems;
                KepItems4 = KepGroup4.OPCItems;
                // t = new Thread(outtyre);
                //t.Start();
                /*int[] temp = new int[3];
                temp[0] = 0;
                KepItems.AddItem("123456:OPCAE", 1);
                KepItems.AddItem("123456:lishile", 2);*/
                //OPCItem bItem = KepItems.Item(2);
                try{
                    MyItem = new OPCItem[10];
                    MyItem[0] = KepItems.AddItem("!UI4,PCR_LM2,Plc.PVL,Application.USERVARGLOBAL.JXSStatus", 1);
                    MyItem[1] = KepItems.AddItem("!UI4,PCR_LM2,Plc.PVL,Application.USERVARGLOBAL.r_cphigh", 2);
                    MyItem[2] = KepItems.AddItem("!UI4,PCR_LM2,Plc.PVL,Application.USERVARGLOBAL.r_cpnoid", 3);
                    MyItem[3] = KepItems.AddItem("!UI4,PCR_LM2,Plc.PVL,Application.USERVARGLOBAL.r_cpnoid_back", 4);
                    MyItem[4] = KepItems.AddItem("!UI4,PCR_LM2,Plc.PVL,Application.USERVARGLOBAL.r_cpzik", 5);
                    MyItem[5] = KepItems.AddItem("!UI4,PCR_LM2,Plc.PVL,Application.USERVARGLOBAL.r_hang", 6);
                    MyItem[6] = KepItems.AddItem("!UI4,PCR_LM2,Plc.PVL,Application.USERVARGLOBAL.r_lie", 7);
                    MyItem[7] = KepItems.AddItem("!UI4,PCR_LM2,Plc.PVL,Application.USERVARGLOBAL.r_high", 8);
                    MyItem[8] = KepItems.AddItem("!UI4,PCR_LM2,Plc.PVL,Application.USERVARGLOBAL.r_typeid", 9);
                    MyItem[9] = KepItems.AddItem("!BOOL,PCR_LM2,Plc.PVL,Application.USERVARGLOBAL.ruku_in", 10);
                    MyItem2 = new OPCItem[11];
                    MyItem2[0] = KepItems2.AddItem("!UI4,PCR_LM2,Plc.PVL,Application.USERVARGLOBAL.c_cphigh", 1);
                    MyItem2[1] = KepItems2.AddItem("!UI4,PCR_LM2,Plc.PVL,Application.USERVARGLOBAL.c_orderid", 2);
                    MyItem2[2] = KepItems2.AddItem("!UI4,PCR_LM2,Plc.PVL,Application.USERVARGLOBAL.c_orderid_back", 3);
                    MyItem2[3] = KepItems2.AddItem("!UI4,PCR_LM2,Plc.PVL,Application.USERVARGLOBAL.c_cpzik", 4);
                    MyItem2[4] = KepItems2.AddItem("!UI4,PCR_LM2,Plc.PVL,Application.USERVARGLOBAL.c_hang", 5);
                    MyItem2[5] = KepItems2.AddItem("!UI4,PCR_LM2,Plc.PVL,Application.USERVARGLOBAL.c_lie", 6);
                    MyItem2[6] = KepItems2.AddItem("!UI4,PCR_LM2,Plc.PVL,Application.USERVARGLOBAL.c_high", 7);
                    MyItem2[7] = KepItems2.AddItem("!UI4,PCR_LM2,Plc.PVL,Application.USERVARGLOBAL.c_cpnum", 8);
                    MyItem2[8] = KepItems2.AddItem("!UI4,PCR_LM2,Plc.PVL,Application.USERVARGLOBAL.c_typeid", 9);
                    MyItem2[9] = KepItems2.AddItem("!I2,PCR_LM2,Plc.PVL,Application.USERVARGLOBAL.ischuk_lk", 10);
                    MyItem2[10] = KepItems2.AddItem("!UI4,PCR_LM2,Plc.PVL,Application.USERVARGLOBAL.JXSStatus", 11);
                    MyItem3 = new OPCItem[10];
                    MyItem3[0] = KepItems3.AddItem("!UI4,PCR_LM4,Plc.PVL,Application.USERVARGLOBAL.JXSStatus", 1);
                    MyItem3[1] = KepItems3.AddItem("!UI4,PCR_LM4,Plc.PVL,Application.USERVARGLOBAL.r_cphigh", 2);
                    MyItem3[2] = KepItems3.AddItem("!UI4,PCR_LM4,Plc.PVL,Application.USERVARGLOBAL.r_cpnoid", 3);
                    MyItem3[3] = KepItems3.AddItem("!UI4,PCR_LM4,Plc.PVL,Application.USERVARGLOBAL.r_cpnoid_back", 4);
                    MyItem3[4] = KepItems3.AddItem("!UI4,PCR_LM4,Plc.PVL,Application.USERVARGLOBAL.r_cpzik", 5);
                    MyItem3[5] = KepItems3.AddItem("!UI4,PCR_LM4,Plc.PVL,Application.USERVARGLOBAL.r_hang", 6);
                    MyItem3[6] = KepItems3.AddItem("!UI4,PCR_LM4,Plc.PVL,Application.USERVARGLOBAL.r_lie", 7);
                    MyItem3[7] = KepItems3.AddItem("!UI4,PCR_LM4,Plc.PVL,Application.USERVARGLOBAL.r_high", 8);
                    MyItem3[8] = KepItems3.AddItem("!UI4,PCR_LM4,Plc.PVL,Application.USERVARGLOBAL.r_typeid", 9);
                    MyItem3[9] = KepItems3.AddItem("!BOOL,PCR_LM4,Plc.PVL,Application.USERVARGLOBAL.ruku_in", 10);
                    MyItem4 = new OPCItem[11];
                    MyItem4[0] = KepItems4.AddItem("!UI4,PCR_LM4,Plc.PVL,Application.USERVARGLOBAL.c_cphigh", 1);
                    MyItem4[1] = KepItems4.AddItem("!UI4,PCR_LM4,Plc.PVL,Application.USERVARGLOBAL.c_orderid", 2);
                    MyItem4[2] = KepItems4.AddItem("!UI4,PCR_LM4,Plc.PVL,Application.USERVARGLOBAL.c_orderid_back", 3);
                    MyItem4[3] = KepItems4.AddItem("!UI4,PCR_LM4,Plc.PVL,Application.USERVARGLOBAL.c_cpzik", 4);
                    MyItem4[4] = KepItems4.AddItem("!UI4,PCR_LM4,Plc.PVL,Application.USERVARGLOBAL.c_hang", 5);
                    MyItem4[5] = KepItems4.AddItem("!UI4,PCR_LM4,Plc.PVL,Application.USERVARGLOBAL.c_lie", 6);
                    MyItem4[6] = KepItems4.AddItem("!UI4,PCR_LM4,Plc.PVL,Application.USERVARGLOBAL.c_high", 7);
                    MyItem4[7] = KepItems4.AddItem("!UI4,PCR_LM4,Plc.PVL,Application.USERVARGLOBAL.c_cpnum", 8);
                    MyItem4[8] = KepItems4.AddItem("!UI4,PCR_LM4,Plc.PVL,Application.USERVARGLOBAL.c_typeid", 9);
                    MyItem4[9] = KepItems4.AddItem("!I2,PCR_LM4,Plc.PVL,Application.USERVARGLOBAL.ischuk_lk", 10);
                    MyItem4[10] = KepItems4.AddItem("!UI4,PCR_LM4,Plc.PVL,Application.USERVARGLOBAL.JXSStatus", 11);
                }
                catch (Exception ex)
                {
                    Trace.WriteLine(ex);
                }
               /* MyItem[2].Read(1, out ItemValues, out Qualities, out TimeStamps);
                aaa = int.Parse(ItemValues.ToString());
                MyItem[3].Read(1, out ItemValues, out Qualities, out TimeStamps);
                bbb = int.Parse(ItemValues.ToString());
                if (aaa == 0)
                {
                    aaa = bbb;
                }
                else
                {
                    bbb = aaa;
                }
                if (aaa == 0)
                {
                    aaa = 1;
                }*/
               // lie1 = 5 + (aaa - 1) / 4;
                //maduo = (aaa - 1) / 4;
                DataSet myds2 = new DataSet();
                string dstr2 = "select * from luntaiweihu where flag = 1";
                myds2 = sql.GetDataSet(dstr2, "luntaiweihu");
                zikou = int.Parse(myds2.Tables[0].Rows[0]["zikou"].ToString());
                higher1 = int.Parse(myds2.Tables[0].Rows[0]["high1"].ToString());
                higher2 = int.Parse(myds2.Tables[0].Rows[0]["high2"].ToString());
                higher3 = int.Parse(myds2.Tables[0].Rows[0]["high3"].ToString());
                higher4 = int.Parse(myds2.Tables[0].Rows[0]["high4"].ToString());
                zhuangxiangnumber = int.Parse(myds2.Tables[0].Rows[0]["zhuangxiangnumber"].ToString());
                fullnumber = int.Parse(myds2.Tables[0].Rows[0]["number"].ToString());
                DataSet myds = new DataSet();
                string dstr = "select * from ruku";
                myds = sql.GetDataSet(dstr, "ruku");
                hang1 = int.Parse(myds.Tables[0].Rows[0]["hang"].ToString());
                lie11 = int.Parse(myds.Tables[0].Rows[0]["lie"].ToString());
                aaa1 = int.Parse(myds.Tables[0].Rows[0]["aaa"].ToString());
                tyrenumber = aaa1 % fullnumber;
                if (tyrenumber == 0)
                {
                    tyrenumber = fullnumber;
                }
                DataSet myds1 = new DataSet();
                string dstr1 = "select * from ruku3";
                myds1 = sql.GetDataSet(dstr1, "ruku3");
                hang = int.Parse(myds1.Tables[0].Rows[0]["hang"].ToString());
                lie1 = int.Parse(myds1.Tables[0].Rows[0]["lie"].ToString());
                aaa = int.Parse(myds1.Tables[0].Rows[0]["aaa"].ToString());
                tyrenumber1 = aaa % fullnumber;
                if (tyrenumber1 == 0)
                {
                    tyrenumber1 = fullnumber;
                }
                KepGroup.DataChange += new DIOPCGroupEvent_DataChangeEventHandler(KepGroup_DataChange);
                KepGroup2.DataChange += new DIOPCGroupEvent_DataChangeEventHandler(KepGroup_DataChange2);
                KepGroup3.DataChange += new DIOPCGroupEvent_DataChangeEventHandler(KepGroup_DataChange3);
                KepGroup4.DataChange += new DIOPCGroupEvent_DataChangeEventHandler(KepGroup_DataChange4);
            }
            catch (Exception ex)
            {
                textBox1.Text = ex.Message;
            }
        }
        bool tyreflag1 = true;
        int hang;
        int hang1;
        int c_lie1;
        int c_hang1;
        bool CD = false;
        bool CD1 = false;
        private void KepGroup_DataChange4(int TransactionID, int NumItems, ref Array ClientHandles, ref Array ItemValues, ref Array Qualities, ref Array TimeStamps)
        {
            listBox1.Items.Add("动均2出库");
            for (int i = 1; i <= NumItems; i++)
                listBox1.Items.Add(ItemValues.GetValue(i));//取到改变的值
            if (MyItem4[9].Value == 2)
            {
                return;
            }
            DataSet myds11 = new DataSet();
            string dstr11 = "select * from dongjunqiankuwei2 where status = '满' order by id asc;";
            myds11 = sql.GetDataSet(dstr11, "dongjunqiankuwei2");
            if (myds11.Tables[0].Rows.Count == 0)
            {
                return;
            }
            MyItem3[3].Write(0);
            int ischuk_lk = MyItem4[9].Value;
            Thread.Sleep(500);
            int JXSStatus = int.Parse(MyItem4[10].Value.ToString());
            if (JXSStatus == 3)
            {
                MyItem4[2].Write(0);
                CD1 = false;
            }
            if (ischuk_lk == 1 && JXSStatus == 1 && !CD1 && MyItem3[2].Value == 0)
            {
                DataSet myds3 = new DataSet();
                string dstr3 = "select * from dongjunqiankuwei2 where status = '满' order by id asc;";
                myds3 = sql.GetDataSet(dstr3, "chuku");
                if (myds3.Tables[0].Rows.Count == 0)
                {
                    return;
                }
                int id = int.Parse(myds3.Tables[0].Rows[0]["id"].ToString());
                int cpmun = int.Parse(myds3.Tables[0].Rows[0]["number"].ToString());
                c_hang1 = id / 100;
                c_lie1 = id % 100;
                //c_lie1 = int.Parse(myds3.Tables[0].Rows[0]["lie"].ToString());
                //c_hang1 = int.Parse(myds3.Tables[0].Rows[0]["hang"].ToString());
                //ordernum = ordernum + 1;
                // c_lie = c_lie + 1;
                int[] temp11 = new int[9]{ 0,MyItem4[0].ServerHandle, MyItem4[1].ServerHandle,
                MyItem4[3].ServerHandle, MyItem4[4].ServerHandle,
                MyItem4[5].ServerHandle, MyItem4[6].ServerHandle,
                MyItem4[7].ServerHandle, MyItem4[8].ServerHandle};
                Array serverHandles = temp11;
                object[] valueTemp = new object[9] { "", 200, id, zikou, c_hang1, c_lie1, 200, cpmun, 2 };
                Array values = (Array)valueTemp;
                Array Errors;
                Object cancel;
                // Object Qualities;
                int cancelID;
                KepGroup4.AsyncWrite(8, ref serverHandles, ref values, out Errors, 1, out cancelID);//第一参数为item数量
                string dstr4 = "update dongjunqiankuwei2 set status = '空', number = 0 where id = " + id + ";";
                sql.ExecuteSqlCommand(dstr4);
                /*if(ordernum==4)
                {
                    ordernum = 0;
                    c_lie = 2;

                }*/
                CD1 = true;
            }

        }
        bool flag = true;
        bool notyre = false;
        int id1;
        public  static int tyrenumber;
        private void KepGroup_DataChange3(int TransactionID, int NumItems, ref Array ClientHandles, ref Array ItemValues, ref Array Qualities, ref Array TimeStamps)
        {
            if(MyItem3[9].Value == false)
            {
                return;
            }

            listBox1.Items.Add("动均2入库"); 
            for (int i = 1; i <= NumItems; i++)
                listBox1.Items.Add(ItemValues.GetValue(i));//取到改变的值

            DataSet myds4 = new DataSet();
            string dstr4 = "select * from dongjunqiankuwei2 where status = '满' order by id asc;";
            myds4 = sql.GetDataSet(dstr4, "dongjunqiankuwei2");
            if (myds4.Tables[0].Rows.Count == 0)
            {
                notyre = true;
            }
            else
            {
                notyre = false;
            }
            if (MyItem4[9].Value == 1&&!notyre)
            {
                return;
            }
            //Thread.Sleep(1000);
            MyItem4[2].Write(0);
            if (MyItem3[0].Value == 3)
            {

                MyItem3[3].Write(0);
                Thread.Sleep(1000);
                tyreflag1 = true;
            }
            if (/*aaa1 > fullnumber &&*/ aaa1 % fullnumber == 1 && !cd1)
            {
                //DataSet myds1 = new DataSet();
                DataSet Myds = new DataSet();
                string b1 = "select * from dongjunqiankuwei2 where status = '空' order by id asc;";
                Myds = sql.GetDataSet(b1, "dongjunqiankuwei2");
                if (Myds.Tables[0].Rows.Count == 0)
                {
                    return;
                }
                id1 = int.Parse(Myds.Tables[0].Rows[0]["id"].ToString());
                hang1 = id1 / 100;
                lie11 = id1%100;
                high1 = higher1;
                tyrenumber = 1;
                string b2 = "update dongjunqiankuwei2 set status = '有'where id = " + id1 + " ;";
                sql.ExecuteSqlCommand(b2);


                cd1 = true;
                /* if (lie11 == 8)
                 {
                     aaa1 = 1;
                     lie11 = 4;
                     hang1 = hang1 + 1;
                     if (hang1 == 6)
                     {
                         hang1 = 4;
                     }
                     cd1 = false;
                     return;
                 }*/
            }

            if (MyItem3[9].Value == true && MyItem3[0].Value == 1 && MyItem4[1].Value ==0&&tyreflag1)
            {
                MyItem3[3].Write(0);
                Thread.Sleep(500);


                if (aaa1 % 4 == 1)
                {
                    high1 = higher1;
                }
                else if (aaa1 % 4 == 2)
                {
                    high1 = higher2;
                }
                else if (aaa1 % 4 == 3)
                {
                    high1 = higher3;
                }
                else if (aaa1 % 4 == 0)
                {
                    high1 = higher4;
                }
                int[] temp = new int[8]{ 0, MyItem3[1].ServerHandle,MyItem3[2].ServerHandle, MyItem3[4].ServerHandle,
                MyItem3[5].ServerHandle, MyItem3[6].ServerHandle,
                MyItem3[7].ServerHandle, MyItem3[8].ServerHandle};
                Array serverHandles = temp;
                object[] valueTemp = new object[8] { "", 200, aaa1, zikou, hang1, lie11, high1, 1 };
                Array values = (Array)valueTemp;
                Array Errors;
                Object cancel;
                // Object Qualities;
                int cancelID;
                KepGroup3.AsyncWrite(7, ref serverHandles, ref values, out Errors, 1, out cancelID);//第一参数为item数量
                DataSet myds = new DataSet();
                string dstr = "update ruku set lie=" + lie11 + ",hang = " + hang1 + " where id = 1;";
                sql.ExecuteSqlCommand(dstr);
               
                if (tyrenumber >= fullnumber)
                {
                    string b3 = "update dongjunqiankuwei2 set number = "+fullnumber+",status = '好' where id =" + id1 + " ;";
                    sql.ExecuteSqlCommand(b3);
                }
                else
                {
                    string b4 = "update dongjunqiankuwei2 set number = " + tyrenumber + "  where id = " + id1 + " ;";
                    sql.ExecuteSqlCommand(b4);
                }
                aaa1 = aaa1 + 1;
                string DStr = "update ruku set aaa = " + aaa1 + ";";
                sql.ExecuteSqlCommand(DStr);
                tyrenumber = aaa1 % fullnumber;
                if (tyrenumber == 0)
                {
                    tyrenumber = fullnumber;
                }

                // high = high + 200;
                tyreflag1 = false;
                cd1 = false;
                //bbb = aaa;

            }
            }

            /* private void timer_Elapsed(object sender, ElapsedEventArgs e)
             {
                 throw new NotImplementedException();
             }*/

            private void outtyre()
        {
            object ItemValues, Qualities, TimeStamps;
            //KepGroups = KepServer.OPCGroups;
            /*KepGroup2 = KepGroups.Add("Group2");
            KepGroup2.UpdateRate = 250;
            KepGroup2.IsActive = true;
            KepGroup2.IsSubscribed = true;
            KepItems2 = KepGroup2.OPCItems;*/
            MyItem2 = new OPCItem[12];
            MyItem2[0] = KepItems2.AddItem("!I2,PCR_2LM_1,Plc.PVL,Application.USERVARGLOBAL.c_cphigh", 1);
            MyItem2[1] = KepItems2.AddItem("!I2,PCR_2LM_1,Plc.PVL,Application.USERVARGLOBAL.c_orderid", 2);
            MyItem2[2] = KepItems2.AddItem("!I2,PCR_2LM_1,Plc.PVL,Application.USERVARGLOBAL.c_orderid_back", 3);
            MyItem2[3] = KepItems2.AddItem("!I2,PCR_2LM_1,Plc.PVL,Application.USERVARGLOBAL.c_cpzik", 4);
            MyItem2[4] = KepItems2.AddItem("!I2,PCR_2LM_1,Plc.PVL,Application.USERVARGLOBAL.c_hang", 5);
            MyItem2[5] = KepItems2.AddItem("!I2,PCR_2LM_1,Plc.PVL,Application.USERVARGLOBAL.c_lie", 6);
            MyItem2[6] = KepItems2.AddItem("!I2,PCR_2LM_1,Plc.PVL,Application.USERVARGLOBAL.c_high", 7);
            MyItem2[7] = KepItems2.AddItem("!I2,PCR_2LM_1,Plc.PVL,Application.USERVARGLOBAL.c_cpnum", 8);
            MyItem2[8] = KepItems2.AddItem("!I2,PCR_2LM_1,Plc.PVL,Application.USERVARGLOBAL.c_typeid", 9);
            MyItem2[9] = KepItems2.AddItem("!I2,PCR_2LM_1,Plc.PVL,Application.USERVARGLOBAL.ischuk_lk", 10);
            MyItem2[10] = KepItems2.AddItem("!I2,PCR_2LM_1,Plc.PVL,Application.USERVARGLOBAL.JXSStatus", 11);
            MyItem2[11] = KepItems2.AddItem("!I2,PCR_2LM_1,Plc.PVL,Application.USERVARGLOBAL.c_ordernum", 12);
            KepGroup2.DataChange += new DIOPCGroupEvent_DataChangeEventHandler(KepGroup_DataChange2);
            /* int ischuk_lk = MyItem2[9].Value;
           int JXSStatus = MyItem2[10].Value;
           if (ischuk_lk == 1 && JXSStatus == 1)
           {
               KepGroup2.DataChange += new DIOPCGroupEvent_DataChangeEventHandler(KepGroup_DataChange2);
               int[] temp = new int[9]{ 0, MyItem2[1].ServerHandle,
               MyItem2[2].ServerHandle,MyItem2[3].ServerHandle, MyItem2[4].ServerHandle,
               MyItem2[5].ServerHandle, MyItem2[6].ServerHandle,
               MyItem2[7].ServerHandle, MyItem2[8].ServerHandle};
               Array serverHandles = temp;
               object[] valueTemp = new object[9] { "", 200, 1, 0, 540, 4, 6, 1, 2 };
               Array values = (Array)valueTemp;
               Array Errors;
               Object cancel;
               // Object Qualities;
               int cancelID;
               KepGroup.AsyncWrite(8, ref serverHandles, ref values, out Errors, 1, out cancelID);//第一参数为item数量
           }*/

            /*int[] temp = new int[] {0,MyItem2[9].ServerHandle,MyItem2[10].ServerHandle };
            Array serverHandles = (Array)temp;
            Array Errors;
            int cancelID;
            KepGroup2.AsyncRead(2, ref serverHandles, out Errors, 1, out cancelID);*/

        }
        bool cd = false;
        bool cd1 = false;
        public static int zikou,c_lie,c_hang;
        public static int higher1, higher2, higher3, higher4, higher5;

        private void xiugai_Click(object sender, EventArgs e)
        {
            if(luntaiguige.Text == "")
            {
                return;
            }
            DialogResult dr = MessageBox.Show("要修改轮胎规格吗？", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dr == DialogResult.Yes)
            {
                string a1 = "update rukuxinxi set guige = '" + luntaiguige.Text + "'where id = 1;";
                sql.ExecuteSqlCommand(a1);
                DataSet MYDS = new DataSet();
                string a2 = "select * from luntaiweihu where guige = '" + luntaiguige.Text + "';";
                MYDS = sql.GetDataSet(a2, "luntaiweihu");
                zikou = int.Parse(MYDS.Tables[0].Rows[0]["zikou"].ToString());
                higher1 = int.Parse(MYDS.Tables[0].Rows[0]["high1"].ToString());
                higher2 = int.Parse(MYDS.Tables[0].Rows[0]["high2"].ToString());
                higher3 = int.Parse(MYDS.Tables[0].Rows[0]["high3"].ToString());
                higher4 = int.Parse(MYDS.Tables[0].Rows[0]["high4"].ToString());
                zhuangxiangnumber = int.Parse(MYDS.Tables[0].Rows[0]["zhuangxiangnumber"].ToString());
                fullnumber = int.Parse(MYDS.Tables[0].Rows[0]["number"].ToString());
                string a3 = "update luntaiweihu set flag = 0 where id = 0";
                sql.ExecuteSqlCommand(a3);
                string a4 = "update luntaiweihu set flag = 1 where guige = '" + luntaiguige.Text + "';";
                sql.ExecuteSqlCommand(a4);
                aaa = 1;
                luntaiguige.Text = "";
            }

        }

        private void weihu_Click(object sender, EventArgs e)
        {
            tyremaintenance Ftyremaintenance = new tyremaintenance();
            Ftyremaintenance.ShowDialog();
        }

        private void djhchushihua_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("要初始化动均后数据吗？", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dr == DialogResult.Yes)
            {
                string a = "update dongjunhoukuwei set number = 0,status = '空' where id != 0";
                sql.ExecuteSqlCommand(a);
                string b = "update dongjunhouruku set hang = 3,lie =4,aaa = 1 where id =1";
                sql.ExecuteSqlCommand(b);
                hang = 3; lie1 = 4; aaa = 1; ordernum = 0;
            }
        }

        private void djqchushihua_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("要初始化动均前数据吗？", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dr == DialogResult.Yes)
            {
                string a = "update dongjunqiankuwei2 set number = 0,status = '空' where id != 0";
                sql.ExecuteSqlCommand(a);
                string b = "update ruku set hang = 3,lie =4,aaa = 1 where id =1";
                sql.ExecuteSqlCommand(b);
                hang1 = 3; lie11 = 4; aaa1 = 1;
            }
        }

        public static int maxduowei;
        public static int fullnumber;
        public static int zhuangxiangnumber;
        private bool notyre1;

        private void luntaiguige_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void KepGroup_DataChange2(int TransactionID, int NumItems, ref Array ClientHandles, ref Array ItemValues, ref Array Qualities, ref Array TimeStamps)
        {
            listBox1.Items.Add("动均3出库");
            for (int i = 1; i <= NumItems; i++)
                listBox1.Items.Add(ItemValues.GetValue(i));//取到改变的值
            if (MyItem2[9].Value == 2)
            {
                return;
            }
            DataSet myds11 = new DataSet();
            string dstr11 = "select * from dongjunqiankuwei3 where status = '满' order by id asc;";
            myds11 = sql.GetDataSet(dstr11, "dongjunqiankuwei333");
            if (myds11.Tables[0].Rows.Count == 0)
            {
                return;
            }
            MyItem[3].Write(0);
            int ischuk_lk = MyItem2[9].Value;
            Thread.Sleep(500);
            int JXSStatus = int.Parse(MyItem2[10].Value.ToString());
            if (JXSStatus == 3)
            {
                MyItem2[2].Write(0);
                CD = false;
            }
            if (ischuk_lk == 1 && JXSStatus == 1 && !CD && MyItem[2].Value == 0)
            {
                DataSet myds3 = new DataSet();
                string dstr3 = "select * from dongjunqiankuwei3 where status = '满' order by id asc;";
                myds3 = sql.GetDataSet(dstr3, "chuku");
                if (myds3.Tables[0].Rows.Count == 0)
                {
                    return;
                }
                int id = int.Parse(myds3.Tables[0].Rows[0]["id"].ToString());
                int cpmun = int.Parse(myds3.Tables[0].Rows[0]["number"].ToString());
                c_hang = id / 100;
                c_lie = id % 100;
                //c_lie1 = int.Parse(myds3.Tables[0].Rows[0]["lie"].ToString());
                //c_hang1 = int.Parse(myds3.Tables[0].Rows[0]["hang"].ToString());
                //ordernum = ordernum + 1;
                // c_lie = c_lie + 1;
                int[] temp11 = new int[9]{ 0,MyItem2[0].ServerHandle, MyItem2[1].ServerHandle,
                MyItem2[3].ServerHandle, MyItem2[4].ServerHandle,
                MyItem2[5].ServerHandle, MyItem2[6].ServerHandle,
                MyItem2[7].ServerHandle, MyItem2[8].ServerHandle};
                Array serverHandles = temp11;
                object[] valueTemp = new object[9] { "", 200, id, zikou, c_hang, c_lie, 200, cpmun, 2 };
                Array values = (Array)valueTemp;
                Array Errors;
                Object cancel;
                // Object Qualities;
                int cancelID;
                KepGroup2.AsyncWrite(8, ref serverHandles, ref values, out Errors, 1, out cancelID);//第一参数为item数量
                string dstr4 = "update dongjunqiankuwei3 set status = '空', number = 0 where id = " + id + ";";
                sql.ExecuteSqlCommand(dstr4);
                /*if(ordernum==4)
                {
                    ordernum = 0;
                    c_lie = 2;

                }*/
                CD = true;
            }

        }
    }
}

