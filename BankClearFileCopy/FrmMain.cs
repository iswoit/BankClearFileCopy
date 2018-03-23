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


        private void executeBtn_Click(object sender, EventArgs e)
        {
            if (!distBW.IsBusy)
            {
                executeBtn.Text = "执行中...";
                distBW.RunWorkerAsync();
                Manager.GetInstance().IsRunning = true;
                stautsLb.Text = "正在运行...";
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


                    // 0.设置运行状态为运行中，需要重置状态（还没搞）
                    bankDist.IsRunning = true;
                    bgWorker.ReportProgress(1);
                    Thread.Sleep(50);


                    // 1.源路径是否能访问
                    if (!Util.IsPathExistWithTimeout(bankDist.Source))
                    {
                        bankDist.IsSourceExist = false;
                        bgWorker.ReportProgress(1);

                        continue;
                    }
                    else
                    {
                        bankDist.IsSourceExist = true;
                        bgWorker.ReportProgress(1);
                    }


                    // 目标路径是否能访问（！目的路径是否能访问，这个有问题，因为有可能文件夹不存在）
                    if (!Util.IsPathExistWithTimeout(bankDist.Dest))
                    {
                        Directory.CreateDirectory
                        bankDist.IsSourceExist = false;
                        bgWorker.ReportProgress(1);

                        continue;
                    }
                    else
                    {
                        bankDist.IsSourceExist = true;
                        bgWorker.ReportProgress(1);
                    }


                    // 2.判断索引文件是否存在
                    string idxFile = Path.Combine(bankDist.Source, Util.ReplaceStringWithDateFormat(@"YYYYMMDD证券清算文件.txt", Manager.GetInstance().DTNow));
                    if (!File.Exists(idxFile))
                    {
                        bankDist.IsIdxFileExist = false;
                        bgWorker.ReportProgress(1);
                    }
                    else
                    {
                        bankDist.IsIdxFileExist = true;
                        bgWorker.ReportProgress(1);
                    }

                    //FileInfo fi = new FileInfo(idxFile);
                    //fi.

                    // 3.解析索引文件，生成对象表
                    bankDist.BankDistFileList = new List<BankDistFile>();   // 清空
                    try
                    {
                        using (FileStream fs = new FileStream(idxFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                        {
                            using (StreamReader sr = new StreamReader(fs))
                            {
                                int idx = 0;
                                string strContent = string.Empty;  // 行内容
                                while ((strContent = sr.ReadLine()) != null)
                                {
                                    idx++;  // 行号
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
                        throw new Exception(ex.Message);
                    }


                    // 4.对每个文件进行校验，通过一个复制一个



                    

                }



                //// 解压清算文件

                //foreach (ZipFile zipFile in manager.ZipFileCollection)
                //{
                //    try
                //    {
                //        if (true == bgWorker.CancellationPending)
                //        {
                //            e.Cancel = true;
                //            return;
                //        }

                //        zipFile.Unzip();
                //    }
                //    catch (Exception ex)
                //    {
                //        UserState us = new UserState(true, string.Format(@"解压清算文件出现异常{0}, {1}!", zipFile.Path, ex.Message));
                //        bgWorker.ReportProgress(1, us);
                //    }
                //}

                //// 更新清算文件状态
                //foreach (ClearFile clearFile in manager.ClearFileCollection)
                //{
                //    try
                //    {

                //        if (true == bgWorker.CancellationPending)
                //        {
                //            e.Cancel = true;
                //            return;
                //        }

                //        clearFile.UpdateClearFileListStatus(manager);
                //    }
                //    catch (Exception ex)
                //    {
                //        UserState us = new UserState(true, string.Format(@"检查清算原始文件文件出现异常{0}, {1}!", clearFile.FilePath, ex.Message));
                //        bgWorker.ReportProgress(1, us);
                //    }

                //}


                //// 开始分解每支产品的清算文件
                //foreach (Product product in manager.ProductCollection)
                //{
                //    try
                //    {
                //        if (true == bgWorker.CancellationPending)
                //        {
                //            e.Cancel = true;
                //            return;
                //        }

                //        product.ProcessProductFiles(manager.LogQueue);
                //        bgWorker.ReportProgress(1);
                //    }
                //    catch (Exception ex)
                //    {
                //        UserState us = new UserState(true, string.Format(@"分解产品{0}出现异常{1}!", product.ProductName, ex.Message));
                //        bgWorker.ReportProgress(1, us);
                //    }
                //}
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
                //ProductListViewUpdate();
                //FileDetailListViewChange();
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

                //lbStatus.Text = "异常停止";
                //lbStatus.BackColor = Color.Red;
            }
            else if (e.Cancelled)
            {
                Print_Message("任务被手工取消");

                //lbStatus.Text = "手工停止";
                //lbStatus.BackColor = Color.Red;

                // 状态清空
                //for (int i = 0; i < _manager.ListHqFile.Count; i++)
                //{
                //    //_manager.ListHqFile[i].IsRunning = false;
                //}
            }
            else
            {
                //UpdateLvHq();
                //Print_Message(string.Format(@"****检查完毕 [文件进度: 所有文件({2}/{3}), 必检文件({0}/{1}), 是否已就绪: {4}]****", _manager.GetFinishedRequiredCnt, _manager.GetAllRequiredCnt, _manager.GetFinishedCnt, _manager.GetAllCnt, _manager.IsAllOK ? "是" : "否"));
                //UpdateFileSourceInfo();
                //UpdateFileListInfo();

                //// 处理状态标签
                //lbStatus.Text = "完成，等待下一轮";
                //lbStatus.BackColor = Color.ForestGreen;

                Print_Message("执行完成");

                //ProductListViewUpdate();
                //FileDetailListViewChange();
            }

            //manager.IsRunning = false;
            //statusLabel.Text = "运行完毕";

            //isOKLabel.Text = manager.IsAllOK ? "是" : "否";
            //executeLoopButton.Text = "开始自动执行";
        }


        private void Print_Message(string message)
        {
            logTb.Text = string.Format("{0}:{1}", DateTime.Now.ToString("HH:mm:ss"), message) + System.Environment.NewLine + logTb.Text;
        }

    }
}
