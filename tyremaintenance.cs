using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OPCDA
{
    public partial class tyremaintenance : Form
    {
        public tyremaintenance()
        {
            InitializeComponent();
        }
        int myid;
        private void tyremaintenance_Load(object sender, EventArgs e)
        {
            string dStr = "select * from luntaiweihu;";
            DataSet myds = sql.GetDataSet(dStr, "tyremaintenance");
            this.dataGridView1.DataSource = myds.Tables[0];
            dataGridView1.Columns[0].HeaderText = "序号";
            dataGridView1.Columns[1].HeaderText = "物料编码";
            dataGridView1.Columns[2].HeaderText = "子口";
            dataGridView1.Columns[3].HeaderText = "轮胎高度1";
            dataGridView1.Columns[4].HeaderText = "轮胎高度2"; ;
            dataGridView1.Columns[5].HeaderText = "轮胎高度3";
            dataGridView1.Columns[6].HeaderText = "轮胎高度4";
            dataGridView1.Columns[7].HeaderText = "每垛数量";
            dataGridView1.Columns[8].HeaderText = "最大垛数";
            button4.Enabled = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            #region 判断输入是否合法
            if (luntaiguige.Text.Trim() == "")
            {
                MessageBox.Show("输入不能为空！");
                luntaiguige.Focus();
                return;
            }
            if (zikou.Text.Trim() == "")
            {
                MessageBox.Show("输入不能为空！");
                zikou.Focus();
                return;
            }
            if (high1.Text.Trim() == "")
            {
                MessageBox.Show("输入不能为空！");
                high1.Focus();
                return;
            }
            if (high2.Text.Trim() == "")
            {
                MessageBox.Show("输入不能为空！");
                high2.Focus();
                return;
            }
            if (high3.Text.Trim() == "")
            {
                MessageBox.Show("输入不能为空！");
                high3.Focus();
                return;
            }
            if (high4.Text.Trim() == "")
            {
                MessageBox.Show("输入不能为空！");
                high4.Focus();
                return;
            }
            if (number.Text.Trim() == "")
            {
                MessageBox.Show("输入不能为空！");
                number.Focus();
                return;
            }
            if (maxnumber.Text.Trim() == "")
            {
                MessageBox.Show("输入不能为空！");
                maxnumber.Focus();
                return;
            }
            double aa;
            int i;
            if (!double.TryParse(high1.Text.Trim(), out aa))
            {
                MessageBox.Show("输入的不是数字！");
                high1.Focus();
                return;
            }
            if (!double.TryParse(high2.Text.Trim(), out aa))
            {
                MessageBox.Show("输入的不是数字！");
                high2.Focus();
                return;
            }
            if (!double.TryParse(high3.Text.Trim(), out aa))
            {
                MessageBox.Show("输入的不是整数！");
                high3.Focus();
                return;
            }
            if (!double.TryParse(high4.Text.Trim(), out aa))
            {
                MessageBox.Show("输入的不是数字！");
                high4.Focus();
                return;
            }
            if (!int.TryParse(zikou.Text.Trim(), out i))
            {
                MessageBox.Show("子口输入的不是整数！");
                zikou.Focus();
                return;
            }
            if (!int.TryParse(number.Text.Trim(), out i))
            {
                MessageBox.Show("每垛数量输入的不是整数！");
                number.Focus();
                return;
            }
            if (!int.TryParse(maxnumber.Text.Trim(), out i))
            {
                MessageBox.Show("装箱垛数输入的不是整数！");
                maxnumber.Focus();
                return;
            }
            #endregion
            if (button4.Text == "添加")
            {
                string dStr = "insert into luntaiweihu(guige,zikou,high1,high2,high3,high4,number,zhuangxiangnumber,flag) " +
                    "values('" + luntaiguige.Text.Trim() + "'," + zikou.Text.Trim() + "," + high1.Text.Trim() +
                    "," + high2.Text.Trim() + "," + high3.Text.Trim() + "," + high4.Text.Trim() + "," + number.Text.Trim() +
                    "," + maxnumber.Text.Trim() + ",0);";
                sql.ExecuteSqlCommand(dStr);
            }
            if (button4.Text == "修改")
            {
                string dStr = "update luntaiweihu set guige = '" + luntaiguige.Text.Trim() + "',zikou = "
                    + zikou.Text.Trim() + ",high1 = " + high1.Text.Trim() + ",high2 = " + high2.Text.Trim() +
                    ",high3 = " + high3.Text.Trim() + ",high4 =" + high4.Text.Trim() + ",number =" + number.Text.Trim() +
                    ",zhuangxiangnumber =" + maxnumber.Text.Trim() + " where id = "+ myid.ToString() + ";";
                sql.ExecuteSqlCommand(dStr);
            }
            if (button4.Text == "删除")
            {
                DialogResult dr = MessageBox.Show("删除后数据将无法修复，是否删除？", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dr == DialogResult.Yes)
                {
                    string dStr = "delete from luntaiweihu where id=" + myid.ToString();
                    sql.ExecuteSqlCommand(dStr);
                }

            }
            luntaiguige.Text = "";
            zikou.Text = "";
            high1.Text = "";
            high2.Text = "";
            high3.Text = "";
            high4.Text = "";
            number.Text = "";
            maxnumber.Text = "";
            string dStr1 = "select * from luntaiweihu";
            DataSet myds = sql.GetDataSet(dStr1, "luntaiweihu");
            this.dataGridView1.DataSource = myds.Tables[0];
            button4.Enabled = false;
        }
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
             if (e.RowIndex < 0)
            {
                return;
            }
            if (e.RowIndex >= dataGridView1.Rows.Count - 1)
            {
                return;
            }
            if (button4.Text == "修改" || button4.Text == "删除")
            {
                int.TryParse(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString(), out myid);
                luntaiguige.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                zikou.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                high1.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                high2.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                high3.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                high4.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                number.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
                maxnumber.Text = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
                button4.Enabled = true;
            }
        }

            private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            button4.Text = "添加";
            button4.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button4.Text = "修改";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button4.Text = "删除";
        }
    }
}
