using System;
using System.Collections.Generic;
using System.Text;

namespace BankClearFileCopy
{
    /// <summary>
    /// 存管文件
    /// </summary>
    public class BankDistFile
    {
        private string _fileName;           // 文件名
        private bool _fileExist;            // 文件是否存在

        private int _fileSizeIdx;           // 文件大小-索引
        private DateTime _fileDateIdx;      // 文件修改日期-索引
        private DateTime _fileTimeIdx;      // 文件修改时间-索引

        private int _fileSizeActual;        // 文件大小-实际
        private DateTime _fileTimeActual;   // 文件修改时间-实际
        private DateTime _fileDateActual;   // 文件修改日期-实际

        private BankDist _bankDistribution;      // 父对象的引用


        /// <summary>
        /// 存管文件-构造函数
        /// </summary>
        /// <param name="bankDistribution"></param>
        /// <param name="fileName"></param>
        /// <param name="fileSize"></param>
        /// <param name="fileDate"></param>
        /// <param name="fileTime"></param>
        //public BankDistFile(BankDist bankDistribution, string fileName, string fileSize, string fileDate, string fileTime)
        //{

        //}
    }
}
