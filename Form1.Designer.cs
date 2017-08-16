namespace OPCDA
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.luntaiguige = new System.Windows.Forms.TextBox();
            this.djqchushihua = new System.Windows.Forms.Button();
            this.djhchushihua = new System.Windows.Forms.Button();
            this.weihu = new System.Windows.Forms.Button();
            this.xiugai = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(22, 24);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(308, 25);
            this.textBox1.TabIndex = 0;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 15;
            this.listBox1.Location = new System.Drawing.Point(22, 67);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(308, 304);
            this.listBox1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 395);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 15);
            this.label1.TabIndex = 5;
            this.label1.Text = "轮胎规格";
            // 
            // luntaiguige
            // 
            this.luntaiguige.Location = new System.Drawing.Point(83, 392);
            this.luntaiguige.Name = "luntaiguige";
            this.luntaiguige.Size = new System.Drawing.Size(166, 25);
            this.luntaiguige.TabIndex = 6;
            this.luntaiguige.TextChanged += new System.EventHandler(this.luntaiguige_TextChanged);
            // 
            // djqchushihua
            // 
            this.djqchushihua.Location = new System.Drawing.Point(22, 436);
            this.djqchushihua.Name = "djqchushihua";
            this.djqchushihua.Size = new System.Drawing.Size(126, 23);
            this.djqchushihua.TabIndex = 7;
            this.djqchushihua.Text = "动均前初始化";
            this.djqchushihua.UseVisualStyleBackColor = true;
            this.djqchushihua.Click += new System.EventHandler(this.djqchushihua_Click);
            // 
            // djhchushihua
            // 
            this.djhchushihua.Location = new System.Drawing.Point(183, 436);
            this.djhchushihua.Name = "djhchushihua";
            this.djhchushihua.Size = new System.Drawing.Size(126, 23);
            this.djhchushihua.TabIndex = 8;
            this.djhchushihua.Text = "动均后初始化";
            this.djhchushihua.UseVisualStyleBackColor = true;
            this.djhchushihua.Click += new System.EventHandler(this.djhchushihua_Click);
            // 
            // weihu
            // 
            this.weihu.Location = new System.Drawing.Point(22, 477);
            this.weihu.Name = "weihu";
            this.weihu.Size = new System.Drawing.Size(126, 23);
            this.weihu.TabIndex = 9;
            this.weihu.Text = "轮胎信息维护";
            this.weihu.UseVisualStyleBackColor = true;
            this.weihu.Click += new System.EventHandler(this.weihu_Click);
            // 
            // xiugai
            // 
            this.xiugai.Location = new System.Drawing.Point(268, 395);
            this.xiugai.Name = "xiugai";
            this.xiugai.Size = new System.Drawing.Size(75, 23);
            this.xiugai.TabIndex = 10;
            this.xiugai.Text = "修改";
            this.xiugai.UseVisualStyleBackColor = true;
            this.xiugai.Click += new System.EventHandler(this.xiugai_Click);
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(355, 525);
            this.Controls.Add(this.xiugai);
            this.Controls.Add(this.weihu);
            this.Controls.Add(this.djhchushihua);
            this.Controls.Add(this.djqchushihua);
            this.Controls.Add(this.luntaiguige);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.textBox1);
            this.Name = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox luntaiguige;
        private System.Windows.Forms.Button djqchushihua;
        private System.Windows.Forms.Button djhchushihua;
        private System.Windows.Forms.Button weihu;
        private System.Windows.Forms.Button xiugai;
    }
}

