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
        // 静态变量
        private string _name;                           // 存管名称
        private string _source;                         // 源路径
        private string _dest;                           // 目的路径
        private List<BankDistFile> _bankDistFileList;       // 存管清算发送文件列表
        private string _idxFileName;                    // 索引文件名


        // 运行时变量
        private bool _isRunning;                        // 是否运行中
        private bool _isSourceExist;                    // 源文件是否可访问
        private bool _isDestExist;                      // 目的文件是否可访问
        private bool _isIdxFileExist;                   // 索引文件是否存在
        private bool _isIdxFileCopied;                  // 索引文件是否已经拷贝

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

            _idxFileName = Util.ReplaceStringWithDateFormat(@"YYYYMMDD证券清算文件.txt", Manager.GetInstance().DTNow);


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
        /// 索引文件名
        /// </summary>
        public string IdxFileName
        {
            get { return _idxFileName; }
        }

        /// <summary>
        /// 索引文件是否拷贝完成
        /// </summary>
        public bool IsIdxFileCopied
        {
            get { return _isIdxFileCopied; }
            set { _isIdxFileCopied = value; }
        }

        /// <summary>
        /// 子文件是否都完成(要改)
        /// </summary>
        public bool IsOK
        {
            get { return false; }
        }

        /// <summary>
        /// 此存管是否在运行中
        /// </summary>
        public bool IsRunning
        {
            get { return _isRunning; }
            set { _isRunning = value; }
        }

        /// <summary>
        /// 源路径是否能访问
        /// </summary>
        public bool IsSourceExist
        {
            get { return _isSourceExist; }
            set { _isSourceExist = value; }
        }

        /// <summary>
        /// 目的路径是否能访问
        /// </summary>
        public bool IsDestExist
        {
            get { return _isDestExist; }
            set { _isDestExist = value; }
        }


        /// <summary>
        /// 源路径下的索引文件是否存在
        /// </summary>
        public bool IsIdxFileExist
        {
            get { return _isIdxFileExist; }
            set { _isIdxFileExist = value; }
        }

        public string Status
        {
            get { return status; }
        }

        public List<BankDistFile> BankDistFileList
        {
            get { return _bankDistFileList; }
            set { _bankDistFileList = value; }
        }


    }
}
