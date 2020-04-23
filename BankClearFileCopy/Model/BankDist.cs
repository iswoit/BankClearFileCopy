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
        private List<string> _dest;                           // 目的路径-20200423：修改为支持多目的
        private List<BankDistFile> _bankDistFileList;       // 存管清算发送文件列表
        private string _idxFileName;                    // 索引文件名


        // 运行时变量
        private bool _isStarted;
        private bool _isRunning;                        // 是否运行中
        private bool _isSourceExist;                    // 源文件是否可访问
        private bool _isDestExist;                      // 目的文件是否可访问
        private bool _isIdxFileExist;                   // 索引文件是否存在
        private bool _isIdxFileCopied;                  // 索引文件是否已经拷贝

        private string _status;                          // 说明

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name"></param>
        /// <param name="source"></param>
        /// <param name="dest"></param>
        public BankDist(string name, string source, List<string> dest, DateTime dtNow)
        {
            _name = name;
            _source = source;
            _dest = dest;
            _bankDistFileList = new List<BankDistFile>();

            _idxFileName = Util.ReplaceStringWithDateFormat(@"YYYYMMDD证券清算文件.txt", dtNow);

            _isStarted = false;
            _isRunning = false;
        }


        public string Name
        {
            get { return _name; }
        }

        public string Source
        {
            get { return _source; }
        }

        public List<string> Dest
        {
            get
            {
                return _dest;
            }
        }


        public string DestDescription
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                foreach(string s in _dest)
                {
                    if (sb.Length != 0)
                        sb.Append(";");
                    sb.Append(s);
                }
                return sb.ToString();
            }
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


        public bool IsStarted
        {
            get { return _isStarted; }
            set { _isStarted = value; }
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
            get
            {
                if (_isStarted == false)
                    return "未开始";
                else
                {
                    if (_isRunning)
                    {
                        return "运行中...";
                    }
                    else if (_isSourceExist == false)
                        return "源路径无法访问";
                    else if (_isDestExist == false)
                        return "目的路径无法访问";
                    else if (_isIdxFileExist == false)
                        return "索引文件不存在[YYYYMMDD证券清算文件.txt]";
                    else if (_isIdxFileCopied == false)
                        return "索引文件拷贝失败";
                    else
                    {
                        if (IsFileAllCopied)
                            return "完成";
                        else
                            return "已执行, 有误";
                    }
                }
            }
            set { _status = value; }
        }

        public List<BankDistFile> BankDistFileList
        {
            get { return _bankDistFileList; }
            set { _bankDistFileList = value; }
        }



        public bool IsFileAllCopied
        {
            get
            {
                if (_bankDistFileList == null || _bankDistFileList.Count <= 0)
                    return false;

                if (_isIdxFileExist == false || _isIdxFileCopied == false)
                    return false;

                foreach (BankDistFile bankDistFile in _bankDistFileList)
                {
                    if (bankDistFile.IsCopied == false)
                        return false;
                }

                return true;
            }
        }

    }
}
