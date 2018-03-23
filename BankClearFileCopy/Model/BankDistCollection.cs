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

        public bool IsAllOK
        {
            get
            {
                foreach(BankDist bankDist in this)
                {
                    if (!bankDist.IsFileAllCopied)
                        return false;
                }

                return true;
            }
        }
    }
}
