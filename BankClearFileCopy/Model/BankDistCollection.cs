using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace BankClearFileCopy
{
    /// <summary>
    /// 存管清算文件集合对象
    /// </summary>
    public class BankDistCollection : Collection<BankDist>
    {
        /// <summary>
        /// 所有存管清算文件已发送
        /// </summary>
        public bool IsAllBankCopied
        {
            get
            {
                foreach (BankDist bankDist in this)
                {
                    if (!bankDist.IsIdxFileCopied || !bankDist.IsFileAllCopied)   // 只要有一个存管索引文件或者文件没发，就报错
                        return false;
                }

                return true;
            }
        }


        /// <summary>
        /// 完成的个数
        /// </summary>
        public int OKCnt
        {
            get
            {
                int cnt = 0;
                foreach (BankDist bankDist in this)
                {
                    if (bankDist.IsFileAllCopied)
                        cnt++;
                }
                return cnt;
            }
        }
    }
}
