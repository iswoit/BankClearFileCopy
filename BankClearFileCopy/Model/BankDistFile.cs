using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace BankClearFileCopy
{
    /// <summary>
    /// 存管文件
    /// </summary>
    public class BankDistFile
    {
        private BankDist _bankDist;      // 父对象的引用
        private string _fileName;           // 文件名
        private bool _fileExist;            // 文件是否存在
        private bool _checkedPassed;        // 验证是否通过
        private bool _isCopied;             // 文件是否已经拷贝完成

        private long _fileSizeIdx;           // 文件大小-索引
        private DateTime _fileDateIdx;      // 文件修改日期-索引
        private DateTime _fileTimeIdx;      // 文件修改时间-索引

        private long _fileSizeActual;        // 文件大小-实际
        private DateTime _fileDateActual;   // 文件修改日期-实际
        private DateTime _fileTimeActual;   // 文件修改时间-实际

        private string _stauts;              // 错误信息


        /// <summary>
        /// 存管文件-构造函数
        /// </summary>
        /// <param name="bankDist"></param>
        /// <param name="fileName"></param>
        /// <param name="fileSize"></param>
        /// <param name="fileDate"></param>
        /// <param name="fileTime"></param>
        public BankDistFile(BankDist bankDist, string fileName, long fileSize, DateTime fileDate, DateTime fileTime)
        {
            _bankDist = bankDist;
            _fileName = fileName;
            _fileSizeIdx = fileSize;
            _fileDateIdx = fileDate;
            _fileTimeIdx = fileTime;

            _fileExist = false;
            _checkedPassed = false;
            _isCopied = false;
        }


        /// <summary>
        /// 判断文件是否存在，以及文件的大小、修改日期、修改时间
        /// </summary>
        public void GetFileInfo()
        {
            // 1.判断文件是否存在
            string sourceFilePath = Path.Combine(_bankDist.Source, _fileName);
            if (File.Exists(sourceFilePath))
            {
                _fileExist = true;

                FileInfo fi = new FileInfo(sourceFilePath);
                _fileSizeActual = fi.Length;
                _fileDateActual = fi.LastWriteTime;
                _fileTimeActual = fi.LastWriteTime;

                _stauts = "文件存在";
            }
            else
            {
                _fileExist = false;
                _stauts = "文件不存在";
            }
        }



        /// <summary>
        /// 检查文件（20180324：只检查文件大小）
        /// </summary>
        /// <returns></returns>
        public bool CheckFile()
        {
            _stauts = string.Empty;
            if (IsSizeCheckPass() && IsDateCheckPass() && IsTimeCheckPass())//if (IsSizeCheckPass() && IsDateCheckPass() && IsTimeCheckPass())
            {
                _checkedPassed = true;
                return true;
            }
            else
            {
                _checkedPassed = false;
                return false;
            }
        }


        private bool IsSizeCheckPass()
        {
            if (_fileExist == false)
            {
                _checkedPassed = false;
                return false;
            }

            if (_fileSizeIdx == _fileSizeActual)
            {
                _stauts = string.Empty;
                return true;
            }
            else
            {
                _stauts = string.Format(@"[{0}]文件[{1}]在索引文件中大小为[{2}], 实际大小为[{3}]. 请检查!", _bankDist.Name, _fileName, _fileSizeIdx, _fileSizeActual);
                _checkedPassed = false;
                return false;
            }
        }


        private bool IsDateCheckPass()
        {
            if (_fileExist == false)
            {
                _checkedPassed = false;
                return false;
            }

            if (_fileDateIdx.Date == _fileDateActual.Date)
            {
                _stauts = string.Empty;
                return true;
            }
            else
            {
                _stauts = string.Format(@"[{0}]文件[{1}]在索引文件中日期为[{2}], 实际日期为[{3}]. 请检查!", _bankDist.Name, _fileName, _fileDateIdx.ToString("yyyyMMdd"), _fileDateActual.ToString("yyyyMMdd"));
                _checkedPassed = true;      // 暂时放过
                return true;
            }
        }


        private bool IsTimeCheckPass()
        {
            if (_fileExist == false)
            {
                _checkedPassed = false;
                return false;
            }

            if (_fileTimeIdx.Hour == _fileTimeActual.Hour
                && _fileTimeIdx.Minute == _fileTimeActual.Minute
                && _fileTimeIdx.Second == _fileTimeActual.Second)
            {
                _stauts = string.Empty;
                return true;
            }
            else
            {
                _stauts = string.Format(@"[{0}]文件[{1}]在索引文件中修改时间为[{2}], 实际修改时间为[{3}]. 请检查!", _bankDist.Name, _fileName, _fileTimeIdx.ToString("HH:mm:ss"), _fileTimeActual.ToString("HH:mm:ss"));
                _checkedPassed = true;  // 暂时放过
                return true;
            }
        }


        public void CopyFile()
        {
            if (_checkedPassed)
            {
                string sourcePath = Path.Combine(_bankDist.Source, _fileName);

                foreach(string s in _bankDist.Dest)
                {
                    string destPath = Path.Combine(s, _fileName);

                    try
                    {
                        File.Copy(sourcePath, destPath, true);
                        
                    }
                    catch (Exception ex)
                    {
                        _isCopied = false;
                        _stauts = ex.Message;
                    }
                }
                _isCopied = true;
            }
        }



        public string BankName
        {
            get { return _bankDist.Name; }
        }

        public string FileName
        {
            get { return _fileName; }
        }

        public bool FileExist
        {
            get { return _fileExist; }
            set { _fileExist = value; }
        }

        public long FileSizeIdx
        {
            get { return _fileSizeIdx; }
        }

        public DateTime FileDateIdx
        {
            get { return _fileDateIdx; }
        }

        public DateTime FileTimeIdx
        {
            get { return _fileTimeIdx; }
        }

        public long FileSizeActual
        {
            get { return _fileSizeActual; }
        }

        public DateTime FileDateActual
        {
            get { return _fileDateActual; }
        }

        public DateTime FileTimeActual
        {
            get { return _fileTimeActual; }
        }

        public bool CheckedPassed
        {
            get { return _checkedPassed; }
        }


        public bool IsCopied
        {
            get { return _isCopied; }
        }

        /// <summary>
        /// 文件错误信息
        /// </summary>
        public string Status
        {
            get { return _stauts; }
        }

    }
}
