using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Globalization;

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
                dateLabel.Text = Manager.GetInstance().DTNow.ToString("yyyy-MM-dd");

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
                lvi.SubItems.Add(bankDist.IsFileAllCopied ? "√" : "×");
                lvi.SubItems.Add(bankDist.Status);
                lvi.Tag = bankDist;

                if (bankDist.IsFileAllCopied)
                    lvi.BackColor = SystemColors.Window;
                else
                    lvi.BackColor = Color.Pink;

                bankDistLv.Items.Add(lvi);
            }

            bankDistLv.Columns[0].Width = -1;
            bankDistLv.Columns[1].Width = -1;
            bankDistLv.Columns[2].Width = -1;
            bankDistLv.Columns[3].Width = -1;
            bankDistLv.Columns[5].Width = -1;

            bankDistLv.EndUpdate();

            if (Manager.GetInstance().BankDistCollection.IsAllBankCopied)
            {
                isAllOKLb.Text = "是";
                isAllOKLb.ForeColor = Color.Green;
            }
            else
            {
                isAllOKLb.Text = "否";
                isAllOKLb.ForeColor = Color.Red;
            }
        }


        private void BankDistLvUpdate()
        {
            bankDistLv.BeginUpdate();
            // 进度列表
            try
            {
                for (int i = 0; i < bankDistLv.Items.Count; i++)
                {
                    BankDist bankDist = (BankDist)bankDistLv.Items[i].Tag;   // 配置对象
                    bankDistLv.Items[i].SubItems[4].Text = bankDist.IsFileAllCopied ? "√" : "×";
                    bankDistLv.Items[i].SubItems[5].Text = bankDist.Status;              // 标志到齐

                    if (bankDist.IsRunning)
                    {
                        bankDistLv.Items[i].BackColor = Color.LightBlue;
                        bankDistLv.Items[i].EnsureVisible();
                    }
                    else
                    {
                        if (bankDist.IsFileAllCopied)
                            bankDistLv.Items[i].BackColor = SystemColors.Window;
                        else
                            bankDistLv.Items[i].BackColor = Color.Pink;
                    }
                }

                bankDistLv.Columns[0].Width = -1;
                bankDistLv.Columns[1].Width = -1;
                bankDistLv.Columns[2].Width = -1;
                bankDistLv.Columns[3].Width = -1;
                bankDistLv.Columns[5].Width = -1;
            }
            catch (Exception)
            {
                // ui异常过滤
            }

            bankDistLv.EndUpdate();


            if ((Manager.GetInstance().IsRunning))
            {
                stautsLb.Text = "运行中...";
            }
            else
            {
                stautsLb.Text = "运行完毕";
            }


            if (Manager.GetInstance().BankDistCollection.IsAllBankCopied)
            {
                isAllOKLb.Text = "是";
                isAllOKLb.ForeColor = Color.Green;
            }
            else
            {
                isAllOKLb.Text = "否";
                isAllOKLb.ForeColor = Color.Red;
            }
        }


        private void BankDistDetailLvChange()
        {
            if (bankDistLv.SelectedItems != null && bankDistLv.SelectedItems.Count > 0)
            {
                ListViewItem bankDistLVI = bankDistLv.SelectedItems[0];
                if (bankDistLVI != null)
                {
                    BankDist bankDist = (BankDist)bankDistLVI.Tag;


                    bankDistDetailLv.Items.Clear();
                    bankDistDetailLv.BeginUpdate();
                    int idx = 0;
                    foreach (BankDistFile bankDistFile in bankDist.BankDistFileList)
                    {
                        ListViewItem bankDistFileLVI = new ListViewItem((++idx).ToString());
                        bankDistFileLVI.SubItems.Add(bankDistFile.BankName);
                        bankDistFileLVI.SubItems.Add(bankDistFile.FileName);
                        bankDistFileLVI.SubItems.Add(bankDistFile.FileExist ? "√" : "×");
                        bankDistFileLVI.SubItems.Add(bankDistFile.CheckedPassed ? "√" : "×");
                        bankDistFileLVI.SubItems.Add(bankDistFile.IsCopied ? "√" : "×");
                        bankDistFileLVI.SubItems.Add(bankDistFile.Status);
                        bankDistFileLVI.Tag = bankDistFile;

                        bankDistDetailLv.Items.Add(bankDistFileLVI);

                        //if (tmpHqFile.Required)
                        //{
                        //    if (tmpHqFile.IsOK)
                        //        lvHq.Items[i].BackColor = SystemColors.Window;
                        //    else
                        //        lvHq.Items[i].BackColor = Color.Pink;
                        //}
                        //else
                        //{
                        //    if (tmpHqFile.IsOK)
                        //        lvHq.Items[i].BackColor = SystemColors.Window;
                        //    else
                        //        lvHq.Items[i].BackColor = Color.LightYellow;
                        //}

                    }


                    bankDistDetailLv.Columns[0].Width = -1;
                    bankDistDetailLv.Columns[1].Width = -1;
                    bankDistDetailLv.Columns[2].Width = -1;
                    bankDistDetailLv.Columns[6].Width = -1;
                    //bankDistDetailLv.Columns[1].Width = -1;
                    //bankDistDetailLv.Columns[2].Width = -1;
                    //bankDistLv.Columns[6].Width = -1;

                    bankDistDetailLv.EndUpdate();
                }
            }
            else
            {
                bankDistDetailLv.Items.Clear();
            }
        }


        private void executeBtn_Click(object sender, EventArgs e)
        {
            if (!distBW.IsBusy)
            {
                executeBtn.Text = "执行中...";
                distBW.RunWorkerAsync();
                Manager.GetInstance().IsRunning = true;
            }
            else
            {
                executeBtn.Text = "执行";
                distBW.CancelAsync();
            }
        }


        private void distBW_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                BackgroundWorker bgWorker = sender as BackgroundWorker;


                // 循环每一个存管
                foreach (BankDist bankDist in Manager.GetInstance().BankDistCollection)
                {
                    if (true == bgWorker.CancellationPending)       // 取消事件
                    {
                        e.Cancel = true;
                        return;
                    }


                    // 标记运行状态
                    bankDist.IsStarted = true;
                    bankDist.IsRunning = true;
                    bgWorker.ReportProgress(1);
                    Thread.Sleep(40);


                    // 1.源路径是否能访问
                    if (Util.IsPathExistWithTimeout(bankDist.Source))
                    {
                        bankDist.IsSourceExist = true;
                        bgWorker.ReportProgress(1);
                    }
                    else
                    {
                        bankDist.IsSourceExist = false;
                        bgWorker.ReportProgress(1);

                        bankDist.IsRunning = false;
                        continue;
                    }
                    Thread.Sleep(40);


                    // 2.目标路径是否能访问
                    try
                    {
                        // 新建目标文件夹
                        if (!Util.IsPathExist(bankDist.Dest))
                            Directory.CreateDirectory(bankDist.Dest);


                        // 目的路径存在性
                        if (Util.IsPathExistWithTimeout(bankDist.Dest))
                        {
                            bankDist.IsDestExist = true;
                            bgWorker.ReportProgress(1);
                        }
                        else
                        {
                            bankDist.IsDestExist = false;
                            bgWorker.ReportProgress(1);

                            bankDist.IsRunning = false;
                            continue;
                        }
                    }
                    catch (Exception ex)
                    {
                        string strMessage = string.Format(@"[{0}]判断目标路径是否能访问发生异常: {1}", bankDist.Name, ex.Message);
                        UserState us = new UserState(true, strMessage);
                        bgWorker.ReportProgress(1, us);

                        bankDist.IsRunning = false;
                        continue;
                    }
                    Thread.Sleep(40);


                    // 3.判断索引文件是否存在
                    string idxFile = Path.Combine(bankDist.Source, bankDist.IdxFileName);       // 索引文件路径
                    try
                    {
                        if (File.Exists(idxFile))
                        {
                            bankDist.IsIdxFileExist = true;
                            bgWorker.ReportProgress(1);
                        }
                        else
                        {
                            bankDist.IsIdxFileExist = false;
                            bgWorker.ReportProgress(1);

                            bankDist.IsRunning = false;
                            continue;
                        }
                    }
                    catch (Exception ex)
                    {
                        string strMessage = string.Format(@"[{0}]判断索引文件是否存在发生异常: {1}", bankDist.Name, ex.Message);
                        UserState us = new UserState(true, strMessage);
                        bgWorker.ReportProgress(1, us);

                        bankDist.IsRunning = false;
                        continue;
                    }
                    Thread.Sleep(40);


                    // 4.解析索引文件，生成对象表
                    bankDist.BankDistFileList = new List<BankDistFile>();   // 清空
                    try
                    {
                        using (FileStream fs = new FileStream(idxFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                        {
                            using (StreamReader sr = new StreamReader(fs))
                            {
                                int idx = 0;                                // 行号
                                string strContent = string.Empty;           // 行内容
                                while ((strContent = sr.ReadLine()) != null)
                                {
                                    idx++;
                                    string[] tmpArr = strContent.Split('|');
                                    if (tmpArr.Length != 4)
                                        throw new Exception(string.Format(@"文件 {0} 第{1}行格式有误(竖线分割的要素不为4个)!请检查!", idxFile, idx));

                                    string fileName = tmpArr[0].Trim();
                                    long fileSize;
                                    if (!long.TryParse(tmpArr[1].Trim(), out fileSize))
                                        throw new Exception(string.Format(@"文件 {0} 第{1}行格式有误: {2}(第2个值->[文件大小KB]无法解析为整数)!请检查!", idxFile, idx, tmpArr[1]));

                                    DateTime fileDate;
                                    if (!DateTime.TryParseExact(tmpArr[2].Trim(), "yyyyMMdd", new CultureInfo("zh-CN", true), DateTimeStyles.None, out fileDate))
                                        throw new Exception(string.Format(@"文件 {0} 第{1}行格式有误: {2}(第3个值->[文件修改日期]无法解析为日期)!请检查!", idxFile, idx, tmpArr[2]));

                                    DateTime fileTime;
                                    if (!DateTime.TryParseExact(tmpArr[3].Trim(), "HH:mm:ss", new CultureInfo("zh-CN", true), DateTimeStyles.None, out fileTime))
                                        throw new Exception(string.Format(@"文件 {0} 第{1}行格式有误: {3}(第3个值->[文件修改日期]无法解析为时间)!请检查!", idxFile, idx, tmpArr[3]));

                                    // 新增对象
                                    BankDistFile bankDistFile = new BankDistFile(bankDist, fileName, fileSize, fileDate, fileTime);
                                    bankDist.BankDistFileList.Add(bankDistFile);

                                }
                                sr.Close();
                            }//eof sr
                            fs.Close();
                        }//eof fs
                    }
                    catch (Exception ex)
                    {
                        string strMessage = string.Format(@"[{0}]解析索引文件发生异常: {1}", bankDist.Name, ex.Message);
                        UserState us = new UserState(true, strMessage);
                        bgWorker.ReportProgress(1, us);

                        bankDist.IsRunning = false;
                        continue;
                    }
                    Thread.Sleep(40);


                    // 5.复制索引文件
                    try
                    {
                        string sourcePath = idxFile;
                        string destPath = Path.Combine(bankDist.Dest, bankDist.IdxFileName);
                        File.Copy(sourcePath, destPath, true);
                        bankDist.IsIdxFileCopied = true;

                    }
                    catch (Exception ex)
                    {
                        bankDist.IsIdxFileCopied = false;
                        bankDist.IsRunning = false;

                        string strMessage = string.Format(@"[{0}]复制索引文件至目的路径发生异常: {1}", bankDist.Name, ex.Message);
                        UserState us = new UserState(true, strMessage);
                        bgWorker.ReportProgress(1, us);

                        continue;
                    }
                    Thread.Sleep(40);



                    // 6.对每个文件进行校验，通过一个复制一个
                    foreach (BankDistFile bankDistFile in bankDist.BankDistFileList)
                    {
                        try
                        {
                            // 获取文件状态
                            bankDistFile.GetFileInfo();

                            // 文件大小、修改时间检查
                            bankDistFile.CheckFile();
                            bgWorker.ReportProgress(1);

                            // 拷贝文件
                            bankDistFile.CopyFile();
                            bgWorker.ReportProgress(1);


                            if (bankDistFile.Status != string.Empty)
                            {
                                string strMessage = string.Format(@"[{0}]复制文件[{1}]警告: {2}", bankDist.Name, bankDistFile.FileName, bankDistFile.Status);
                                UserState us = new UserState(true, strMessage);
                                bgWorker.ReportProgress(1, us);
                            }
                        }
                        catch (Exception ex)
                        {
                            string strMessage = string.Format(@"[{0}]复制文件[{1}]时发生异常: {2}", bankDist.Name, bankDistFile.FileName, ex.Message);
                            UserState us = new UserState(true, strMessage);
                            bgWorker.ReportProgress(1, us);

                            continue;
                        }
                    }
                    Thread.Sleep(40);


                    // 结束，关闭状态
                    bankDist.IsRunning = false;
                    bgWorker.ReportProgress(1);

                }//eof loop list
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void distBW_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // 如果有错误日志，输出
            if (e.UserState != null)
            {
                UserState us = (UserState)e.UserState;
                if (us.HasError)
                    Print_Message(us.ErrorMsg);
            }

            // 更新listView
            try
            {
                BankDistLvUpdate();
                BankDistDetailLvChange();
            }
            catch (Exception ex)
            {
                Print_Message(ex.Message);
            }
        }

        private void distBW_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            

            if (e.Error != null)    // 未处理的异常，需要弹框
            {
                Print_Message(e.Error.Message);
            }
            else if (e.Cancelled)
            {
                Print_Message("任务被手工取消");
            }
            else
            {
                Print_Message(string.Format(@"****执行完成 [进度: ({0}/{1}), 是否完成: {2}]****", Manager.GetInstance().BankDistCollection.OKCnt, Manager.GetInstance().BankDistCollection.Count, Manager.GetInstance().BankDistCollection.IsAllBankCopied ? "是" : "否"));
            }

            Manager.GetInstance().IsRunning = false;
            executeBtn.Text = "执行";

            // 最后刷新
            BankDistLvUpdate();
            BankDistDetailLvChange();
        }


        /// <summary>
        /// 输出日志
        /// </summary>
        /// <param name="message"></param>
        private void Print_Message(string message)
        {
            logTb.Text = string.Format("{0}:{1}", DateTime.Now.ToString("HH:mm:ss"), message) + System.Environment.NewLine + logTb.Text;
        }


        /// <summary>
        /// 主ListView切换事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bankDistLv_SelectedIndexChanged(object sender, EventArgs e)
        {
            BankDistDetailLvChange();
        }


        private void bankDistLv_Click(object sender, EventArgs e)
        {
            BankDistDetailLvChange();
        }
    }
}
