using System;
using System.Collections.Generic;
using System.Text;

namespace BankClearFileCopy
{
    /// <summary>
    /// 存管清算发送
    /// </summary>
    public class BankDist
    {
        private string _name;                           // 存管名称
        private string _source;                         // 源路径
        private string _dest;                           // 目的路径
        private List<BankDistFile> _bankDistFileList;       // 存管清算发送文件列表
        private bool _isRunning;                        // 是否运行中
        private string status;                          // 说明

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name"></param>
        /// <param name="source"></param>
        /// <param name="dest"></param>
        public BankDist(string name, string source, string dest)
        {
            _name = name;
            _source = source;
            _dest = dest;
            _bankDistFileList = new List<BankDistFile>();

            _isRunning = false;
            status = "未开始";
        }


        public string Name
        {
            get { return _name; }
        }

        public string Source
        {
            get { return _source; }
        }

        public string Dest
        {
            get { return _dest; }
        }

        /// <summary>
        /// 子文件是否都完成(要改)
        /// </summary>
        public bool IsOK
        {
            get { return false; }
        }

        public string Status
        {
            get { return status; }
        }

        public List<BankDistFile> BankDistFileList
        {
            get { return _bankDistFileList; }
        }

        
    }
}
