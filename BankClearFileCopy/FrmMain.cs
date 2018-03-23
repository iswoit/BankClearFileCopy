using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BankClearFileCopy
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();

            try
            {
                Manager.GetInstance();
                //Print_Message(string.Format(@"从数据库获取上一交易日成功, 上一交易日: {0}", manager.DtPrevious));

                // 更新UI
                BankDistLvInit();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Environment.Exit(0);
            }
        }


        /// <summary>
        /// 初始化存管列表
        /// </summary>
        private void BankDistLvInit()
        {
            bankDistLv.Items.Clear();
            bankDistLv.BeginUpdate();
            int idx = 0;    // 计数器
            foreach (BankDist bankDist in Manager.GetInstance().BankDistCollection)
            {
                ListViewItem lvi = new ListViewItem((++idx).ToString());
                lvi.SubItems.Add(bankDist.Name);
                lvi.SubItems.Add(bankDist.Source);
                lvi.SubItems.Add(bankDist.Dest);
                lvi.SubItems.Add(bankDist.IsOK ? "√" : "×");
                lvi.SubItems.Add(bankDist.Status);
                lvi.Tag = bankDist;

                if (bankDist.IsOK)
                    lvi.BackColor = SystemColors.Window;
                else
                    lvi.BackColor = Color.Pink;

                bankDistLv.Items.Add(lvi);
            }

            bankDistLv.Columns[0].Width = -1;
            bankDistLv.Columns[1].Width = -1;
            bankDistLv.Columns[2].Width = -1;
            bankDistLv.Columns[3].Width = -1;

            bankDistLv.EndUpdate();
        }
    }
}
