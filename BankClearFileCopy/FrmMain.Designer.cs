﻿namespace BankClearFileCopy
{
    partial class FrmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.mainTab = new System.Windows.Forms.TabControl();
            this.distributeTabPage = new System.Windows.Forms.TabPage();
            this.isAllOKLb = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.stautsLb = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.logTb = new System.Windows.Forms.TextBox();
            this.executeBtn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.distBW = new System.ComponentModel.BackgroundWorker();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.dateLabel = new System.Windows.Forms.ToolStripLabel();
            this.bankDistDetailLv = new BankClearFileCopy.DoubleBufferListView();
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader13 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader12 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.bankDistLv = new BankClearFileCopy.DoubleBufferListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.mainTab.SuspendLayout();
            this.distributeTabPage.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainTab
            // 
            this.mainTab.Controls.Add(this.distributeTabPage);
            this.mainTab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTab.Location = new System.Drawing.Point(0, 0);
            this.mainTab.Name = "mainTab";
            this.mainTab.SelectedIndex = 0;
            this.mainTab.Size = new System.Drawing.Size(933, 570);
            this.mainTab.TabIndex = 0;
            // 
            // distributeTabPage
            // 
            this.distributeTabPage.BackColor = System.Drawing.SystemColors.Control;
            this.distributeTabPage.Controls.Add(this.isAllOKLb);
            this.distributeTabPage.Controls.Add(this.label6);
            this.distributeTabPage.Controls.Add(this.stautsLb);
            this.distributeTabPage.Controls.Add(this.label4);
            this.distributeTabPage.Controls.Add(this.label3);
            this.distributeTabPage.Controls.Add(this.logTb);
            this.distributeTabPage.Controls.Add(this.executeBtn);
            this.distributeTabPage.Controls.Add(this.label2);
            this.distributeTabPage.Controls.Add(this.label1);
            this.distributeTabPage.Controls.Add(this.bankDistDetailLv);
            this.distributeTabPage.Controls.Add(this.bankDistLv);
            this.distributeTabPage.Location = new System.Drawing.Point(4, 22);
            this.distributeTabPage.Name = "distributeTabPage";
            this.distributeTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.distributeTabPage.Size = new System.Drawing.Size(925, 544);
            this.distributeTabPage.TabIndex = 0;
            this.distributeTabPage.Text = "清算后发送银行数据";
            // 
            // isAllOKLb
            // 
            this.isAllOKLb.AutoSize = true;
            this.isAllOKLb.Font = new System.Drawing.Font("SimSun", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.isAllOKLb.Location = new System.Drawing.Point(850, 454);
            this.isAllOKLb.Name = "isAllOKLb";
            this.isAllOKLb.Size = new System.Drawing.Size(29, 19);
            this.isAllOKLb.TabIndex = 10;
            this.isAllOKLb.Text = "否";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(785, 460);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 12);
            this.label6.TabIndex = 9;
            this.label6.Text = "是否完成:";
            // 
            // stautsLb
            // 
            this.stautsLb.AutoSize = true;
            this.stautsLb.Location = new System.Drawing.Point(850, 428);
            this.stautsLb.Name = "stautsLb";
            this.stautsLb.Size = new System.Drawing.Size(41, 12);
            this.stautsLb.TabIndex = 8;
            this.stautsLb.Text = "未运行";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(785, 428);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "运行状态:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 415);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "日志:";
            // 
            // logTb
            // 
            this.logTb.Location = new System.Drawing.Point(16, 430);
            this.logTb.Multiline = true;
            this.logTb.Name = "logTb";
            this.logTb.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.logTb.Size = new System.Drawing.Size(748, 102);
            this.logTb.TabIndex = 5;
            // 
            // executeBtn
            // 
            this.executeBtn.Location = new System.Drawing.Point(801, 494);
            this.executeBtn.Name = "executeBtn";
            this.executeBtn.Size = new System.Drawing.Size(75, 26);
            this.executeBtn.TabIndex = 4;
            this.executeBtn.Text = "执行";
            this.executeBtn.UseVisualStyleBackColor = true;
            this.executeBtn.Click += new System.EventHandler(this.executeBtn_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 191);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "存管文件明细:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "存管列表:";
            // 
            // distBW
            // 
            this.distBW.WorkerReportsProgress = true;
            this.distBW.WorkerSupportsCancellation = true;
            this.distBW.DoWork += new System.ComponentModel.DoWorkEventHandler(this.distBW_DoWork);
            this.distBW.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.distBW_ProgressChanged);
            this.distBW.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.distBW_RunWorkerCompleted);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dateLabel});
            this.toolStrip1.Location = new System.Drawing.Point(0, 570);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(933, 29);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // dateLabel
            // 
            this.dateLabel.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.dateLabel.Font = new System.Drawing.Font("Microsoft YaHei", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dateLabel.Name = "dateLabel";
            this.dateLabel.Size = new System.Drawing.Size(146, 26);
            this.dateLabel.Text = "YYYY-MM-DD";
            // 
            // bankDistDetailLv
            // 
            this.bankDistDetailLv.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader5,
            this.columnHeader13,
            this.columnHeader6,
            this.columnHeader7,
            this.columnHeader8,
            this.columnHeader11,
            this.columnHeader12});
            this.bankDistDetailLv.FullRowSelect = true;
            this.bankDistDetailLv.GridLines = true;
            this.bankDistDetailLv.Location = new System.Drawing.Point(16, 206);
            this.bankDistDetailLv.Name = "bankDistDetailLv";
            this.bankDistDetailLv.Size = new System.Drawing.Size(883, 176);
            this.bankDistDetailLv.TabIndex = 1;
            this.bankDistDetailLv.UseCompatibleStateImageBehavior = false;
            this.bankDistDetailLv.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "No";
            this.columnHeader5.Width = 40;
            // 
            // columnHeader13
            // 
            this.columnHeader13.Text = "存管名称";
            this.columnHeader13.Width = 80;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "文件名";
            this.columnHeader6.Width = 372;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "文件存在";
            this.columnHeader7.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "校验通过";
            this.columnHeader8.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "拷贝完成";
            this.columnHeader11.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // columnHeader12
            // 
            this.columnHeader12.Text = "说明";
            this.columnHeader12.Width = 160;
            // 
            // bankDistLv
            // 
            this.bankDistLv.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader9,
            this.columnHeader10,
            this.columnHeader3,
            this.columnHeader4});
            this.bankDistLv.FullRowSelect = true;
            this.bankDistLv.GridLines = true;
            this.bankDistLv.HideSelection = false;
            this.bankDistLv.Location = new System.Drawing.Point(16, 25);
            this.bankDistLv.MultiSelect = false;
            this.bankDistLv.Name = "bankDistLv";
            this.bankDistLv.Size = new System.Drawing.Size(883, 152);
            this.bankDistLv.TabIndex = 0;
            this.bankDistLv.UseCompatibleStateImageBehavior = false;
            this.bankDistLv.View = System.Windows.Forms.View.Details;
            this.bankDistLv.SelectedIndexChanged += new System.EventHandler(this.bankDistLv_SelectedIndexChanged);
            this.bankDistLv.Click += new System.EventHandler(this.bankDistLv_Click);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "No";
            this.columnHeader1.Width = 40;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "存管名称";
            this.columnHeader2.Width = 80;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "源路径";
            this.columnHeader9.Width = 260;
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "目的路径";
            this.columnHeader10.Width = 260;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "是否完成";
            this.columnHeader3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "说明";
            this.columnHeader4.Width = 166;
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(933, 599);
            this.Controls.Add(this.mainTab);
            this.Controls.Add(this.toolStrip1);
            this.Name = "FrmMain";
            this.Text = "银行清算文件拷贝";
            this.mainTab.ResumeLayout(false);
            this.distributeTabPage.ResumeLayout(false);
            this.distributeTabPage.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl mainTab;
        private System.Windows.Forms.TabPage distributeTabPage;
        private DoubleBufferListView bankDistLv;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private DoubleBufferListView bankDistDetailLv;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ColumnHeader columnHeader11;
        private System.Windows.Forms.ColumnHeader columnHeader12;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.ColumnHeader columnHeader10;
        private System.Windows.Forms.Button executeBtn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox logTb;
        private System.Windows.Forms.Label isAllOKLb;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label stautsLb;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ColumnHeader columnHeader13;
        private System.ComponentModel.BackgroundWorker distBW;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel dateLabel;
    }
}

